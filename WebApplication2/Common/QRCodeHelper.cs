using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Common
{
    public class QRCodeHelper
    {
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="str">地址</param>
        /// <returns></returns>
        public Bitmap CreateQRimg(string address)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(address, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap bt = qrCode.GetGraphic(20);
            return bt;
        }

        /// <summary>
        /// 生成二维码图片流（二维码上含文字）
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="content">文字内容</param>
        /// <returns></returns>
        public byte[] GenerateQRCode(string address, string content)
        {
            using (Image codeImage = CreateQRimg(address), strImage = ConvertStringToImage(content))
            {
                Image img = CombineImage(600, 600, codeImage, 60, 50, strImage, 0, 530);
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, ImageFormat.Jpeg);
                    //输出图片流
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// 生成文字图片
        /// </summary>
        /// <param name="context">文字内容</param>
        /// <returns></returns>
        public Image ConvertStringToImage(string context)
        {
            Bitmap image = new Bitmap(600, 40, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Font font = new Font("SimHei", 14, FontStyle.Regular);
                g.Clear(Color.White);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Rectangle rectangle = new Rectangle(0, 0, 600, 40);
                g.DrawString(context, font, new SolidBrush(Color.Black), rectangle, format);
                return image;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// 在画板中合并二维码图片和文字图片
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imgLeft"></param>
        /// <param name="imgLeft_left"></param>
        /// <param name="imgLeft_top"></param>
        /// <param name="imgRight"></param>
        /// <param name="imgRight_left"></param>
        /// <param name="imgRight_top"></param>
        /// <returns></returns>
        public Image CombineImage(int width, int height, Image imgLeft, int imgLeft_left, int imgLeft_top, Image imgRight, int imgRight_left, int imgRight_top)
        {
            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);
            try
            {
                g.Clear(Color.White);
                g.DrawImage(imgLeft, imgLeft_left, imgLeft_top, 500, 500);
                g.DrawImage(imgRight, imgRight_left, imgRight_top, imgRight.Width, imgRight.Height);
                return image;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                g.Dispose();
            }
        }
    }
}