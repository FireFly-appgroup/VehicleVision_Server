namespace VehicleVision.Engine.DataProcessing
{
    internal sealed class DictionaryCarsPictureAndNumber
    {
        public byte[] carsPicture { get; set; }
        public string carsNumber { get; set; }

        public DictionaryCarsPictureAndNumber(byte[] carsPicture, string carsNumber)
        {
            this.carsPicture = carsPicture;
            this.carsNumber = carsNumber;
        }
    }
}
