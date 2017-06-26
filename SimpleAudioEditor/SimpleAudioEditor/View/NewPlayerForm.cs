using SimpleAudioEditor.Controller;
using SimpleAudioEditor.Controller.Editor;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.View
{
    public partial class NewPlayerForm : Form
    {
        MainSoundLine m;
        public Primary primary;
        public Project project;
        int x = 6;
        int pauseX = 6;
        string pathToPause;
        string[] pauses;

        private void getPauses() {
            try
            {
                string dir = Directory.GetCurrentDirectory() + "\\Pauses";
                pauses = Directory.GetFiles(dir);

                for (int i = 0; i < pauses.Length; i++)
                {
                    string s = pauses[i];
                    string[] temp = s.Split('\\');

                    comboBox1.Items.Add(temp[temp.Length - 1]);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public NewPlayerForm()
        {
            InitializeComponent();

            panelSamples.AutoScroll = false;
            panelSamples.HorizontalScroll.Enabled = false;
            panelSamples.HorizontalScroll.Visible = false;
            panelSamples.HorizontalScroll.Maximum = 0;
            panelSamples.AutoScroll = true;

            /*
            var ofd = new OpenFileDialog();
            ofd.Filter = "Cursor Files|*.mp3;*.wav";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ClassTest ct = new ClassTest(ofd.FileName);
                

            }
            */
            primary = new Primary();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if ((new IntroForm(this).ShowDialog()) != DialogResult.OK)
            {
                this.Close();
                return;
            }
            this.Text = project.title;

            m = new MainSoundLine(660, 75, panelMain, new Point(0, 0), project);

            try
            {
                getPauses();
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddSample_Click(object sender, EventArgs e)
        {

            var ofd = new OpenFileDialog();
            ofd.Filter = "Cursor Files|*.mp3;*.wav";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    //Sample samp = new Sample(ofd.FileNames[i]);
                    //layoutSamples.Controls.Add(samp.lineEditor);
                    SoundLineEditor s = new SoundLineEditor(ofd.FileNames[i], panelSamples, new Point(6, x), 640, project);
                    x += 116;
                    pauseX = x;
                }
                MessageBox.Show("Загружено");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pathToPause = pauses[comboBox1.SelectedIndex];
        }

        private void buttonAddPause_Click(object sender, EventArgs e)
        {
            AddPause(pathToPause);
        }

        private void AddPause(string pathToPause)
        {
            SoundLineEditor s = new SoundLineEditor(pathToPause, panelSamples, new Point(6, pauseX), 640, project);
            pauseX += 116;

            MessageBox.Show("Пауза загружена");
        }
    }

}

