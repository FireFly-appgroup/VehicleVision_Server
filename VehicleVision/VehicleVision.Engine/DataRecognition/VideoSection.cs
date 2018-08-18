using System.Drawing;
using System.Drawing.Drawing2D;

namespace VehicleVision.Engine.DataRecognition
{
    internal class VideoSection : IVideoSection
    {
        public Image CutSection(Image srcBitmap, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);
                g.SmoothingMode = SmoothingMode.HighQuality;
            }
            return bmp;
        }
        public Image CutNumber(Image srcBitmap, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width - 4, section.Height - 8);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(srcBitmap, -2, -4, section, GraphicsUnit.Pixel);
                g.SmoothingMode = SmoothingMode.HighQuality;
            }
            return bmp;
        }
    }
}
