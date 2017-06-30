using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio
{
    public class ProjectPlayer
    {
        string audioPath;
        Mp3FileReader fileReader;
        WaveOutEvent outEvents;
        TimeSpan currentTime;
        Timer timer;
        int plaingSample;
        bool play;
        
        public bool Playing
        {
            set { play = value;
                timer.Stop();
            }
            get { return play; }
        }
        bool setTime;
        public Timer Timer
        {
            get { return timer; }
        }


        public WaveOutEvent OutEvents
        { get { return outEvents; } }
        public TimeSpan CurrentTime
        {
            set
            {
                setTime = true;
                currentTime = Mathf.Clamp( value,new TimeSpan(),GetAllTotalTime());
            }
            get { return currentTime; }
        }


        List<Sample> listSample;

        public List<Sample> SetListSample
        {
            set
            {
                listSample = value;

                plaingSample = TimeToSegment();
                if (listSample.Count > 0)
                {
                    fileReader = new Mp3FileReader(listSample[plaingSample].SoundPath);
                    fileReader.CurrentTime = listSample[plaingSample].SplitStartTime;
                    outEvents.Stop();
                    outEvents.Init(fileReader);
                }
                else
                {
                    outEvents.Stop();
                    currentTime = new TimeSpan();
                    play = false;
                    timer.Stop();
                }
            }
            get
            {
                return listSample;
            }
        }


        public static ProjectPlayer Create(string path)
        {
            if (FileValidator.IsValid(path))
            {
                ProjectPlayer s = new ProjectPlayer(path);
                return s;
            }
            return null;
        }

        private ProjectPlayer(string path)
        {
            audioPath = path;
            fileReader = new Mp3FileReader(audioPath);
            outEvents = new WaveOutEvent();
            outEvents.Init(fileReader);
            currentTime = new TimeSpan();
            outEvents.PlaybackStopped += outEvents_PlaybackStopped;
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

        protected void outEvents_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            if (outEvents.PlaybackState == PlaybackState.Stopped)
            {
                if (play == true && plaingSample < listSample.Count() - 1)
                {
                    plaingSample++;
                    fileReader = new Mp3FileReader(listSample[plaingSample].SoundPath);
                    fileReader.CurrentTime = listSample[plaingSample].SplitStartTime;
                    outEvents.Stop();
                    outEvents.Init(fileReader);
                    outEvents.Play();
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        public int TimeToSegment()
        {
            int index = 0;

            if (listSample.Count > 0)
            {
                for (int i = 0; i < listSample.Count; i++)
                {

                    TimeSpan splitTime = SumBackToSegment(i);

                    if (currentTime >= splitTime && currentTime <= splitTime + listSample[i].SplitEndTime - listSample[i].SplitStartTime)
                        return i;

                }
                return listSample.Count - 1;
            }
            return index;
        }

        public ProjectPlayer(List<Sample> _listSample)
        {
            listSample = _listSample;
            currentTime = new TimeSpan();
            outEvents = new WaveOutEvent();
            if (listSample.Count > 0)
            {

                fileReader = new Mp3FileReader(listSample[0].SoundPath);
                fileReader.CurrentTime = listSample[0].SplitStartTime;

                outEvents.Init(fileReader);
                currentTime = new TimeSpan();
            }
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            //    outEvents.PlaybackStopped 
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            if (setTime && outEvents.PlaybackState == PlaybackState.Playing)
            {
                plaingSample = TimeToSegment();
                fileReader = new Mp3FileReader(listSample[plaingSample].SoundPath);
                fileReader.CurrentTime = currentTime - SumBackToSegment(plaingSample) + listSample[plaingSample].SplitStartTime;
                outEvents.Stop();
                outEvents.Init(fileReader);
                outEvents.Play();
                currentTime = SumBackToSegment(plaingSample) + fileReader.CurrentTime - listSample[plaingSample].SplitStartTime;
                setTime = false;
            }
            else
            {
                if (setTime && (outEvents.PlaybackState == PlaybackState.Paused || outEvents.PlaybackState == PlaybackState.Stopped)) {
                    fileReader.CurrentTime = currentTime - SumBackToSegment(plaingSample) + listSample[plaingSample].SplitStartTime;
                    currentTime = SumBackToSegment(plaingSample) + fileReader.CurrentTime - listSample[plaingSample].SplitStartTime;
                    setTime = false;
                } else {
                    currentTime = SumBackToSegment(plaingSample) + fileReader.CurrentTime - listSample[plaingSample].SplitStartTime;
                }
            }
            if (currentTime >= GetAllTotalTime())
            {
                outEvents.Stop();
                play = false;
                currentTime = new TimeSpan();
            }else
            if (outEvents.PlaybackState == PlaybackState.Stopped)
            {
                if (play == true && plaingSample < listSample.Count() - 1)
                {
                    plaingSample++;
                    fileReader = new Mp3FileReader(listSample[plaingSample].SoundPath);
                    fileReader.CurrentTime = listSample[plaingSample].SplitStartTime;
                    outEvents.Stop();
                    outEvents.Init(fileReader);
                    outEvents.Play();
                }
                else
                {
                    play = false;
                    timer.Stop();
                    outEvents.Stop();
                    currentTime = new TimeSpan();
                }
            }
            else
            if (SumBackToSegment(plaingSample) + listSample[plaingSample].SplitEndTime - listSample[plaingSample].SplitStartTime < SumBackToSegment(plaingSample) + fileReader.CurrentTime - listSample[plaingSample].SplitStartTime)
            {
                plaingSample = TimeToSegment();
                fileReader = new Mp3FileReader(listSample[plaingSample].SoundPath);
                fileReader.CurrentTime = listSample[plaingSample].SplitStartTime;
                outEvents.Stop();
                outEvents.Init(fileReader);
                outEvents.Play();
            }
        }

        public void Play()
        {
            if (listSample.Count > 0)
            {
                plaingSample = 0;
                outEvents.Play();
                timer.Start();
                play = true;
            }
        }

        private TimeSpan GetAllTotalTime()
        {
            TimeSpan totalTime = new TimeSpan();
            foreach (var i in listSample)
            {
                totalTime += i.SplitEndTime - i.SplitStartTime;
            }
            return totalTime;
        }

        public void Pause()
        {
            outEvents.Pause();
        }
        public void Stop()
        {
            play = false;
            outEvents.Stop();
            currentTime = new TimeSpan();
        }

    }
}
