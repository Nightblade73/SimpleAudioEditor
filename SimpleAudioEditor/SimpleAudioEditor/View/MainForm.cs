using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SimpleAudioEditor
{

    public partial class MainForm : Form
    {
        List<string> inputFiles = new List<string>();
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void buttonPause_Click(object sender, EventArgs e)
        {

        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {

        }

        private void buttonEditorDelete_Click(object sender, EventArgs e)//метод по осуществлению события клика
        {

        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {

        }

        private void RelocationEditorController()
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                inputFiles.Add(open.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("E:\\output.mp3", FileMode.Create);
            Combine(fs);
        }

        public void Combine(Stream output)
        {
            foreach (string file in inputFiles)
            {
                Mp3FileReader reader = new Mp3FileReader(file);
                if ((output.Position == 0) && (reader.Id3v2Tag != null))
                {
                    output.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                {
                    output.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }

        public static MemoryStream ConvertWavToMp3(Wave32To16Stream wavFile)
        {
            using (var retMs = new MemoryStream())
            using (var wtr = new LameMP3FileWriter(retMs, wavFile.WaveFormat, 128))
            {
                wavFile.CopyTo(wtr);
                return retMs;
            }
        }

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {

        }
    }
}
