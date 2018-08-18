using System.Drawing;

namespace VehicleVision.Engine.DataRecognition
{
    internal class AverageOfBrightness
    {
        public static int AVGofBrightness;
        public static int GetAVGofBrightness(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    int r = bmp.GetPixel(j, i).R;
                    int g = bmp.GetPixel(j, i).G;
                    int b = bmp.GetPixel(j, i).B;
                    if (r + g + b != 0)
                    {
                        int temp = (r + g + b) / 3;
                        AVGofBrightness = temp;
                    }
                }
            return AVGofBrightness;
        }
    }
}
