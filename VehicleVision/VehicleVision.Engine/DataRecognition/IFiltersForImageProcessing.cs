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
          Bitmap SetBrightnessCorrection(Bitmap bmpPhoto);
          Bitmap SetContrast(Bitmap bmpPhoto);
          Bitmap SetContrastStretch(Bitmap bmpPhoto);
          Bitmap SetGammaCorrection(Bitmap bmpPhoto);
          Bitmap SetNormalizedRGBChannel(Bitmap bmpPhoto);
          Bitmap SetSharpenFilter(Bitmap bmpPhoto);
          Bitmap SetGrayScale(Bitmap bmpPhoto);
          Bitmap SetThresholdBinary(Bitmap bmpPhoto);
          Bitmap SetOtsuThreshold(Bitmap bmpPhoto);
          Bitmap SetHistogramEqualization(Bitmap bmpPhoto);
    }
}
