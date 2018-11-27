using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tesseract;

namespace VehicleVision.Engine.DataRecognition
{
    internal class DataEngine : IDataEngine
    {
        private string _tessDataDir = Directory.GetCurrentDirectory() + "/userdata/tessdata/";
        private const string _language = "eng";
        private const string _sample = "A,B,C,E,F,O,H,M,P,T,I,X,Y,0,1,2,3,4,5,6,7,8,9";
        private static string _str = String.Empty;

        public string StartDataEngine(Bitmap bmp)
        {
            _str = String.Empty;
            using (var engine = new TesseractEngine(_tessDataDir, _language, EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", _sample);
                using (var page = engine.Process(Interpolation.ScaleByPercent(bmp, 600), Rect.Empty, PageSegMode.SingleBlock))
                 {
                    using (StreamWriter file = new StreamWriter(Directory.GetCurrentDirectory() + "/userdata/images/NotesForCarNumbers.txt", true))
                    {
                        string text = String.Empty;
                        Regex regex = new Regex(@"\W");
                        text = regex.Replace(page.GetText(), "");
                        if (text.Length > 6 && text.Length < 10)
                        {
                            file.WriteLine(text);
                            _str = text;
                        }
                    }
                }
            }
            return _str;
        }
    }
}
