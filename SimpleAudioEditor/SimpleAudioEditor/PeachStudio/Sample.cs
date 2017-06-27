using System;

namespace SimpleAudioEditor.PeachStudio {
    public class Sample {
        string soundPath;
        TimeSpan splitStartTime;
        TimeSpan splitEndTime;
        TimeSpan currentTime;
        TimeSpan totalTime;
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
        private Sample(String filePath)
        {
            this.soundPath = filePath;
        }        
    }
}
