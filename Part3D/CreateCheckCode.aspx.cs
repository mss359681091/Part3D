using System;
using System.IO;


namespace Part3D
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public partial class CreateCheckCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CreateCheckCodeImage();
            }
        }

        private void CreateCheckCodeImage()
        {
            string[] Fonts = new string[] { "Microsoft Sans Serif", "Comic Sans MS", "Arial",
                                            "Verdana", "Courier New", "Arial", "黑体", "宋体" };
            Random Rand = new Random();
            string strCheckCode = GetRandomString(4);

            Bitmap Img = new Bitmap(86, 29);
            Graphics g = Graphics.FromImage(Img);//生成Graphics   
            Color bgColor = Color.FromArgb(Rand.Next(224, 256), Rand.Next(224, 256), Rand.Next(224, 256));//背景色
            g.Clear(bgColor);

            drawPoint(Img, Rand, 100);//背景噪点,100个
            for (int i = 0; i < strCheckCode.Length; i++)
            {
                //扭曲
                Matrix X = new Matrix();
                X.Shear((float)Rand.Next(0, 300) / 1000 - 0.25f, (float)Rand.Next(0, 100) / 1000 - 0.05f);
                g.Transform = X;

                string tempchar = strCheckCode[i].ToString();
                Brush b = new System.Drawing.SolidBrush(GetRandomColor(Rand));//颜色  
                Point p = new Point(i * 20 + Rand.Next(3), 1 + Rand.Next(4));//位置
                Font f = new Font(Fonts[Rand.Next(Fonts.Length - 1)], Rand.Next(15, 17), FontStyle.Bold);//字体
                //绘制
                g.DrawString(tempchar, f, b, p);
            }
            drawLine(g, Img, Rand, 10);//前景线条,10条
            drawPoint(Img, Rand, 50);//前景噪点,50个
            //输出
            Session["CheckCode"] = strCheckCode;//验证码存储在Session中，供验证。
            MemoryStream ms = new MemoryStream();
            Img.Save(ms, ImageFormat.Png);
            Response.ClearContent();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            Img.Dispose();
            Response.End();
        }
        //生成随机线条
        private void drawLine(Graphics g, Bitmap img, Random ran, int lineCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                int x1 = ran.Next(img.Width);
                int y1 = ran.Next(img.Height);
                int x2 = ran.Next(img.Width);
                int y2 = ran.Next(img.Height);
                g.DrawLine(new Pen(Color.FromArgb(ran.Next())), x1, y1, x2, y2);
            }
        }
        //生成随机噪点
        private void drawPoint(Bitmap img, Random ran, int pointCount)
        {
            for (int i = 0; i < pointCount; i++)
            {
                int x = ran.Next(img.Width);
                int y = ran.Next(img.Height);
                img.SetPixel(x, y, Color.FromArgb(ran.Next()));
            }

        }
        //获取随机字符串
        private string GetRandomString(int strLength)
        {
            string codeChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            string valString = string.Empty;
            Random theRandomNumber = new Random((int)DateTime.Now.Ticks);

            while (valString.Length < strLength)
            {
                char tempChar = codeChars[theRandomNumber.Next(codeChars.Length - 1)];
                if (valString.IndexOf(tempChar) >= 0) continue; //用于保证出现4个不同字符
                else valString += tempChar;
            }

            return valString;
        }
        //获取随机颜色
        private Color GetRandomColor(Random RndA)
        {
            Random RndB = new Random(RndA.Next(256));

            int[] intColor = new int[3];
            intColor[0] = RndA.Next(256);
            intColor[1] = RndB.Next(256);

            intColor[2] = (intColor[0] + intColor[1] > 400) ? 0 : 400 - intColor[0] - intColor[1];
            intColor[2] = (intColor[2] > 255) ? 255 : intColor[2];

            int Rand1 = RndA.Next(0, 3);
            int Rand2 = Rand1 == 0 ? RndB.Next(1, 3) : 0;
            int Rand3 = 3 - Rand1 - Rand2;

            return Color.FromArgb(intColor[Rand1], intColor[Rand2], intColor[Rand3]);
        }
    }
}