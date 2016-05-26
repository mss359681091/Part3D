using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _3DPart.DAL.BULayer;
using System.Data;
using _3DPart.DAL.BULayer.Schema;
using System.Collections;
using Part3D.models;
using log4net;
using System.Configuration;

namespace Part3D
{
    public partial class View : System.Web.UI.Page
    {
        private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["partid"] != null)
                {
                    try
                    {
                        string count = "0";
                        dpPartManager mydpPartManager = new dpPartManager();
                        dpPartQuery mydpPartQuery = new dpPartQuery();
                        mydpPartQuery.ID = Request.QueryString["partid"];
                        DataSet myDataSet = mydpPartManager.SearchSingle(mydpPartQuery);
                        if (myDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.hidid.Value = Request.QueryString["partid"];
                            this.hidclassid.Value = myDataSet.Tables[0].Rows[0][dpPart.ClassifyID].ToString();
                            dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                            dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                            mydpAdvertisementQuery.ClassifyID = myDataSet.Tables[0].Rows[0][dpPart.ClassifyID].ToString();
                            mydpAdvertisementQuery.ADPosition = "下载页";
                            DataSet ds = mydpAdvertisementManager.Search(mydpAdvertisementQuery);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.lnkad.HRef = ds.Tables[0].Rows[0][dpAdvertisement.ADLink].ToString();
                                this.imgad.Src = ds.Tables[0].Rows[0][dpAdvertisement.PicturePath].ToString();
                                this.lnkad.Visible = true;
                            }
                            else
                            {
                                this.lnkad.Visible = false;
                            }

                            this.btitle.InnerText = myDataSet.Tables[0].Rows[0][dpPart.Name].ToString();
                            this.ausername.InnerText = myDataSet.Tables[0].Rows[0][sysUser.Nickname].ToString();
                            this.acreate.InnerText = myDataSet.Tables[0].Rows[0]["CreateDate"].ToString();
                            this.imgPreview.Src = myDataSet.Tables[0].Rows[0][dpPart.Preview].ToString();
                            count = myDataSet.Tables[0].Rows[0][dpPart.Accesslog].ToString();
                        }

                        count = count == "" ? "0" : count;
                        mydpPartManager.UpdateParams(dpPart.Accesslog, (Convert.ToInt32(count) + 1).ToString(), Request.QueryString["partid"]);
                    }
                    catch (Exception ex)
                    {
                        m_log.Error(ex.Message);
                    }
                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }

            }
        }


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="strid">文件id</param>
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
                        //添加下载记录
                        Hashtable myHashtable = new Hashtable();
                        if (Session[sysUser.ID] != null)
                        {
                            myHashtable.Add(dpDownRecord.UserID, Session[sysUser.ID].ToString());
                        }
                        else
                        {
                            myHashtable.Add(dpDownRecord.UserID, "0");
                        }
                        myHashtable.Add(dpDownRecord.PartID, myDataSet.Tables[0].Rows[0][dpModelFile.PartID].ToString());
                        myHashtable.Add(dpDownRecord.IPAddress, CommonManager.GetClientIPv4Address());
                        SQLHelper.ExcuteProc("sp_DownRecord", myHashtable);//执行存储过程注册
                        //执行下载
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
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string id = this.hidfileid.Value;
            if (ChkIP())
            {
                DowmLoad(id);
            }
            else
            {
                ////如果有UpdatePanel就用如下代码调用前台js
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "Ceshi();", true);
                //如果没有就如下代码
                //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>fntip();</script>", true);
                Response.Write("<script>alert('非注册用户每天只能下载10次！')</script>");
            }
        }
        private bool ChkIP()
        {
            bool returnValue = false;
            string currentip = CommonManager.GetClientIPv4Address();
            dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
            dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
            mydpDownRecordQuery.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
            mydpDownRecordQuery.IP = currentip.Trim();
            string count = mydpDownRecordManager.SearchIP(mydpDownRecordQuery);
            if (count.Length > 0)
            {
                int defaultcount = Convert.ToInt32(ConfigurationManager.AppSettings["UploadCount"].ToString());//默认非用户可下载数量
                if (int.Parse(count) < defaultcount)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }

    }
}