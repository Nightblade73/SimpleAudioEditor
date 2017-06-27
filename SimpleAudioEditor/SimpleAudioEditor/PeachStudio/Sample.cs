using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using CSCore;

namespace SimpleAudioEditor.PeachStudio {
    public class Sample {
        string soundPath;
        TimeSpan splitStartTime;
        TimeSpan splitEndTime;
        TimeSpan currentTime;
        TimeSpan totalTime;

        ISampleSource mDrawSource;
        float[] optimizedArray;

        public static Sample CreateSample(String filePath)
        {
            if(FileValidator.IsValid(filePath))
            {
                Sample s = new Sample(filePath);
                return s;
            }
            return null;
        }

        public Sample(string _soundPath)
        {
            soundPath = _soundPath;
            mDrawSource = Mathf.CreateDrawSource(soundPath);
            optimizedArray = Mathf.CreateOptimizedArray(soundPath,mDrawSource);
            AudioFileReader a = new AudioFileReader(soundPath);
            totalTime = a.TotalTime;
            splitStartTime = new TimeSpan();
            splitEndTime = totalTime;
        }

        public TimeSpan SplitStartTime
        {
            set { splitStartTime = value;}
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

        public float[] OptimizedArray
        {
            set { optimizedArray = value; }
            get { return optimizedArray; }
        }

        public ISampleSource DrawSource
        {
            set { mDrawSource = value; }
            get { return mDrawSource; }
        }

        public string SoundPath
        {
            set{soundPath =value;}
            get { return soundPath;}
        }
    }
}
