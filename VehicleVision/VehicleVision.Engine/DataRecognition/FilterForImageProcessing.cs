using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace VehicleVision.Engine.DataRecognition
{
    internal class FilterForImageProcessing : IFiltersForImageProcessing
    {
        private static int ParameterOfBrightness = 25; //100 //60
        private static int ParameterOfContrast = -50; //-100 //-60
        private static double ParameterOfGamma = 1.5; //0.9

        public Bitmap SetBrightnessCorrection(Bitmap vehicleNumberImage)
        {
            BrightnessCorrection filter = new BrightnessCorrection(ParameterOfBrightness);
            filter.ApplyInPlace(vehicleNumberImage);
            return vehicleNumberImage;
        }

        public Bitmap SetContrast(Bitmap vehicleNumberImage)
        {
            ContrastCorrection _contrastFilter = new ContrastCorrection(ParameterOfContrast);
            _contrastFilter.ApplyInPlace(vehicleNumberImage);
            return vehicleNumberImage;
        }

        public Bitmap SetContrastStretch(Bitmap vehicleNumberImage)
        {
            ContrastStretch _contrastStretchFilter = new ContrastStretch();
            _contrastStretchFilter.ApplyInPlace(vehicleNumberImage);
            return vehicleNumberImage;
        }
        public Bitmap SetGammaCorrection(Bitmap vehicleNumberImage)
        {
            GammaCorrection _setGammaCorrection = new GammaCorrection(ParameterOfGamma);
            _setGammaCorrection.ApplyInPlace(vehicleNumberImage);
            return vehicleNumberImage;
        }
        public Bitmap SetNormalizedRGBChannel(Bitmap vehicleNumberImage)
        {
            ExtractNormalizedRGBChannel _normalizedRGB_G = new ExtractNormalizedRGBChannel(RGB.G);
            _normalizedRGB_G.Apply(vehicleNumberImage);
            //ExtractNormalizedRGBChannel _normalizedRGB_R = new ExtractNormalizedRGBChannel(RGB.R);
            //_normalizedRGB_R.Apply(vehicleNumberImage);
            //ExtractNormalizedRGBChannel _normalizedRGB_B = new ExtractNormalizedRGBChannel(RGB.B);
            //_normalizedRGB_B.Apply(vehicleNumberImage);
            return vehicleNumberImage;
        }
        public Bitmap SetSharpenFilter(Bitmap vehicleNumberImage)
        {
            Sharpen _sharpenFilter = new Sharpen();
            _sharpenFilter.ApplyInPlace(vehicleNumberImage);
            return vehicleNumberImage;
        }

        public Bitmap SetThresholdBinary(Bitmap vehicleNumberImage)
        {
            Bitmap rgbVehicleNumber = AForge.Imaging.Image.Clone(FilterForImageProcessing.Convert(vehicleNumberImage) as Bitmap, PixelFormat.Format8bppIndexed);
            Threshold filter = new Threshold(100);
            filter.ApplyInPlace(rgbVehicleNumber);
            return rgbVehicleNumber;
        }

        public Bitmap SetOtsuThreshold(Bitmap vehicleNumberImage)
        {
            Bitmap rgbVehicleNumber = AForge.Imaging.Image.Clone(FilterForImageProcessing.Convert(vehicleNumberImage) as Bitmap, PixelFormat.Format8bppIndexed);
            OtsuThreshold filter = new OtsuThreshold();
            filter.ApplyInPlace(rgbVehicleNumber);
            return rgbVehicleNumber;
        }

        public Bitmap SetGaussianSharpen(Bitmap vehicleNumberImage)
        {
            GaussianSharpen filter = new GaussianSharpen(4, 11);
            filter.ApplyInPlace(vehicleNumberImage);
            return vehicleNumberImage;
        }

        public Bitmap SetGrayScale(Bitmap vehicleNumberImage)
        {
            //for (int y = 0; y < vehicleNumberImage.Height; y++)
            //{
            //    for (int x = 0; x < vehicleNumberImage.Width; x++)
            //    {
            //        Color color = vehicleNumberImage.GetPixel(x, y);
            //        int r = color.R;
            //        int g = color.G;
            //        int b = color.B;
            //        int avg = (r + g + b) / 3;
            //        vehicleNumberImage.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
            //    }
            //}
            //return vehicleNumberImage;
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            return filter.Apply(vehicleNumberImage);
        }

        public static System.Drawing.Image Convert(Bitmap oldbmp)
        {
            using (var ms = new MemoryStream())
            {
                oldbmp.Save(ms, ImageFormat.Gif);
                ms.Position = 0;
                return System.Drawing.Image.FromStream(ms);
            }
        }
    }
}
