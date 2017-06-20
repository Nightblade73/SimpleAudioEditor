using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleAudioEditor.Controller;

namespace SimpleAudioEditor
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        SoundSource soundSource = new SoundSource();
        IWaveSource soundSource1;
        //для загрузки ресурса(музыки) 2
        IWaveSource soundSource2;
        public static ISoundOut soundOut;
        string fileSound;



        private void MainForm_Load(object sender, EventArgs e)
        {
            trackBarVolume.Value = 30;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            soundOut.Play();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            soundOut.Dispose();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            soundOut.Pause();
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Cursor Files|*.mp3";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileSound = openFileDialog1.FileName;
                //ресурс 1
                soundSource1 = soundSource.InitializationWaveSource(fileSound);
                //теперь воспросизводить будем ресурс 1
                soundSource.InitializationSoundOut(soundSource1);
                soundOut.Volume = (float)0.3;
                MessageBox.Show("загружено");
            }
        }

        private void trackBarVolum_Scroll(object sender, EventArgs e)
        {
            soundOut.Volume = (float)trackBarVolume.Value / 100;
        }
    }
}
