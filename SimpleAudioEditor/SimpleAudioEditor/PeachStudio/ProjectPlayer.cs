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

        public void Play() { }
        public void Pause() { }
        public void Stop() { }
    }
}
