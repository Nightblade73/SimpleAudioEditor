using System;
using System.Drawing;


namespace SimpleAudioEditor.Controller.Editor
{
    public class Segment : IComparable<Segment>
    {
        public Point segmentStartPos, segmentEndPos;
        public string filePath;
        double splitEndTimeFromSecond;
        double splitStartTimeFromSecond;

        public int indexQueue;


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

        public Segment(double _splitStartTimeFromSecond, double _splitEndTimeFromSecond)
        {
            splitStartTimeFromSecond = _splitStartTimeFromSecond;
            splitEndTimeFromSecond = _splitEndTimeFromSecond;
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
