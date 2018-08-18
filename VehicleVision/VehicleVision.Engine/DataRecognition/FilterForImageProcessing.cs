using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Drawing;

namespace VehicleVision.Engine.DataRecognition
{
    internal class FilterForImageProcessing : IFiltersForImageProcessing
    {
        private static int ParameterOfBrightness = 60;
        private static int ParameterOfContrast = -60;
        private static double ParameterOfGamma = 0.9;

        public Bitmap SetBrightness(Bitmap bmp)
        {
            BrightnessCorrection _brightnessFilter = new BrightnessCorrection(ParameterOfBrightness);
            _brightnessFilter.ApplyInPlace(bmp);
            return bmp;
        }
        public Bitmap SetContrast(Bitmap bmp)
        {
            ContrastCorrection _contrastFilter = new ContrastCorrection(ParameterOfContrast);
            _contrastFilter.ApplyInPlace(bmp);
            return bmp;
        }
        public Bitmap SetContrastStretch(Bitmap bmp)
        {
            ContrastStretch _contrastStretchFilter = new ContrastStretch();
            _contrastStretchFilter.ApplyInPlace(bmp);
            return bmp;
        }
        public Bitmap SetGammaCorrection(Bitmap bmp)
        {
            GammaCorrection _setGammaCorrection = new GammaCorrection(ParameterOfGamma);
            _setGammaCorrection.ApplyInPlace(bmp);
            return bmp;
        }
        public Bitmap SetNormalizedRGBChannel(Bitmap bmp)
        {
            ExtractNormalizedRGBChannel _normalizedRGB = new ExtractNormalizedRGBChannel(RGB.G);
            _normalizedRGB.Apply(bmp);
            return bmp;
        }
        public Bitmap SetSharpenFilter(Bitmap bmp)
        {
            Sharpen _sharpenFilter = new Sharpen();
            _sharpenFilter.Apply(bmp);
            return bmp;
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
