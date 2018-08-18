using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace VehicleVision.Engine.VideoProcessing
{
    internal interface IVideoDetection
    {
        Image<Hls, Byte> StartFrameProcessing();
    }
}
