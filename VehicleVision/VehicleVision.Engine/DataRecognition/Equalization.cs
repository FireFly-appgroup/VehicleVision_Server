using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace VehicleVision.Engine.DataRecognition
{
    internal static class Equalization
    {
        public static Image<Hls, byte> GetHistrogramEqualization(Image<Bgr, byte> ModifiedFrameSize)
        {
            Image<Hls, Byte> EqualizeImage = new Image<Hls, Byte>(ModifiedFrameSize.Bitmap);
            Image<Gray, Byte> ImageInGray = EqualizeImage[1];
            ImageInGray._EqualizeHist();
            EqualizeImage[1] = ImageInGray;
            return EqualizeImage;
        }
    }
}
