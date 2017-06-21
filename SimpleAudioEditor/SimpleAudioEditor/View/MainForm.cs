using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace SimpleAudioEditor
{

    public partial class MainForm : Form
    {
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
                waveViewer1.SamplesPerPixel = 400;
                waveViewer1.BackColor = System.Drawing.Color.OrangeRed;
                waveViewer1.WaveStream = new WaveFileReader(open.FileName);
            }
        }
    }
}
