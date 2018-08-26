using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleVision.Engine.DataRecognition
{
    internal interface IFiltersForImageProcessing
    {
          Bitmap SetBrightnessCorrection(Bitmap bmp);
          Bitmap SetContrast(Bitmap bmp);
          Bitmap SetContrastStretch(Bitmap bmp);
          Bitmap SetGammaCorrection(Bitmap bmp);
          Bitmap SetNormalizedRGBChannel(Bitmap bmp);
          Bitmap SetSharpenFilter(Bitmap bmp);
          Bitmap SetGrayScale(Bitmap bmpPhoto);
          Bitmap SetThresholdBinary(Bitmap bmpPhoto);
          Bitmap SetOtsuThreshold(Bitmap bmpPhoto);
    }
}
