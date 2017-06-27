using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace SimpleAudioEditor.PeachStudio {
    class ProjectPlayer {
        string audioPath;
        Mp3FileReader fileReader;
        WaveOutEvent outEvents;
        TimeSpan currentTime;

        public static ProjectPlayer Create(string path) {
            if (FileValidator.IsValid(path)) {
                ProjectPlayer s = new ProjectPlayer(path);
                return s;
            }
            return null;
        }

        private ProjectPlayer(string path) {
            audioPath = path;
            fileReader = new Mp3FileReader(audioPath);
            outEvents = new WaveOutEvent();
            outEvents.Init(fileReader);
            currentTime = new TimeSpan();
        } 

        public void Play() {
            outEvents.Play();
        }
        public void Pause() {
            outEvents.Pause();
        }
        public void Stop() {
            outEvents.Stop();
        }
    }
}
