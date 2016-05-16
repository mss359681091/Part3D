﻿using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class WebUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["txtPartname"] != null && Request["hidClassifyId"] != null)
            {
                string strName = Request["txtPartname"];
                string strClassify = this.hidClassifyId.Value;
                //var file = Request.Files[0];

                HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;

                if (strName.Length > 0 && strClassify.Length > 0)
                {
                    fnSaveImg(strClassify, strName, file[0]);//保存信息
                }
            }

            if (!IsPostBack)
            {
                if (Session[sysUser.Username] == null)
                {
                    Response.Redirect("/Login.aspx");
                }
            }
        }

        public static void MakeThumbnail(string sourcePath, string newPath, int width, int height)
        {
            System.Drawing.Image ig = System.Drawing.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(ig, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }


        public void fnSaveImg(string clssifyid, string partname, HttpPostedFile inputfile)
        {

            //第一步上传封面
            string uploadPath = HttpContext.Current.Server.MapPath(@"/user/jqUploadify/uploads/f/");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string filePath = inputfile.FileName;//文件路径
            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);//文件名称
            string fileExtname = System.IO.Path.GetExtension(filePath).ToLower();//文件扩展名
            string ran = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999);
            string temp = uploadPath + ran;
            string newfilename = temp + fileExtname; //新文件名称
            string newfile_s_name = temp + "_s" + fileExtname;//新文件名称（缩略图）
            inputfile.SaveAs(newfilename);//上传文件
            MakeThumbnail(newfilename, newfile_s_name, 240, 170);//生成缩略图


            //第二步插入插件数据
            dpPartManager mydpPartManager = new dpPartManager();
            Hashtable myHashtable = new Hashtable();
            myHashtable.Add(dpPart.UserID, HttpContext.Current.Session["ID"].ToString());//用户ID
            myHashtable.Add(dpPart.ParentID, "0");//父组件ID
            myHashtable.Add(dpPart.ClassifyID, clssifyid);//班级ID
            myHashtable.Add(dpPart.Name, partname);//组件名称
            myHashtable.Add(dpPart.Preview, @"/user/jqUploadify/uploads/f/" + ran + fileExtname); //封面
            myHashtable.Add(dpPart.PreviewSmall, @"/user/jqUploadify/uploads/f/" + ran + "_s" + fileExtname);//封面缩略图
            myHashtable.Add(dpPart.Description, "");
            myHashtable.Add(dpPart.Limits, "");
            myHashtable.Add(dpPart.Keyword, "");
            int partid = SQLHelper.ExcuteProc("sp_AddPart", myHashtable);//执行存储过程并返回id

            //第三部插入附件表
            if (HttpContext.Current.Session["modefile"] != null)
            {
                string modefiles = HttpContext.Current.Session["modefile"].ToString();
                if (modefiles.Length > 0)
                {
                    modefiles = modefiles.Substring(0, modefiles.Length - 1);
                }
                string[] strmodesfiles = modefiles.Split(',');
                if (strmodesfiles.Length > 0)
                {
                    for (int i = 0; i < strmodesfiles.Length; i++)
                    {
                        string[] str = strmodesfiles[i].Split(';');
                        if (str.Length > 0)
                        {
                            string strmodels = string.Empty;
                            if (str[0].Contains(' '))
                            {
                                string standard = str[0].Split(' ')[0];

                                if (str[0].Contains("x"))
                                {
                                    strmodels = str[0].Split(' ')[1].Split('x')[0];
                                }
                                savestandard(standard, partid);//保存标准
                            }

                            Hashtable ht = new Hashtable();
                            ht.Add(dpModelFile.PartID, partid);//文件id
                            ht.Add(dpModelFile.Name, str[0]);//文件名称
                            ht.Add(dpModelFile.Format, str[1]);//文件格式
                            ht.Add(dpModelFile.Location, str[2]);//文件路径
                            ht.Add(dpModelFile.Size, str[3]);//文件大小
                            ht.Add(dpModelFile.Models, strmodels);//型号
                            SQLHelper.ExcuteProc("sp_AddModelFile", ht);//执行存储过程

                         
                        }
                    }
                }
                Session.Remove("modefile");//移除
            }
        }

        private void savestandard(string standard, int partid)
        {

            //检测并添加或获取标准id
            dpStandardManager mydpStandardManager = new dpStandardManager();
            dpStandardQuery mydpStandardQuery = new dpStandardQuery();
            mydpStandardQuery.Name = standard.ToLower();//转小写
            string standardid = mydpStandardManager.CheckISNull(mydpStandardQuery);//返回id
            if (standardid.Trim().Length == 0)
            {
                Hashtable ht = new Hashtable();
                ht.Add(dpStandard.Name, standard.ToLower());
                ht.Add(dpStandard.Content, "");
                standardid = SQLHelper.ExcuteProc("sp_Adddp_Standard", ht).ToString();//执行存储过程
            }
            //匹配组件和标准关联
            dpStandardMappingManager mydpStandardMappingManager = new dpStandardMappingManager();
            dpStandardMappingQuery mydpStandardMappingQuery = new dpStandardMappingQuery();
            mydpStandardMappingQuery.PartID = partid.ToString();
            mydpStandardMappingQuery.StandardID = standardid;
            string dpStandardMappingid = mydpStandardMappingManager.CheckISNull(mydpStandardMappingQuery);
            if (dpStandardMappingid.Trim().Length == 0)
            {
                Hashtable ht = new Hashtable();
                ht.Add(dpStandardMapping.StandardID, Convert.ToInt32(standardid));//标准ID
                ht.Add(dpStandardMapping.PartID, partid);//组件ID
                SQLHelper.ExcuteProc("sp_Adddp_StandardMapping", ht).ToString();//执行存储过程
            }

        }

        private void DowmLoad(string strid)
        {

            try
            {
                dpModelFileManager mydpModelFileManager = new dpModelFileManager();
                dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
                mydpModelFileQuery.ID = strid;
                DataSet myDataSet = mydpModelFileManager.Search(mydpModelFileQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    string fullPathUrl = Server.MapPath(myDataSet.Tables[0].Rows[0][dpModelFile.Location].ToString());//获取下载文件的路劲
                    System.IO.FileInfo file = new System.IO.FileInfo(fullPathUrl);

                    if (file.Exists)//判断文件是否存在
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.Buffer = false;
                        Response.AddHeader("content-disposition", "attachment;filename=" + file.Name);
                        Response.AddHeader("cintent_length", "attachment;filename=" + HttpUtility.UrlDecode(file.Name));
                        Response.AddHeader("cintent_length", file.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(file.FullName);//通过response对象，执行下载操作
                        Response.Flush();
                        Response.End();

                    }
                }


            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string id = this.hidfileid.Value;
            DowmLoad(id);
        }
    }
}