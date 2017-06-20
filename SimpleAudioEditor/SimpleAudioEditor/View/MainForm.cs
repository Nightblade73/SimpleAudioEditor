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
using CSCore.CoreAudioAPI;
using System.Collections.ObjectModel;

namespace SimpleAudioEditor
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private readonly ObservableCollection<MMDevice> mDevices = new ObservableCollection<MMDevice>();
        private MMDeviceCollection mOutputDevices;

        SoundSource soundSource = new SoundSource();
        IWaveSource soundSource1;
        //для загрузки ресурса(музыки) 2
        IWaveSource soundSource2;
        public static ISoundOut soundOut;
        string fileSound;



        private void MainForm_Load(object sender, EventArgs e)
        {
            trackBarVolume.Value = 30;

            //Find sound capture devices and fill the cmbInput combo
            MMDeviceEnumerator deviceEnum = new MMDeviceEnumerator();
            //Find sound render devices and fill the cmbOutput combo
            MMDevice activeDevice = deviceEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            mOutputDevices = deviceEnum.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
            foreach (MMDevice device in mOutputDevices) {
                comboBox1.Items.Add(device);
                if (device.DeviceID == activeDevice.DeviceID) comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
            comboBox1.DisplayMember = "FriendlyName";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            soundOut.Play();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (soundOut != null) {
                soundOut.Dispose();                
            }
            if(mEditor != null) {
                mEditor.Dispose();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            soundOut.Pause();
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Cursor Files|*.mp3";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileSound = openFileDialog.FileName;
                //ресурс 1
                soundSource1 = soundSource.InitializationWaveSource(fileSound);
                //теперь воспросизводить будем ресурс 1
                soundSource.InitializationSoundOut(soundSource1);
                soundOut.Volume = (float)0.3;
                MessageBox.Show("загружено");
                try {
                    mEditor.OpenWaveFile(openFileDialog.FileName, (MMDevice)comboBox1.SelectedItem);
                    trackBarVolume.Value = mEditor.Player.Volume;
                    mEditor.Focus();
                } catch (Exception ex) {
                    MessageBox.Show("Could not open file: " + ex.Message);
                }
            }
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e) {
            soundOut.Volume = (float)trackBarVolume.Value / 100;
            mEditor.Player.Volume = trackBarVolume.Value;
        }
    }
}
