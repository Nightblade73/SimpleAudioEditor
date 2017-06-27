using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(ValidateFile(filePath))
            {
                Sample s = new Sample(filePath);
            }
            return null;
        }
        public Sample(String filePath)
        {
            this.soundPath = filePath;
        }
        private static bool ValidateFile(String filePath)
        {
            if (!File.Exists(filePath)) {
                //throw new FileNotFoundException();
                return false;
            }
            String ext = Path.GetExtension(filePath);
            if (ext != "wav" || ext != "mp3")
            {
                //throw new InvalidDataException("Неверное расширение сэмпла");
                return false;
            }
            return true;
        }
    }
}
