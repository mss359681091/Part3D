using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class WebUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[sysUser.Username] == null)
                {
                    Response.Redirect("/Login.aspx");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strPartname = this.txtPartname.Value;
            string strClassifyID = this.hidClassfiyID.Value;
            string uploadPath = Server.MapPath(@"/user/jqUploadify/uploads/f/");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string filePath = btnfile.PostedFile.FileName;//文件路径
            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);//文件名称
            string fileExtname = System.IO.Path.GetExtension(filePath).ToLower();//文件扩展名
            string ran = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999);
            string temp = uploadPath + ran;
            string newfilename = temp + fileExtname; //新文件名称
            string newfile_s_name = temp + "_s" + fileExtname;//新文件名称（缩略图）
            this.btnfile.PostedFile.SaveAs(newfilename);//上传文件
            CommonManager.GetThumbnail(this.btnfile, newfile_s_name, 240, 170);//生成缩略图
            this.imghead.Src = @"/user/jqUploadify/uploads/f/" + ran + "_s" + fileExtname;
            this.addpic.Attributes.Add("style", "display:none");
            this.preview.Attributes.Add("style", "");
        }


    }
}