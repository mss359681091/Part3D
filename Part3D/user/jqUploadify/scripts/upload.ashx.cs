using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.SessionState;
using System.IO;

namespace jqUploadify.scripts
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class upload : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = context.Server.MapPath("..\\uploads\\");
            string uploadPaths = context.Server.MapPath("..\\uploads\\s\\");
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (!Directory.Exists(uploadPaths))
                {
                    Directory.CreateDirectory(uploadPaths);
                }
                //string ran = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999);
                string ran = "_" + new Random().Next(100, 999).ToString();
                string fileExtname = System.IO.Path.GetExtension(uploadPath + file.FileName).ToLower();//文件扩展名
                string fileName = System.IO.Path.GetFileNameWithoutExtension(uploadPath + file.FileName).ToLower();//文件扩展名
                file.SaveAs(uploadPath + fileName.Replace(" ", "") + ran + fileExtname);
                //生成缩略图
                // MakeThumbnail(uploadPath + ran + fileExtname, uploadPath + "\\s\\" + ran + fileExtname, 240, 170);
                if (HttpContext.Current.Session["modefile"] != null)
                {
                    HttpContext.Current.Session["modefile"] += fileName + ";" + fileExtname + ";" + @"/user/jqUploadify/uploads/" + fileName.Replace(" ", "") + ran + fileExtname + ";" + file.ContentLength + ",";
                }
                else
                {
                    HttpContext.Current.Session["modefile"] = fileName + ";" + fileExtname + ";" + @"/user/jqUploadify/uploads/" + fileName.Replace(" ", "") + ran + fileExtname + ";" + file.ContentLength + ",";
                }
            }
        }

        private void MakeThumbnail(string sourcePath, string newPath, int width, int height)
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
