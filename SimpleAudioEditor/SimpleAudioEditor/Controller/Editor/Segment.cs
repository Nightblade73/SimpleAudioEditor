using NAudio.Wave;
using System;
using System.Drawing;


namespace SimpleAudioEditor.Controller.Editor
{
    public class Segment : IComparable<Segment>
    {
        private Bitmap frequencyBitMap;
        public Bitmap BitMap
        {
            get { return frequencyBitMap; }
        }
        public Point segmentStartPos, segmentEndPos;
        private string filePath;
        double splitEndTimeFromSecond;
        double splitStartTimeFromSecond;
        double allTimeFromSecond;

        public int indexQueue;

        public string getFilePath
        {
            get { return filePath; }
        }

        public double getAllTimeFromSecond
        {
            get { return allTimeFromSecond; }
        }
        public double LeghtFromSecond
        {
            get { return splitEndTimeFromSecond - splitStartTimeFromSecond; }
        }

        public double SplitEndTimeFromSecond
        {
            get { return splitEndTimeFromSecond; }
        }

        public double SplitStartTimeFromSecond
        {
            get { return splitStartTimeFromSecond; }
        }

        public Segment(double _splitStartTimeFromSecond, double _splitEndTimeFromSecond, double _allTimeFromSecond, string _filePath, Bitmap bitMap)
        {
            splitStartTimeFromSecond = _splitStartTimeFromSecond;
            splitEndTimeFromSecond = _splitEndTimeFromSecond;
            allTimeFromSecond = _allTimeFromSecond;
            filePath = _filePath;
            frequencyBitMap = bitMap;
           // mp3Reader = new Mp3FileReader(filePath);
        }

        public void ResizeBitMap(Size size)
        {
            frequencyBitMap = ResizeImage(frequencyBitMap, size);
        }

       private static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }

        public int CompareTo(Segment other)
        {

            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return this.indexQueue.CompareTo(other.indexQueue);
        }

        
    }
}
