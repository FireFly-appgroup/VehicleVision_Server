using System.Drawing;

namespace VehicleVision.Engine.DataRecognition
{
    internal interface IVideoSection
    {
        Image CutSection(Image srcBitmap, Rectangle section);
        Image CutNumber(Image srcBitmap, Rectangle section);
    }
}
