using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using CSCore;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using SimpleAudioEditor.PeachStudio.WorkMethods;

namespace SimpleAudioEditor.PeachStudio
{
    public class Sample : ICloneable
    {

        private string soundPath;
        private TimeSpan splitStartTime;
        private TimeSpan splitEndTime;
        private TimeSpan currentTime;
        private TimeSpan totalTime;        
        private ISampleSource mDrawSource;
        private float[] optimizedArray;
        private string samplePath;
        [DataMember]
        public string SamplePath
        {
            set { samplePath = value; }
            get { return samplePath; }
        }


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
            if (_soundPath.Contains(".wav"))
            {
                soundPath = SampleController.Converter(_soundPath);
            }
            else
            {
                soundPath = _soundPath;
            }
            CreateDrawSourceAndArray();
            AudioFileReader a = new AudioFileReader(soundPath);
            totalTime = a.TotalTime;
            splitStartTime = new TimeSpan();
            splitEndTime = totalTime;
        }

        private void CreateDrawSourceAndArray()
        {
            mDrawSource = Mathf.CreateDrawSource(soundPath);
            optimizedArray = Mathf.CreateOptimizedArray(soundPath, mDrawSource);
        }

        public Sample(string _soundPath, float[] _optimizedArray, ISampleSource _mDrawSource, TimeSpan _splitStartTime, TimeSpan _splitEndTime, TimeSpan _totalTime)
        {
            if (_soundPath.Contains(".wav"))
            {
                soundPath = SampleController.Converter(_soundPath);
            }
            else
            {
                soundPath = _soundPath;
            }
            mDrawSource = _mDrawSource;
            optimizedArray = _optimizedArray;
            totalTime = _totalTime;
            splitStartTime = _splitStartTime;
            splitEndTime = _splitEndTime;
        }


        public Sample(Sample _sample)
        {
            if (_sample.soundPath.Contains(".wav"))
            {
                soundPath = SampleController.Converter(_sample.soundPath);
            }
            else
            {
                soundPath = _sample.soundPath;
            }
            mDrawSource = _sample.DrawSource;
            optimizedArray = _sample.OptimizedArray;
            totalTime = _sample.TotalTime;
            splitStartTime = _sample.SplitStartTime;
            splitEndTime = _sample.SplitEndTime;
        }

        public String CreateSamplePath(string projectPath, int order)
        {
            String newSamplePath = projectPath + "\\" + Path.GetFileName(this.SoundPath);
            newSamplePath = newSamplePath.Replace(".wav", "_" + order + ".wav");
            newSamplePath = newSamplePath.Replace(".mp3", "_" + order + ".mp3");
            return newSamplePath;
        }


        [IgnoreDataMember]
        public TimeSpan SplitStartTime
        {
            set { splitStartTime = value; }
            get { return splitStartTime; }
        }

        [IgnoreDataMember]
        public TimeSpan SplitEndTime
        {
            set { splitEndTime = value; }
            get { return splitEndTime; }
        }

        [IgnoreDataMember]
        public TimeSpan CurrentTime
        {
            set { currentTime = value; }
            get { return currentTime; }
        }

        [IgnoreDataMember]
        public TimeSpan TotalTime
        {
            get { return totalTime; }
        }

        [IgnoreDataMember]
        public float[] OptimizedArray
        {
            set { optimizedArray = value; }
            get { return optimizedArray; }
        }

        [IgnoreDataMember]
        public ISampleSource DrawSource
        {
            set { mDrawSource = value; }
            get { return mDrawSource; }
        }

        [DataMember]
        public string SoundPath
        {
            set
            {
                soundPath = value;
                CreateDrawSourceAndArray();
            }
            get
            {
                return soundPath;
            }
        }

        /// <summary>
        /// для сериализатора
        /// </summary>

        [DataMember]
        public string SplitStartTimeToString
        {
            set { splitStartTime = TimeSpan.Parse(value); }
            get { return splitStartTime.ToString(); }
        }

        [DataMember]
        public string SplitEndTimeToString
        {
            set { splitEndTime = TimeSpan.Parse(value); }
            get { return splitEndTime.ToString(); }
        }

        [DataMember]
        public string CurrentTimeToString
        {
            set { currentTime = TimeSpan.Parse(value); }
            get { return currentTime.ToString(); }
        }

        [DataMember]
        public string TotalTimeToString
        {
            get { return totalTime.ToString(); }
            set { totalTime = TimeSpan.Parse(value); }
        }

        object ICloneable.Clone()
        {
            Sample s = new Sample(this.soundPath, this.optimizedArray, this.mDrawSource, this.splitStartTime, this.splitEndTime, this.totalTime);
            s.currentTime = this.currentTime;
            s.samplePath = this.samplePath;
            return s;
        }
    }
}
