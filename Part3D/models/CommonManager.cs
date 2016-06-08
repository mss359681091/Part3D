using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml;

namespace Part3D.models
{
    public class CommonManager
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="paramUsername">用户名</param>
        /// <param name="paramPassword">密码</param>
        /// <param name="paramRemember">是否记住密码</param>
        /// <returns></returns>
        public static string LoginValidate(string paramUsername, string paramPassword, string paramRemember)
        {
            string returnValue = string.Empty;
            DataSet myDataSet = new DataSet();
            sysUserManager mysysUserManager = new sysUserManager();
            sysUserQuery mysysUserQuery = new sysUserQuery();
            mysysUserQuery.Username = paramUsername;
            mysysUserQuery.Password = paramPassword;
            myDataSet = mysysUserManager.DSLogin(mysysUserQuery);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                FnAuthentication(paramUsername, myDataSet.Tables[0].Rows[0]["Rolename"].ToString());//登录授权
                //记录登录状态
                HttpContext.Current.Session[sysUser.ID] = myDataSet.Tables[0].Rows[0][sysUser.ID].ToString();//用户ID
                HttpContext.Current.Session[sysUser.Username] = myDataSet.Tables[0].Rows[0][sysUser.Username].ToString();//用户名
                HttpContext.Current.Session["Rolename"] = myDataSet.Tables[0].Rows[0]["Rolename"].ToString();//角色名
                HttpContext.Current.Session[sysUser.Email] = myDataSet.Tables[0].Rows[0][sysUser.Email].ToString();//邮箱
                HttpContext.Current.Session[sysUser.Mobile] = myDataSet.Tables[0].Rows[0][sysUser.Mobile].ToString();//手机号
                HttpContext.Current.Session[sysUser.Photo] = myDataSet.Tables[0].Rows[0][sysUser.Photo].ToString();//头像
                HttpContext.Current.Session[sysUser.Password] = myDataSet.Tables[0].Rows[0][sysUser.Password].ToString();//密码
                HttpContext.Current.Session[sysUser.Nickname] = myDataSet.Tables[0].Rows[0][sysUser.Nickname].ToString();//昵称
                //记住密码
                if (paramRemember == "true")
                {
                    if (HttpContext.Current.Request.Cookies["username"] != null && HttpContext.Current.Request.Cookies["password"] != null)
                    {
                        HttpContext.Current.Response.Cookies["username"].Expires = System.DateTime.Now.AddSeconds(-1);//Expires过期时间
                        HttpContext.Current.Response.Cookies["password"].Expires = System.DateTime.Now.AddSeconds(-1);
                    }
                    HttpCookie hcUserName1 = new HttpCookie("username");
                    hcUserName1.Expires = System.DateTime.Now.AddDays(7);
                    hcUserName1.Value = myDataSet.Tables[0].Rows[0][sysUser.Username].ToString();//用户名
                    HttpCookie hcPassword1 = new HttpCookie("password");
                    hcPassword1.Expires = System.DateTime.Now.AddDays(7);
                    hcPassword1.Value = myDataSet.Tables[0].Rows[0][sysUser.Password].ToString();//密码
                    HttpContext.Current.Response.Cookies.Add(hcUserName1);
                    HttpContext.Current.Response.Cookies.Add(hcPassword1);
                }

                returnValue = "1";//登录成功
            }
            else
            {
                returnValue = "-3";//状态-3：用户名密码错误
            }

            return returnValue;
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="rolename">角色名</param>
        public static void FnAuthentication(string username, string rolename)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), true, rolename, FormsAuthentication.FormsCookiePath);
            string strticket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strticket);
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #region  将dataTable数据转化为List<>
        /// <summary>
        /// 将dataTable数据转化为List<>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(DataTable table)
        {
            IList<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName.ToUpper()))
                    {
                        //object value = row[tempName];
                        //pro.SetValue(t, value, null);

                        // 判断此属性是否有Setter  
                        if (!pro.CanWrite) continue;//该属性不可写，直接跳出  
                        //取值  
                        object value = row[tempName];
                        //如果非空，则赋给对象的属性  
                        if (value != DBNull.Value)
                            pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        #endregion

        #region 按字节截取字符串 李赛赛 2013/06/26

        #region 判断是否包含中文
        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="RegType">true:包含中文;false:全部为中文</param>
        /// <returns></returns>
        public static bool IsChinese(string chars, bool RegType)
        {
            if (RegType)
            {
                return Regex.IsMatch(chars, @"^([\u4e00-\u9fa5]|[\uff01-\uff60]|\u3000){1,}$");
            }
            return Regex.IsMatch(chars, @"([\u4e00-\u9fa5]|[\uff01-\uff60]|\u3000){1,}");
        }
        #endregion
        /// <summary>
        /// 字符串截取（按字节）
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string bSubstring(string s, int length)
        {
            string str = "";
            if (Encoding.GetEncoding("GB2312").GetBytes(s).Length < length)
            {
                return s;
            }
            if (!IsChinese(s, false))
            {
                return s.Substring(0, length);
            }
            if (IsChinese(s, true))
            {
                return s.Substring(0, length / 2);
            }
            int num = length / 2;
            int num2 = length;
            while (true)
            {
                str = str + s.Substring(str.Length, num);
                num2 = length - Encoding.GetEncoding("GB2312").GetBytes(str).Length;
                if (num2 <= 1)
                {
                    if ((num2 == 1) && (Encoding.GetEncoding("GB2312").GetBytes(s.Substring(str.Length, 1)).Length == 1))
                    {
                        str = str + s.Substring(str.Length, 1);
                    }
                    return str;
                }
                num = num2 / 2;
            }
        }
        /// <summary>
        /// 截取指定长度的字节数，并在末尾加上指定字符，比如 “...”
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <param name="p_TailString"></param>
        /// <returns></returns>
        public static string bSubstring(string s, int length, string p_TailString)
        {
            string str = bSubstring(s, length);
            if (Encoding.GetEncoding("GB2312").GetBytes(s).Length > length)
            {
                str = str + p_TailString;
            }
            return str;
        }
        #endregion

        #region public static string FilterHtmlTarget(string paramHtml) 过滤HTML元素
        public static string FilterHtmlTarget(string paramHtml)
        {
            //删除脚本
            paramHtml = Regex.Replace(paramHtml, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"<style[^>]*?>[\s\S]*?<\/style>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            paramHtml = Regex.Replace(paramHtml, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"-->", "", RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            paramHtml = Regex.Replace(paramHtml, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            paramHtml.Replace("<", "");
            paramHtml.Replace(">", "");
            paramHtml.Replace("\r\n", "");
            paramHtml = HttpContext.Current.Server.HtmlEncode(paramHtml).Trim();
            return paramHtml;
        }
        #endregion

        #region public void GetThumbnail(FileUpload paramFileUpload, string paramFilePath, int tWidth, int tHeight) 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="paramFileUpload"></param>
        /// <param name="paramFilePath"></param>
        /// <param name="tWidth"></param>
        /// <param name="tHeight"></param>
        public static void GetThumbnail(FileUpload paramFileUpload, string paramFilePath, int tWidth, int tHeight)
        {
            try
            {
                //生成原图
                byte[] oFileByte = new byte[paramFileUpload.PostedFile.ContentLength];
                Stream oStream = paramFileUpload.PostedFile.InputStream;
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
                int oWidth = oImage.Width;//原图宽度
                int oHeight = oImage.Height;//原图高度
                if (tWidth == 0)
                {
                    tWidth = 120;
                }
                if (tHeight == 0)
                {
                    tHeight = 120;
                }
                if (tWidth > oWidth)
                {
                    tWidth = oWidth;
                }
                if (tHeight > oHeight)
                {
                    tHeight = oHeight;
                }
                //按比例计算出缩略图的宽度和高度
                if (oWidth >= oHeight)
                {
                    tHeight = (int)Math.Floor(Convert.ToDouble(tWidth) * Convert.ToDouble(oHeight) / Convert.ToDouble(oWidth));
                }
                else
                {
                    tWidth = (int)Math.Floor(Convert.ToDouble(tHeight) * Convert.ToDouble(oWidth) / Convert.ToDouble(oHeight));
                }
                //生成缩略图
                Bitmap tImage = new Bitmap(tWidth, tHeight);
                Graphics g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;//设置高质量插值法
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量，低速度呈现平滑程度
                g.Clear(Color.Transparent);//清空画布并以透明背景色填充
                g.DrawImage(oImage, new Rectangle(0, 0, tWidth, tHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);
                tImage.Save(paramFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //释放资源                    
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region public string GetClientIPv4Address() 取得客户端IPv4地址方法
        protected static string ClientIP
        {
            get
            {
                string ip = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
                if (ip == null || ip.Length == 0)
                {
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (ip == null || ip.Length == 0)
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (ip == null || ip.Length == 0)
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
                return ip;
            }
        }
        //win7以后的系统包含了ipv6,筛选以取得ipv4地址
        public static string GetClientIPv4Address()
        {
            string ipv4 = String.Empty;

            foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostAddresses(ClientIP))
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }

            if (ipv4 != String.Empty)
            {
                return ipv4;
            }

            // 利用 Dns.GetHostEntry 方法，由获取的 IPv6 位址反查 DNS 纪录，
            // 再逐一判断何者为 IPv4 协议，即可转为 IPv4 位址。
            foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostEntry(ClientIP).AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }

            return ipv4;
        }
        #endregion

        #region 修改config文件  

        /// <summary>  

        /// 修改config文件(AppSetting节点)  

        /// </summary>  

        /// <param name="key">键</param>  

        /// <param name="value">要修改成的值</param>  

        public static void UpdateAppSetting(string key, string value)

        {

            XmlDocument doc = new XmlDocument();

            //获得配置文件的全路径   

            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log4Net.config";

            doc.Load(strFileName);

            //找出名称为“add”的所有元素   

            XmlNodeList nodes = doc.GetElementsByTagName("param");

            for (int i = 0; i < nodes.Count; i++)

            {

                //获得将当前元素的key属性   

                XmlAttribute _key = nodes[i].Attributes["name"];

                //根据元素的第一个属性来判断当前的元素是不是目标元素   

                if (_key != null)

                {

                    if (_key.Value == key)

                    {

                        //对目标元素中的第二个属性赋值   

                        _key = nodes[i].Attributes["value"];



                        _key.Value = value;

                        break;

                    }

                }

            }

            //保存上面的修改   

            doc.Save(strFileName);

        }



        /// <summary>  

        /// 修改config文件(ConnectionString节点)  

        /// </summary>  

        /// <param name="name">键</param>  

        /// <param name="value">要修改成的值</param>  

        public static void UpdateConnectionString(string name, string value)

        {

            XmlDocument doc = new XmlDocument();

            //获得配置文件的全路径   

            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Web.config";

            doc.Load(strFileName);

            //找出名称为“add”的所有元素   

            XmlNodeList nodes = doc.GetElementsByTagName("add");

            for (int i = 0; i < nodes.Count; i++)

            {

                //获得将当前元素的key属性   

                XmlAttribute _name = nodes[i].Attributes["name"];

                //根据元素的第一个属性来判断当前的元素是不是目标元素   

                if (_name != null)

                {

                    if (_name.Value == name)

                    {

                        //对目标元素中的第二个属性赋值   

                        _name = nodes[i].Attributes["connectionString"];



                        _name.Value = value;

                        break;

                    }

                }

            }

            //保存上面的修改   

            doc.Save(strFileName);

        }

        #endregion

        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootURI()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;

                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在   Web   站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }

        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public static string GetRootURI(HttpRequest Req)
        {
            string AppPath = "";
            if (Req != null)
            {
                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在   Web   站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }

        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            if (HttpCurrent != null)
            {
                AppPath = HttpCurrent.Server.MapPath("~");
            }
            else
            {
                AppPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                    AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }
            return AppPath;
        }

    }
}