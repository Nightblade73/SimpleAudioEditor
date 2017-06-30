using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio {
    public class SamplePlayer {
        AudioFileReader fileReader;
        WaveOutEvent player;
        Timer timer;

        public PlaybackState PlayerState
        {
            get {return player.PlaybackState; }
        }

        public float Volume
        {
            set { player.Volume = value; }
            get { return player.Volume; }
        }

        public Timer GetTimer
        {
            get {return timer; }
        }

        public SamplePlayer(string _filePath)
        {
            
            fileReader = new AudioFileReader(_filePath);
            player = new WaveOutEvent();
            player.Init(fileReader);
            timer = new Timer();
            timer.Interval = 10;
            player.PlaybackStopped += player_PlaybackStopped;
        }
        
        public TimeSpan CurrentTime
        {
            set { fileReader.CurrentTime = value; }
            get { return fileReader.CurrentTime; }
        }

        public TimeSpan TotalTime
        {
            get { return fileReader.TotalTime; }
        }

        public void Play()
        {
            player.Play();
            timer.Start();
        }
        public void Pause()
        {
            player.Pause();
            player.Stop();
        }
        public void Stop()
        {
            player.Stop();
            fileReader.Position = 0;
        }

        public void trackBar_ValueChanged(object sender, EventArgs e)
        {
            player.Volume = (sender as TrackBar).Value*0.01f;
        }

        protected void player_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            timer.Stop();
            //if(player.PlaybackState == PlaybackState.Stopped)
            //fileReader.Position = 0;
        }

    }
}
