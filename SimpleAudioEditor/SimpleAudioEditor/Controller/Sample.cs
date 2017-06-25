using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    public class Sample : IComparable<Sample>
    {
        private  String title;
        Project project;
        /* Путь к оригинальной звуковой дорожке
         * (не обрезанной)
         */
        private  String soundPath;
        /* Путь к сэмплу (обрезанной звуковой дорожке)
         * для каждого сэмпла, нужно создавать новый _экземпляр_ оригинальной дорожки
         */
        private String samplePath;

        public Point startPos, endPos;
        private double splitEndTimeFromSecond;
        private double splitStartTimeFromSecond;
        private double allTimeFromSecond;
        private int indexQueue;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public double LeghtFromSecond
        {
            get { return splitEndTimeFromSecond - splitStartTimeFromSecond; }
        }

        public string SoundPath
        {
            get
            {
                return soundPath;
            }

        }

        public string SamplePath
        {
            get
            {
                return samplePath;
            }
        }

        public double SplitEndTimeFromSecond
        {
            get
            {
                return splitEndTimeFromSecond;
            }
        }

        public double SplitStartTimeFromSecond
        {
            get
            {
                return splitStartTimeFromSecond;
            }
        }

        public double AllTimeFromSecond
        {
            get
            {
                return allTimeFromSecond;
            }          
        }

        public int IndexQueue
        {
            get
            {
                return indexQueue;
            }

            set
            {
                indexQueue = value;
                samplePath = project.path + "\\cut" + indexQueue + ".wav";
            }
        }

        public Sample()
        {

        }

        public Sample(double _splitStartTimeFromSecond, double _splitEndTimeFromSecond, double _allTimeFromSecond, string _soundPath, Project _project)
        {
            splitStartTimeFromSecond = _splitStartTimeFromSecond;
            splitEndTimeFromSecond = _splitEndTimeFromSecond;
            allTimeFromSecond = _allTimeFromSecond;
            soundPath = _soundPath;
            project = _project;
        }

        public int CompareTo(Sample other)
        {

            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return this.indexQueue.CompareTo(other.indexQueue);
        }
    }
}
