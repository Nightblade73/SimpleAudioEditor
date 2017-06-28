using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio {
    public class ProjectPlayer {
        string audioPath;
        Mp3FileReader fileReader;
        WaveOutEvent outEvents;
        TimeSpan currentTime;
        Timer timer;
        int plaingSample;
        public Timer Timer
        {
            get { return timer; }
        }

        public TimeSpan CurrentTime
        {
            set { currentTime = value; }
            get { return currentTime; }
        }


        List<Sample> listSample;

        public List<Sample> SetListSample
        {
            set { listSample = value; }
        }


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

        public TimeSpan SumBackToSegment(int index)
        {
            TimeSpan sumTime = new TimeSpan();
            for (int i = 0; i < index; i++)
            {
                sumTime += (listSample[i].SplitEndTime - listSample[i].SplitStartTime);
            }
            return sumTime;
        }

        public int TimeToSegment()
        {
            int index = 0;

             if (listSample.Count > 0)
                {
                    TimeSpan time = new TimeSpan();

                    for (int i = 0; i < listSample.Count; i++)
                    {
                        TimeSpan splitTime2 = (
                        TimeSpan.FromSeconds((listSample[i].SplitEndTime - listSample[i].SplitStartTime).TotalSeconds));
                    if (currentTime >= time && currentTime <= time + (listSample[i].SplitEndTime - listSample[i].SplitStartTime)) ;
                            return i;
                        time += (listSample[i].SplitEndTime - listSample[i].SplitStartTime);
                    }
                    return listSample.Count-1;
                }
            return index;
        }

        public ProjectPlayer(List<Sample> _listSample)
        {
            listSample = _listSample;
            fileReader = new Mp3FileReader(listSample[0].SoundPath);
            fileReader.CurrentTime = listSample[0].SplitStartTime;
            currentTime = new TimeSpan();
            outEvents = new WaveOutEvent();
            outEvents.Init(fileReader);
            currentTime = new TimeSpan();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
        //    outEvents.PlaybackStopped 
        }
        


        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTime = SumBackToSegment(plaingSample) +fileReader.CurrentTime-listSample[plaingSample].SplitStartTime;
            if (SumBackToSegment(plaingSample) + listSample[plaingSample].SplitEndTime - listSample[plaingSample].SplitStartTime < SumBackToSegment(plaingSample)+ fileReader.CurrentTime - listSample[plaingSample].SplitStartTime )
            {
                plaingSample++;
                fileReader = new Mp3FileReader(listSample[plaingSample].SoundPath);
                fileReader.CurrentTime = listSample[plaingSample].SplitStartTime;
                outEvents.Stop();
                outEvents.Init(fileReader);
                outEvents.Play();
            }
        }

        public void Play() {
            if (listSample.Count > 0) { 
            plaingSample = 0;
            outEvents.Play();
            timer.Start();
            }
        }

        public void Pause() {
            outEvents.Pause();
        }
        public void Stop() {
            outEvents.Stop();
        }

    }
}
