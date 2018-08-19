using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Drawing;

namespace VehicleVision.Engine.DataRecognition
{
    internal class FilterForImageProcessing : IFiltersForImageProcessing
    {
        private static int ParameterOfBrightness = 60; //100
        private static int ParameterOfContrast = -60; //-100
        private static double ParameterOfGamma = 1;

        public Bitmap SetBrightness(Bitmap bmp)
        {
            BrightnessCorrection _brightnessFilter = new BrightnessCorrection(ParameterOfBrightness);
            return _brightnessFilter.Apply(bmp);
        }
        public Bitmap SetContrast(Bitmap bmp)
        {
            ContrastCorrection _contrastFilter = new ContrastCorrection(ParameterOfContrast);
            return _contrastFilter.Apply(bmp);
        }
        public Bitmap SetContrastStretch(Bitmap bmp)
        {
            ContrastStretch _contrastStretchFilter = new ContrastStretch();
            return _contrastStretchFilter.Apply(bmp);
        }
        public Bitmap SetGammaCorrection(Bitmap bmp)
        {
            GammaCorrection _setGammaCorrection = new GammaCorrection(ParameterOfGamma);
            return _setGammaCorrection.Apply(bmp);
        }
        public Bitmap SetNormalizedRGBChannel(Bitmap bmp)
        {
            ExtractNormalizedRGBChannel _normalizedRGB_G = new ExtractNormalizedRGBChannel(RGB.G);
            _normalizedRGB_G.Apply(bmp);
          //  ExtractNormalizedRGBChannel _normalizedRGB_R = new ExtractNormalizedRGBChannel(RGB.R);
          //  _normalizedRGB_R.Apply(bmp);
          //  ExtractNormalizedRGBChannel _normalizedRGB_B = new ExtractNormalizedRGBChannel(RGB.B);
          //  _normalizedRGB_B.Apply(bmp);
            return bmp;
        }
        public Bitmap SetSharpenFilter(Bitmap bmp)
        {
            Sharpen _sharpenFilter = new Sharpen();
            return _sharpenFilter.Apply(bmp);
        }
        public Bitmap SetGrayScale(Bitmap bmpPhoto)
        {
            for (int y = 0; y < bmpPhoto.Height; y++)
            {
                for (int x = 0; x < bmpPhoto.Width; x++)
                {
                    Color color = bmpPhoto.GetPixel(x, y);
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    int avg = (r + g + b) / 3;
                    bmpPhoto.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            return bmpPhoto;
        }
    }
}
