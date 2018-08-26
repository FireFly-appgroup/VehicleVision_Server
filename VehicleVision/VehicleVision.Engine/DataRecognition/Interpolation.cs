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
            var bmpPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmpPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                  imgPhoto.VerticalResolution);
            Graphics grPhoto = Graphics.FromImage(bmpPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                              new Rectangle(0, 0, destWidth, destHeight),
                              new Rectangle(0, 0, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);
            string fileName = String.Format(@"{0}.txt", System.Guid.NewGuid());
            RemoveNoise(bmpPhoto);

            bmpPhoto.Save(Directory.GetCurrentDirectory() + "/userdata/images/" + fileName + ".png", ImageFormat.Png);
            grPhoto.Dispose();
            return bmpPhoto;
        }
        public static Bitmap RemoveNoise(Bitmap bmap)
        {
            for (var x = 0; x < bmap.Width; x++)
            {
                for (var y = 0; y < bmap.Height; y++)
                {
                    var pixel = bmap.GetPixel(x, y);
                    if (pixel.R < 162 && pixel.G < 162 && pixel.B < 162)
                        bmap.SetPixel(x, y, Color.Black);
                    else if (pixel.R > 162 && pixel.G > 162 && pixel.B > 162)
                        bmap.SetPixel(x, y, Color.White);
                }
            }
            return bmap;
        }
    }
}
