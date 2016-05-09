/******************************************************************
** Copyright (c) 2012 -2050 成瑞软件技术部
** 创建人: 李赛赛
** 创建日期:2013-06-01
** 描 述: 邮件发送类
** 版 本:1.0
**-----------------------------------------------------------------
********************************************************************/
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Part3D.models
{
    public class SendEmailUtility
    {
        /// <summary>
        /// 发送邮箱设置
        /// </summary>
        /// <param name="fromMail">邮件发送源</param>
        /// <param name="toMail">接收者</param>
        /// <param name="subject">邮件头</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachmentFiles">附件</param>
        /// <param name="body">显示名称</param>
        /// <returns></returns>
        private static MailMessage EmailCompose(string fromMail, List<string> toMail, string subject, string body, string attachmentFiles)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail, "3DPart官网", Encoding);
            //邮件群发
            foreach (string str in toMail)
            {
                message.Bcc.Add(str);
            }
            message.Body = body.Length > 0 ? body : "3DPart官网";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding;

            if (attachmentFiles.Length > 0)
            {
                string[] attachmentCollection = attachmentFiles.Split(',');
                for (int i = 0; i < attachmentCollection.Length; i++)
                {
                    if (attachmentCollection[i] != null && File.Exists(attachmentCollection[i]))
                        message.Attachments.Add(new Attachment(attachmentCollection[i]));
                }
            }
            return message;

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailServer">邮箱类型</param>
        /// <param name="userName">邮箱帐号</param>
        /// <param name="passWord">邮箱密码</param>
        /// <param name="fromMail">邮件发送源</param>
        /// <param name="toMail">接收者</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachmentFiles">附件</param>
        /// <returns></returns>
        public static bool SendEmail(List<string> toMail, string subject, string body, string attachmentFiles)
        {
            string mailServer = string.Empty;
            string userName = string.Empty;
            string passWord = string.Empty;
            string fromMail = string.Empty;
            string strSiteName = string.Empty;

            //string[] list = { CMS_Mail.Port, CMS_Mail.Host, CMS_Mail.Username, CMS_Mail.Password };


            mailServer = "smtp.sina.com";
            fromMail = "mss2129877@sina.com";
            userName = "mss2129877";
            passWord = "mss13966103580";

            try
            {
                MailMessage message;
                message = EmailCompose(fromMail, toMail, subject, body, attachmentFiles);
                SmtpClient client = new SmtpClient(mailServer);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //表示以当前登录用户的默认凭据进行身份验证
                client.UseDefaultCredentials = false;
                //获取或设置一个值，该值指示电子邮件正文是否为  HTML。 
                message.IsBodyHtml = true;
                if (userName.Length > 0)
                {
                    client.Credentials = new System.Net.NetworkCredential(userName, passWord);
                }
                client.Send(message);
                message.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Encoding Encoding
        {
            get { return System.Text.Encoding.GetEncoding("gb2312"); }
        }
    }
}


