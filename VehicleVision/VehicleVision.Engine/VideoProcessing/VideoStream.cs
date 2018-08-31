using Emgu.CV;
using System;
using System.Diagnostics;
using System.IO;

namespace VehicleVision.Engine.VideoProcessing
{
    internal class VideoStream : IVideoStream
    {
        public VideoStream() { }
        public VideoStream(string URl) { }
        private static string _rtmp = @"rtmp://46.175.70.243:1935/mlivetv/cam14_720p";
        private static Capture videoCapture = new Capture(_rtmp);
        public Mat StartVideo()
        {
            Mat getFrame = videoCapture.QueryFrame();
            return getFrame;
        }
    }
}
