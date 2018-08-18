using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace VehicleVision.Engine.DataRecognition
{
    internal static class Interpolation
    {
        static public Bitmap ScaleByPercent(Bitmap imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);
            var bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                  imgPhoto.VerticalResolution);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                              new Rectangle(0, 0, destWidth, destHeight),
                              new Rectangle(0, 0, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);
            string fileName = String.Format(@"{0}.txt", System.Guid.NewGuid());
            bmPhoto.Save(Directory.GetCurrentDirectory() + "/userdata/images/" + fileName + ".png", ImageFormat.Png);
            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
