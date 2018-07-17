using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace CRM11.UI
{
    /// <summary>
    /// 将随机生成的验证码存入Session并返回对应的验证码图片
    /// </summary>
    public class Vcode : IHttpHandler, IRequiresSessionState //继承IRequiresSessionState接口表示此一般处理程序可以使用Session
    {
        public const string VcodeName = "vcode";

        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            string VcodeStr = GetVcodeStr();
            context.Session[VcodeName] = VcodeStr;
            GetVcodeImg(VcodeStr);

        }

        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <returns></returns>
        private string GetVcodeStr()
        {
            string[] resourceVcodes = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            Random r = new Random();

            StringBuilder sbVcodeStr = new StringBuilder(5);
            for (int i = 0; i < 5; i++)
            {
                sbVcodeStr.Append(resourceVcodes[r.Next(resourceVcodes.Length - 1)]);
            }

            return sbVcodeStr.ToString();
        }

        public void GetVcodeImg(string vcodeStr)
        {
            using (Bitmap img = new Bitmap((int)(vcodeStr.Length * 15.5), 25))
            {
                Graphics g = Graphics.FromImage(img);
                g.Clear(Color.White);

                //画图片的背景噪音线
                Random r = new Random();
                for (int i = 0; i < 30; i++)
                {
                    int x1 = r.Next(img.Width);
                    int x2 = r.Next(img.Width);
                    int y1 = r.Next(img.Height);
                    int y2 = r.Next(img.Height);

                    g.DrawLine(Pens.Silver, x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);

                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.DarkRed, 1.2f, true);

                g.DrawString(vcodeStr, font, brush, 2, 2); //将验证码字符串画入图片

                //画图片的前置噪点

                for (int i = 0; i < 100; i++)
                {
                    int x = r.Next(img.Width);
                    int y = r.Next(img.Height);

                    img.SetPixel(x, y, Color.FromArgb(r.Next()));
                }

                //画验证码的边框
                g.DrawRectangle(Pens.Silver, 0, 0, img.Width - 1, img.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif); //将画好的图片存入流中

                context.Response.ClearContent();
                context.Response.ContentType = "image/Gif";
                context.Response.BinaryWrite(ms.ToArray());

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