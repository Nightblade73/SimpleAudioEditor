using CSCore;
using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SimpleAudioEditor.Controller;
using SimpleAudioEditor.Model;
using CSCore.CoreAudioAPI;
using System.Collections.ObjectModel;
using SimpleAudioEditor.Controller.WaveController;
using System.IO;
using System.Threading.Tasks;

namespace SimpleAudioEditor
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static Action ControlClickEvent = delegate(){};
        public static Action ThreadStartEvent = delegate (){};
        public static Action ThreadFinishEvent = delegate (){};

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
            ControlClickEvent += ControlClickEventHandler;
            ThreadStartEvent += ThreadStartEventHandler;
            ThreadFinishEvent += ThreadFinishEventHandler;
            if ((new IntroForm(this).ShowDialog()) != DialogResult.OK)
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

            if (Directory.Exists(Params.ResultSoundsPath)) {
                Console.WriteLine("That path exists already.");
                return;
            } else {
                DirectoryInfo di = Directory.CreateDirectory(Params.ResultSoundsPath);
            }
        }

        private void ThreadFinishEventHandler()
        {
            samplesPanel.Enabled = true;
        }

        private void ThreadStartEventHandler()
        {
            samplesPanel.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            soundOut.Play();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ControlClickEvent -= ControlClickEventHandler;
            if (soundOut != null)
            {
                soundOut.Stop();
                soundOut.Dispose();
            }
            if (mEditor != null)
            {
                mEditor.Player.Stop();
                mEditor.Dispose();
                for (int i = 0; i < WiveEditorList.Count; i++) {
                    WiveEditorList[i].Player.Stop();
                    WiveEditorList[i].Dispose();
                }
            }
            WiveEditorList.Clear();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            soundOut.Pause();
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Cursor Files|*.mp3;*.wav";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    fileSounds.Add(openFileDialog.FileNames[i]);
                    WiveEditorList.Add(new WaveEditor());
                    if (WiveEditorList.Count <= 1)
                        WiveEditorList[WiveEditorList.Count - 1].Location = new Point(0, 0);
                    else
                        WiveEditorList[WiveEditorList.Count - 1].Location = new Point(0, WiveEditorList[WiveEditorList.Count - 2].Location.Y + WiveEditorList[WiveEditorList.Count - 2].Height);

                    WiveEditorList[WiveEditorList.Count - 1].Size = new Size(new Point(Params.NewSamplesWidth, mEditor.Size.Height * Params.CoefNewSamplesToMainSample));
                    
                    this.Controls.Add(WiveEditorList[WiveEditorList.Count - 1]);
                    WiveEditorList[WiveEditorList.Count - 1].Parent = samplesPanel;                    

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

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            soundOut.Volume = (float)trackBarVolume.Value * 0.01f;
            mEditor.Player.Volume = trackBarVolume.Value;
            foreach (var i in WiveEditorList) {
                i.Player.Volume = trackBarVolume.Value;
            }
        }

        //обработчик события ControlClickEvent
        void ControlClickEventHandler() {
            try
            {
                //Task nt = Task.Run(() =>
                //{
                
                    mEditor.OpenWaveFile(Model.Params.ResultSoundsPath + "\\" + Model.Params.ResultFileName, (MMDevice)comboBox1.SelectedItem);
                    trackBarVolume.Value = mEditor.Player.Volume;
                    mEditor.Focus();
                //});
                // MainForm.ThreadStartEvent();
                //var divisionawaiter = nt.GetAwaiter();
                //divisionawaiter.OnCompleted(MainForm.ThreadFinishEvent);
            } catch(Exception ex)
            {
                MessageBox.Show("Could not open file: " + ex.Message);
            }

        }
    }
}
