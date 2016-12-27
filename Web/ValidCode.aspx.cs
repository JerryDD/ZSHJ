using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class public_ValidCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 生成附加码
        string attacheCode = CreateRandomCode(4);
        CreateImage(attacheCode);
        string urlRef = Convert.ToString(Request.UrlReferrer);
        //判断是否非法
        /*
        if (urlRef == null || urlRef.Trim().Length == 0 || urlRef.ToLower().IndexOf("validcode.aspx") != -1)
        {
            return;
        }
         */
        Session[GlobalKeys.KEY_VALID_CODE] = attacheCode;
    }

    /// <summary>
    /// 生成附加码内容
    /// </summary>
    /// <param name="codeCount"></param>
    /// <returns></returns>
    private string CreateRandomCode(int codeCount)
    {

        char[] allCharArray = { '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y', 'Z' };
        string randomCode = "";
        int len = allCharArray.Length;
        Random rand = new Random();
        int ind = -1;
        for (int i = 0; i < codeCount; i++)
        {
            rand = new Random(ind * (int)DateTime.Now.Ticks);
            ind = rand.Next(len - 1);
            while (randomCode.IndexOf(allCharArray[ind]) != -1)
                ind = rand.Next(len - 1);
            randomCode += allCharArray[ind];
        }
        return randomCode;
    }

    /// <summary>
    /// 画图
    /// </summary>
    /// <param name="checkCode"></param>
    private void CreateImage(string checkCode)
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        //随机字体
        string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

        int iwidth = (int)(checkCode.Length * 25 + 50);
        int iHeigh = (int)(50);

        System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, iHeigh);
        Graphics g = Graphics.FromImage(image);
        //背景填充为白色        
        Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
        g.FillRectangle(new SolidBrush(Color.White), rect);

        int codeLen = checkCode.Length;
        float angle = 0;
        for (int i = 0; i < codeLen; i++)
        {
            Random randt = new Random((i + 1) * (int)DateTime.Now.Ticks);
            /*Brush b = null;            
            //随机颜色          
            Color color = Color.FromArgb((byte)randt.Next(102, 210),
                (byte)randt.Next(102, 210), (byte)randt.Next(102, 210));
            b = new System.Drawing.SolidBrush(color);            
            */
            Color col3 = Color.FromArgb((byte)randt.Next(20, 40),
               (byte)randt.Next(70, 90), (byte)randt.Next(102, 254));
            Color col4 = Color.FromArgb((byte)randt.Next(102, 210),
                (byte)randt.Next(102, 254), (byte)randt.Next(102, 254));
            LinearGradientBrush b = new LinearGradientBrush(rect, col3, col4,
            LinearGradientMode.Vertical);
            //随机字体
            string formName = font[randt.Next(5)];
            int size = randt.Next(15, 25);
            Font f = new System.Drawing.Font(formName, size, System.Drawing.FontStyle.Bold);

            //随机角度
            angle = randt.Next(-5, 5);
            g.TranslateTransform(2, 5);
            g.RotateTransform(angle);
            g.DrawString("" + checkCode[i], f, b, 20 + i * 20, 5);
            g.RotateTransform(-1 * angle);
            g.TranslateTransform(2, -5);
        }

        //画干扰线
        for (int i = 0; i < 2; i++)
        {
            Brush lb = null;
            Color col = Color.FromArgb((byte)random.Next(150, 210),
                    (byte)random.Next(150, 210), (byte)random.Next(150, 210));
            lb = new System.Drawing.SolidBrush(col);
            Pen pen = new Pen(lb, 2);

            g.DrawLine(pen, random.Next(iwidth), random.Next(iHeigh), random.Next(iwidth), random.Next(iHeigh));
        }
        //扭曲        
        image = TwistImage(image, true, random.Next(3, 7), random.Next(2, 8));
        //输出
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;
        myEncoder = Encoder.Quality;
        ImageCodecInfo myImageCodecInfo;
        myEncoderParameters = new EncoderParameters(1);
        myEncoderParameter = new EncoderParameter(myEncoder, 95L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        myImageCodecInfo = GetEncoderInfo("image/jpeg");
        image.Save(ms, myImageCodecInfo, myEncoderParameters);
        Response.ClearContent();
        Response.ContentType = "image/Jpeg";
        Response.BinaryWrite(ms.ToArray());


        g.Dispose();
        image.Dispose();
    }

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }


    /// <summary>
    /// 正弦曲线Wave扭曲图片
    /// </summary>
    /// <param name="srcBmp"></param>
    /// <param name="bXDir"></param>
    /// <param name="nMultValue">波形的幅度倍数</param>
    /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
    /// <returns></returns>
    private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
    {
        double PI = 6.283185307179586476925286766559;
        Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

        // 将位图背景填充为随机颜色
        Random random = new Random();
        Color col1 = Color.FromArgb((byte)random.Next(220, 250),
            (byte)random.Next(220, 250), (byte)random.Next(220, 250));
        Color col2 = Color.FromArgb((byte)random.Next(220, 250),
                (byte)random.Next(220, 250), (byte)random.Next(220, 250));
        Rectangle rect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
        LinearGradientBrush lBrush = new LinearGradientBrush(rect, col1, col2,
        LinearGradientMode.Horizontal);
        Graphics graph = Graphics.FromImage(destBmp);
        graph.FillRectangle(lBrush, 0, 0, destBmp.Width, destBmp.Height);
        graph.Dispose();

        double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

        for (int i = 0; i < destBmp.Width; i++)
        {
            for (int j = 0; j < destBmp.Height; j++)
            {
                double dx = 0;
                dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                dx += dPhase;
                double dy = Math.Sin(dx);

                // 取得当前点的颜色
                int nOldX = 0, nOldY = 0;
                nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                Color color = srcBmp.GetPixel(i, j);

                if (nOldX >= 0 && nOldX < destBmp.Width
                 && nOldY >= 0 && nOldY < destBmp.Height)
                {
                    if (!(color.R == 255 && color.G == 255 && color.B == 255))
                        destBmp.SetPixel(nOldX, nOldY, color);
                }
            }
        }
        srcBmp.Dispose();
        return destBmp;
    }
}
