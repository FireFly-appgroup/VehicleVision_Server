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
        private CascadeClassifier _carNumberByCascade = new CascadeClassifier(Directory.GetCurrentDirectory() + "/userdata/numbers.xml");
        private HaarCascade _vehicleByHaar = HaarCascade.FromXml(Directory.GetCurrentDirectory() + "/userdata/cars.xml");

        private Bitmap _plateNumber;
        private byte[] _imageCarForSaveToDB;
        private Dictionary<int, DictionaryCarsPictureAndNumber> _dictionary = new Dictionary<int, DictionaryCarsPictureAndNumber>();
        private DictionaryCarsPictureAndNumber _valueCarAndNumber;

        private Queue<Bitmap> _carsImagesQueue = new Queue<Bitmap>();

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
            Rectangle getRecognitionArea = new Rectangle(0, Convert.ToInt32(originalFrame.Height * 0.35), originalFrame.Width, originalFrame.Height);
            Bitmap modifiedFrameSizeBitmap = new Bitmap(_section.CutSection(originalFrame.Bitmap, getRecognitionArea));
            Image<Bgr, Byte> modifiedFrameSize = new Image<Bgr, Byte>(modifiedFrameSizeBitmap);
            Image<Hls, Byte> equalizeImage = Equalization.GetHistrogramEqualization(modifiedFrameSize);
            Image<Gray, Byte> frameInGgray = modifiedFrameSize.Convert<Gray, Byte>();
            CarDetected(frameInGgray);
            return equalizeImage;
        }

        public void CarDetected(Image<Gray, Byte> frameInGgray)
        {
            var detector = new HaarObjectDetector(_vehicleByHaar, minSize: 200,
            searchMode: ObjectDetectorSearchMode.NoOverlap, scaleFactor: 1.1f,
            scalingMode: ObjectDetectorScalingMode.SmallerToGreater);
            Rectangle[] carsDetected = detector.ProcessFrame(frameInGgray.Bitmap);
            foreach (Rectangle carCoordinates in carsDetected)
            {
                frameInGgray.ROI = carCoordinates;
                Bitmap bmpCar = frameInGgray.ToBitmap();
                _carsImagesQueue.Enqueue(bmpCar);
            }
            CarNumberDetection(_carsImagesQueue);
        }
        public void CarNumberDetection(Queue<Bitmap> carsImagesQueue)
        {
            int counter = 0;
            Image<Gray, Byte> imageInGgray;
            for (int i = carsImagesQueue.Count; i > 0; i--)
            {
                if (carsImagesQueue.Count != 0)
                {
                    imageInGgray = new Image<Gray, Byte>(carsImagesQueue.Dequeue());
                    Bitmap bitmapImageCar = imageInGgray.ToBitmap();
                    carsImagesQueue.TrimExcess();
                    Rectangle[] carNumberDetected = _carNumberByCascade.DetectMultiScale(
                    imageInGgray, 1.1, 4, new Size(50, 20), new Size(100, 50));
                    imageInGgray.ROI = Rectangle.Empty;

                    foreach (Rectangle exampleCarNumber in carNumberDetected)
                    {
                        counter++;
                        var TrackingDetector = new Camshift(exampleCarNumber, CamshiftMode.HSL);

                        Bitmap OriginalNumber = new Bitmap(_section.CutNumber(bitmapImageCar, TrackingDetector.SearchWindow));
                        Bitmap brightnessNumber = new Bitmap(_section.CutNumber(StartFrameProcessing()[1].Bitmap, TrackingDetector.SearchWindow));
                        if (AverageOfBrightness.GetAVGofBrightness(OriginalNumber) <= 125)
                        {
                            _plateNumber = OriginalNumber;
                            TrackingDetector.Reset();
                        }
                        else if (AverageOfBrightness.GetAVGofBrightness(OriginalNumber) > 125)
                        {
                            _plateNumber = brightnessNumber;
                            TrackingDetector.Reset();
                        }

                        SetFiltersToCarNumber(_plateNumber);

                        ImageConverter converterCar = new ImageConverter();
                        _imageCarForSaveToDB = (byte[])converterCar.ConvertTo(bitmapImageCar, typeof(byte[]));

                        if (_data.StartDataEngine(_plateNumber) != "")
                        {
                            _valueCarAndNumber = new DictionaryCarsPictureAndNumber(_imageCarForSaveToDB, _data.StartDataEngine(_plateNumber));
                            _dictionary.Add(counter, _valueCarAndNumber);
                            _dictionary.Clear();
                        }
                    }
                }
            }
        }

        private void SetFiltersToCarNumber(Bitmap plateNumber)
        {
            _filter.SetBrightness(plateNumber);
            _filter.SetContrast(plateNumber);
            _filter.SetGrayScale(plateNumber);
            _filter.SetContrastStretch(plateNumber);
            _filter.SetSharpenFilter(plateNumber);
        }
    }
}
