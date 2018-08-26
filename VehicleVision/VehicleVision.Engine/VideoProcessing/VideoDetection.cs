using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Accord.Vision.Detection;
using Accord.Vision.Tracking;
using VehicleVision.Engine.DataProcessing;
using VehicleVision.Engine.DataRecognition;

namespace VehicleVision.Engine.VideoProcessing
{
    internal class VideoDetection : IVideoDetection
    {
        private CascadeClassifier _vehicleNumberByCascade = new CascadeClassifier(Directory.GetCurrentDirectory() + "/userdata/number.xml");
        private HaarCascade _vehicleByHaar = HaarCascade.FromXml(Directory.GetCurrentDirectory() + "/userdata/vehicle.xml");

        private Bitmap _plateNumber;
        private byte[] _imageVehicleForSaveToDB;
        private Dictionary<int, DictionaryCarsPictureAndNumber> _dictionary = new Dictionary<int, DictionaryCarsPictureAndNumber>();
        private DictionaryCarsPictureAndNumber _valueVehicleAndNumber;

        private Queue<Bitmap> _vehicleImagesQueue = new Queue<Bitmap>();

        private VideoStream _video;
        private DataEngine _data;
        private FilterForImageProcessing _filter;
        private VideoSection _section;
        public VideoDetection()
        {
            _video = new VideoStream();
            _data = new DataEngine();
            _filter = new FilterForImageProcessing();
            _section = new VideoSection();
        }

        public Image<Hls, Byte> StartFrameProcessing()
        {
            Image<Bgr, Byte> originalFrame = _video.StartVideo().ToImage<Bgr, Byte>();
            _filter.SetGammaCorrection(originalFrame.Bitmap);
            _filter.SetNormalizedRGBChannel(originalFrame.Bitmap);
            Rectangle getRecognitionArea = new Rectangle(0, Convert.ToInt32(originalFrame.Height * 0.4), originalFrame.Width, originalFrame.Height);
            Bitmap modifiedFrameSizeBitmap = new Bitmap(_section.CutSection(originalFrame.Bitmap, getRecognitionArea));
            Image<Bgr, Byte> modifiedFrameSize = new Image<Bgr, Byte>(modifiedFrameSizeBitmap);
            Image<Hls, Byte> equalizeImage = Equalization.GetHistrogramEqualization(modifiedFrameSize);
            Image<Gray, Byte> frameInGgray = modifiedFrameSize.Convert<Gray, Byte>();

            //   Image<Gray, Byte> graySoft = frameInGgray.Convert<Gray, Byte>().PyrDown().PyrUp();
            //   Image<Gray, Byte> gray = frameInGgray.SmoothGaussian(3).AddWeighted(frameInGgray, 1.5, -0.5, 0);
            //  Image<Gray, Byte> bin = gray.ThresholdBinary(new Gray(149), new Gray(255));
            VehicleDetecting(frameInGgray);
            return equalizeImage;
        }

        public void VehicleDetecting(Image<Gray, Byte> frameInGgray)
        {
            var detector = new HaarObjectDetector(_vehicleByHaar, minSize: 200,
            searchMode: ObjectDetectorSearchMode.NoOverlap, scaleFactor: 1.1f,
            scalingMode: ObjectDetectorScalingMode.SmallerToGreater);
            Rectangle[] vehicleDetected = detector.ProcessFrame(frameInGgray.Bitmap);
            foreach (Rectangle vehicleCoordinates in vehicleDetected)
            {
                frameInGgray.ROI = vehicleCoordinates;
                Bitmap bmpVehicle = frameInGgray.ToBitmap();
                //Interpolation.ScaleByPercent(bmpCar, 600);
                _vehicleImagesQueue.Enqueue(bmpVehicle);
            }
            VehicleNumberDetection(_vehicleImagesQueue);
        }
        public void VehicleNumberDetection(Queue<Bitmap> vehicleImagesQueue)
        {
            int counter = 0;
            Image<Gray, Byte> imageInGgray;
            for (int i = vehicleImagesQueue.Count; i > 0; i--)
            {
                if (vehicleImagesQueue.Count != 0)
                {
                    imageInGgray = new Image<Gray, Byte>(vehicleImagesQueue.Dequeue());
                    Bitmap bitmapImageVehicle = imageInGgray.ToBitmap();
                    vehicleImagesQueue.TrimExcess();
                    Rectangle[] vehicleNumberDetection = _vehicleNumberByCascade.DetectMultiScale(
                    imageInGgray, 1.1, 4, new Size(50, 20), new Size(100, 50));
                    imageInGgray.ROI = Rectangle.Empty;

                    foreach (Rectangle vehicleNumber in vehicleNumberDetection)
                    {
                        counter++;
                        var TrackingDetector = new Camshift(vehicleNumber, CamshiftMode.HSL);

                        Bitmap OriginalNumber = new Bitmap(_section.CutNumber(bitmapImageVehicle, TrackingDetector.SearchWindow));
                       
                        if (AverageOfBrightness.GetAVGofBrightness(OriginalNumber) <= 125)
                        {
                            _plateNumber = OriginalNumber;
                            TrackingDetector.Reset();
                        }
                        else if (AverageOfBrightness.GetAVGofBrightness(OriginalNumber) > 125)
                        {
                            _plateNumber = OriginalNumber;
                            TrackingDetector.Reset();
                        }

                        SetFiltersToCarNumber(_plateNumber);

                        ImageConverter converterCar = new ImageConverter();
                        _imageVehicleForSaveToDB = (byte[])converterCar.ConvertTo(bitmapImageVehicle, typeof(byte[]));

                        if (_data.StartDataEngine(_plateNumber) != "")
                        {
                            _valueVehicleAndNumber = new DictionaryCarsPictureAndNumber(_imageVehicleForSaveToDB, _data.StartDataEngine(_plateNumber));
                            _dictionary.Add(counter, _valueVehicleAndNumber);
                            _dictionary.Clear();
                        }
                    }
                }
            }
        }

        private void SetFiltersToCarNumber(Bitmap plateNumber)
        {
            _filter.SetSharpenFilter(plateNumber);
            _filter.SetBrightnessCorrection(plateNumber);
            _filter.SetContrast(plateNumber);
            _filter.SetContrastStretch(plateNumber);

            //_filter.SetGrayScale(plateNumber);
            //_filter.SetThresholdBinary(plateNumber);
            //_filter.SetOtsuThreshold(plateNumber);
        }
    }
}
