using System;
using System.Drawing;


namespace SimpleAudioEditor.Controller.Editor
{
    public class Segment : IComparable<Segment>
    {
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

        public Segment(double _splitStartTimeFromSecond, double _splitEndTimeFromSecond, double _allTimeFromSecond, string _filePath)
        {
            splitStartTimeFromSecond = _splitStartTimeFromSecond;
            splitEndTimeFromSecond = _splitEndTimeFromSecond;
            allTimeFromSecond = _allTimeFromSecond;
            filePath = _filePath;
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
