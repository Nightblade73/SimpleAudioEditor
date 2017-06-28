using SimpleAudioEditor.Controller;
using SimpleAudioEditor.Controller.Editor;
using SimpleAudioEditor.Properties;
using System;
using System.Drawing;
using System.IO;
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
                string dir = Directory.GetCurrentDirectory();
                dir = dir.Replace("\\bin\\Debug", "") + "\\Resources\\Pauses";
                pauses = Directory.GetFiles(dir);

                for (int i = 0; i < pauses.Length; i++)
                {
                    string st = pauses[i];
                    string[] temp = st.Split('\\');

                    comboBox1.Items.Add(temp[temp.Length - 1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string s = Application.StartupPath;
            s = s.Replace("\\bin\\Debug", "") + "\\Resources\\Pauses";
        }

        public NewPlayerForm()
        {
            InitializeComponent();
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
                    SoundLineEditor s = new SoundLineEditor(ofd.FileNames[i], panelSamples, new Point(6, x), 640, project);
                    x += 116;
                    pauseX = x;
                }
                MessageBox.Show("Загружено");
            }
        }

<<<<<<< Updated upstream
=======

        private void NewPlayerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                String mes = WorkMethods.CleanRAWFiles();
                if (!mes.Equals("ok"))
                {
                    MessageBox.Show(mes);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            DialogResult dialog = MessageBox.Show("Сохранить композицию перед закрытием?", "Сохранение перед закрытием", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                MessageBox.Show(WorkMethods.Save(project));
                WorkerXML.Serialize(project);
                Application.Exit();
            }
            else {
                Application.Exit();
            }
        }

>>>>>>> Stashed changes
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

        private void panelSamples_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Effect == DragDropEffects.Move)
            {
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < objects.Length; i++)
                {
                    if (string.Equals(Path.GetExtension(objects[i]), ".mp3", StringComparison.InvariantCultureIgnoreCase)
                        || (string.Equals(Path.GetExtension(objects[i]), ".wav", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        SoundLineEditor s = new SoundLineEditor(objects[i], panelSamples, new Point(6, pauseX), 640, project);
                        pauseX += 116;
                    }
                }
            }
        }

        private void panelSamples_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
               ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))

                e.Effect = DragDropEffects.Move;
        }
    }
}
