using SimpleAudioEditor.Controller.Editor;
using SimpleAudioEditor.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.Controller
{
    public class Sample : IComparable<Sample>
    {
        private  String title;
        ProjectOld project;
        /* Путь к оригинальной звуковой дорожке
         * (не обрезанной)
         */
        private  String soundPath;
        private Bitmap frequencyBitMap;
        
        /* Путь к сэмплу (обрезанной звуковой дорожке)
         * для каждого сэмпла, нужно создавать новый _экземпляр_ оригинальной дорожки
         */
        private String samplePath;
        public SampleLineEditor lineEditor;

        public Point startPos, endPos;
        private double splitEndTimeFromSecond;
        private double splitStartTimeFromSecond;
        private double allTimeFromSecond;
        private int indexQueue;

        public Bitmap FrequencyBitMap
        {
            get { return frequencyBitMap; }
        }

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

        public Sample(String filepath)
        {
            this.soundPath = filepath;
            lineEditor = new SampleLineEditor(this);
        }

        public Sample(double _splitStartTimeFromSecond, double _splitEndTimeFromSecond, double _allTimeFromSecond, string _soundPath, ProjectOld _project, Bitmap _frequencyBitMap)
        {
            splitStartTimeFromSecond = _splitStartTimeFromSecond;
            splitEndTimeFromSecond = _splitEndTimeFromSecond;
            allTimeFromSecond = _allTimeFromSecond;
            soundPath = _soundPath;
            project = _project;
            frequencyBitMap = _frequencyBitMap;
        }

        public int CompareTo(Sample other)
        {

            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return this.indexQueue.CompareTo(other.indexQueue);
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

        public void Play()
        {
            MessageBox.Show(this.soundPath + " play");
        }
        public void Pause()
        {
            MessageBox.Show(this.soundPath+" pause");
        }
        public void Stop()
        {
            MessageBox.Show(this.soundPath + " stop");
        }
    }
}
