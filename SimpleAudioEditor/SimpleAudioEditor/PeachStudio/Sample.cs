using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using CSCore;
using System.IO;
using System.Xml.Serialization;

namespace SimpleAudioEditor.PeachStudio
{
    [Serializable]
    public class Sample
    {

        private string soundPath;
        private TimeSpan splitStartTime;
        private TimeSpan splitEndTime;
        private TimeSpan currentTime;
        private TimeSpan totalTime;
        [XmlIgnore]
        private ISampleSource mDrawSource;
        private float[] optimizedArray;


        public static Sample CreateSample(String filePath)
        {
            if (FileValidator.IsValid(filePath))
            {
                Sample s = new Sample(filePath);
                return s;
            }
            return null;
        }

        public Sample() { }

        public Sample(string _soundPath)
        {
            soundPath = _soundPath;
            mDrawSource = Mathf.CreateDrawSource(soundPath);
            optimizedArray = Mathf.CreateOptimizedArray(soundPath, mDrawSource);
            AudioFileReader a = new AudioFileReader(soundPath);
            totalTime = a.TotalTime;
            splitStartTime = new TimeSpan();
            splitEndTime = totalTime;
        }

        public Sample(string _soundPath, float[] _optimizedArray, ISampleSource _mDrawSource, TimeSpan _splitStartTime, TimeSpan _splitEndTime, TimeSpan _totalTime)
        {
            soundPath = _soundPath;
            mDrawSource = _mDrawSource;
            optimizedArray = _optimizedArray;
            totalTime = _totalTime;
            splitStartTime = _splitStartTime;
            splitEndTime = _splitEndTime;
        }


        public Sample(Sample _sample)
        {
            soundPath = _sample.SoundPath;
            mDrawSource = _sample.DrawSource;
            optimizedArray = _sample.OptimizedArray;
            totalTime = _sample.TotalTime;
            splitStartTime = _sample.SplitStartTime;
            splitEndTime = _sample.SplitEndTime;
        }

        public String SamplePath(string projectPath, int order)
        {
            String newSamplePath = projectPath + "\\" + Path.GetFileName(this.SoundPath);
            newSamplePath = newSamplePath.Replace(".wav", "_" + order + ".wav");
            newSamplePath = newSamplePath.Replace(".mp3", "_" + order + ".mp3");
            return newSamplePath;
        }



        public TimeSpan SplitStartTime
        {
            set { splitStartTime = value; }
            get { return splitStartTime; }
        }

        public TimeSpan SplitEndTime
        {
            set { splitEndTime = value; }
            get { return splitEndTime; }
        }

        public TimeSpan CurrentTime
        {
            set { currentTime = value; }
            get { return currentTime; }
        }

        public TimeSpan TotalTime
        {
            get { return totalTime; }
        }
        [XmlIgnore]
        public float[] OptimizedArray
        {
            set { optimizedArray = value; }
            get { return optimizedArray; }
        }
        [XmlIgnore]
        public ISampleSource DrawSource
        {
            set { mDrawSource = value; }
            get { return mDrawSource; }
        }

        public string SoundPath
        {
            set { soundPath = value; }
            get { return soundPath; }
        }
    }
}
