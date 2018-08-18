using Emgu.CV;
using VehicleVision.Engine.DataProcessing;

namespace VehicleVision.Engine.VideoProcessing
{
    internal class VideoStream : IVideoStream
    {
        public VideoStream() { }
        public VideoStream(string URl) { }
        static Capture videoCapture = new Capture(@"rtmp://46.175.70.243:1935/mlivetv/cam14_720p");

        public Mat StartVideo()
        {
            Mat getFrame = videoCapture.QueryFrame();
            return getFrame;
        }
    }
}
