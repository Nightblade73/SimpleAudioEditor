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
using SimpleAudioEditor.Controller.WaveController;

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

        /// <summary>
        /// результирующий файл. НУЖНО ОБЯЗАТЕЛЬНО ИНИЦИАЛИЗИРОВАТЬ ГДЕ-НИБУДЬ
        /// </summary>
        string fileResult;

        List<string> fileSounds = new List<string>();

        public List<Controller.WaveController.WaveEditor> WiveEditorList = new List<Controller.WaveController.WaveEditor>();
        List<Button> EditorDeleteButtons = new List<Button>();
        List<Button> EditorAddTrackButtons = new List<Button>();


        private void MainForm_Load(object sender, EventArgs e)
        {
            if((new IntroForm(this).ShowDialog()) != DialogResult.OK)
            {
                this.Close();
            }

            trackBarVolume.Value = 30;

            //Find sound capture devices and fill the cmbInput combo
            MMDeviceEnumerator deviceEnum = new MMDeviceEnumerator();
            //Find sound render devices and fill the cmbOutput combo
            MMDevice activeDevice = deviceEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            mOutputDevices = deviceEnum.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
            foreach (MMDevice device in mOutputDevices)
            {
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
            if (soundOut != null)
            {
                soundOut.Dispose();
            }
            if (mEditor != null)
            {
                mEditor.Dispose();
                for (int i = 0; i < WiveEditorList.Count; i++)
                {
                    WiveEditorList[i].Dispose();
                }
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
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    fileSounds.Add(openFileDialog.FileNames[i]);
                    WiveEditorList.Add(new Controller.WaveController.WaveEditor());
                    if (WiveEditorList.Count <= 1)
                        WiveEditorList[WiveEditorList.Count - 1].Location = new Point(0, 0);
                    else
                        WiveEditorList[WiveEditorList.Count - 1].Location = new Point(0, WiveEditorList[WiveEditorList.Count - 2].Location.Y + 66);

                    WiveEditorList[WiveEditorList.Count - 1].Size = new Size( new Point(600, mEditor.Size.Height));
                    
                    this.Controls.Add(WiveEditorList[WiveEditorList.Count - 1]);
                    WiveEditorList[WiveEditorList.Count - 1].Parent = samplesPanel;

                    EditorDeleteButtons.Add(new Button());
                    EditorDeleteButtons[EditorDeleteButtons.Count - 1].Text = "удалить";
                    this.Controls.Add(EditorDeleteButtons[EditorDeleteButtons.Count - 1]);
                    EditorDeleteButtons[EditorDeleteButtons.Count - 1].Location = new Point(
                        WiveEditorList[WiveEditorList.Count - 1].Size.Width + 6, WiveEditorList[WiveEditorList.Count - 1].Location.Y);
                    EditorDeleteButtons[EditorDeleteButtons.Count - 1].Parent = samplesPanel;
                    EditorDeleteButtons[EditorDeleteButtons.Count - 1].Click+= buttonEditorDelete_Click;

                    EditorAddTrackButtons.Add(new Button());
                    EditorAddTrackButtons[EditorAddTrackButtons.Count - 1].Text = "добавить";
                    this.Controls.Add(EditorAddTrackButtons[EditorAddTrackButtons.Count - 1]);
                    EditorAddTrackButtons[EditorAddTrackButtons.Count - 1].Location = new Point(
                        WiveEditorList[WiveEditorList.Count - 1].Size.Width + 6, WiveEditorList[WiveEditorList.Count - 1].Location.Y+30);
                    EditorAddTrackButtons[EditorAddTrackButtons.Count - 1].Parent = samplesPanel;
                    EditorAddTrackButtons[EditorAddTrackButtons.Count - 1].Click += buttonEditorAddTrack_Click;

                    this.Update();
                    //ресурс 1
                    soundSource1 = soundSource.InitializationWaveSource(fileSounds[fileSounds.Count-1]);
                    //теперь воспросизводить будем ресурс 1
                     soundSource.InitializationSoundOut(soundSource1);
                     soundOut.Volume = (float)0.3;
                    try
                    {
                        WiveEditorList[WiveEditorList.Count - 1].OpenWaveFile(openFileDialog.FileNames[i], (MMDevice)comboBox1.SelectedItem);
                        trackBarVolume.Value = WiveEditorList[WiveEditorList.Count - 1].Player.Volume;
                        WiveEditorList[WiveEditorList.Count - 1].Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not open file: " + ex.Message);
                    }
                }
                MessageBox.Show("загружено");
                WiveEditorList[WiveEditorList.Count - 1].Focus();
            }
        }
        /// <summary>
        /// /ВОВА, ТЕБЕ СЮДА
        /// обработчик события кнопки добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditorAddTrack_Click(object sender, EventArgs e)//метод по осуществлению события клика
        {
            int index = EditorAddTrackButtons.IndexOf((Button)sender);
            WiveEditorList[index].StopPlaying();
            SampleController sc = new SampleController();
            
            /// путь к файлу с выбранным треком
             fileSounds[index].ToString();
       //     sc.TrimWavFile();
            /// путь к рещзультирующему
            // fileResult
            
            //длина отрезка в мидисекундах
            //тоже пока не то
            //int timeMilisec = WiveEditorList[index].SamplesPerMilisecond;
            //TimeSpan timeSample = new TimeSpan(0, 0, 0, 0, timeMilisec);
            //начальная позиция
            //пока не работает
            //long startPosSample = WiveEditorList[index].getMCursorPosSample();
            //TimeSpan timeStartPosSample = new TimeSpan(startPosSample);

            //длинна всей песни
            TimeSpan allTime = WiveEditorList[index].getmMWaveSourceLength();


            ///не знаю что делает
            RelocationEditorController();
        }

        private void buttonEditorDelete_Click(object sender, EventArgs e)//метод по осуществлению события клика
        {
            int index = EditorDeleteButtons.IndexOf((Button)sender);
            EditorDeleteButtons[index].Click -= buttonEditorDelete_Click;
            EditorDeleteButtons[index].Dispose();
            WiveEditorList[index].StopPlaying();
            WiveEditorList[index].Dispose();
            WiveEditorList.RemoveAt(index);
            EditorDeleteButtons.RemoveAt(index);

            RelocationEditorController();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            soundOut.Volume = (float)trackBarVolume.Value * 0.01f;
            mEditor.Player.Volume = trackBarVolume.Value;
        }

        private void RelocationEditorController()
        {
            for(int i=0;i< WiveEditorList.Count; i++) { 
            if (i == 0)
                WiveEditorList[i].Location = new Point(0, 0);
            else
                WiveEditorList[i].Location = new Point(0, WiveEditorList[i - 1].Location.Y + 66);

            EditorDeleteButtons[i].Location = new Point(
                WiveEditorList[i].Size.Width + 6, WiveEditorList[i].Location.Y);
            }
            this.Update();
        }
    }
}
