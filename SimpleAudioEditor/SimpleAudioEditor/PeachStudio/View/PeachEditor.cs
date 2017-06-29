using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio.View {
    public partial class PeachEditor : Form {
        List<SampleControl> sampleControls;
        public Project project;
        string pathToPause;
        int verticalDistanceBeetwenControls = 5;
        int verticalControlsPosition = 0;
        string[] pauses;
        public Project oldProject;

        public PeachEditor(Project _project) {
            project = _project;
            oldProject = _project;       
            InitializeComponent();
              


            projectControl.ChangeCurrentProject(project);
            panelSample.HorizontalScroll.Enabled = false;
            panelSample.HorizontalScroll.Visible = false;
            panelSample.HorizontalScroll.Visible = false;
            panelSample.HorizontalScroll.Maximum = 0;
            panelSample.HorizontalScroll.LargeChange = 0;
            panelSample.HorizontalScroll.SmallChange = 0;
            panelSample.AutoScroll = true;
            sampleControls = new List<SampleControl>();
        }
        public PeachEditor() {

        }

        private void getPauses() {
            try {
                string dir = Directory.GetCurrentDirectory();
                dir = dir.Replace("\\bin\\Debug", "") + "\\Resources\\Pauses";
                pauses = Directory.GetFiles(dir);

                for (int i = 0; i < pauses.Length; i++) {
                    string st = pauses[i];
                    string[] temp = st.Split('\\');

                    comboBox1.Items.Add(temp[temp.Length - 1]);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {

        }

        private void buttonAddSample_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Cursor Files|*.mp3;*.wav";
            //    ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                foreach (var fileName in ofd.FileNames) {
                    Sample m = new Sample(fileName);
                    SampleControl sc = new SampleControl(m, panelSample, new Point(0, verticalControlsPosition));
                    trackBar1.ValueChanged += sc.GetSamplePlayer.trackBar_ValueChanged;
                    sc.GetSamplePlayer.Volume = trackBar1.Value * 0.01f;
                    sampleControls.Add(sc);


                    verticalControlsPosition += sc.MinimumSize.Height + verticalDistanceBeetwenControls;
                }
            }
        }

        private void panelSample_ControlAdded(object sender, ControlEventArgs e) {


        }

        private void panelSample_Layout(object sender, LayoutEventArgs e) {
            panelSupport.Size = new Size(panelSupport.Size.Width, panelSample.Size.Height + 2);
        }

        private void PeachEditor_Load(object sender, EventArgs e) {
            trackBar1.BackColor = Color.FromArgb(43, 43, 43);

            try {
                getPauses();
                comboBox1.SelectedIndex = 0;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void PeachEditor_FormClosed(object sender, FormClosedEventArgs e) {
            //this.Parent.Show();
            WorkMethods.WorkMethods.CleanRAWFiles();

            if (!project.Equals(oldProject))
            {
                DialogResult dialog = MessageBox.Show("Сохранить композицию перед закрытием?", "Сохранение перед закрытием", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    WorkMethods.WorkMethods.Save(project);
                    WorkMethods.WorkerXML.Serialize(project);
                    MyMessageBox mmb = new MyMessageBox("Сохранено!", false);
                    mmb.ShowDialog();
                    Close();
                }
            }
            else
            {
                Close();

            }
        }

        private void panelSample_DragDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Effect == DragDropEffects.Move) {
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < objects.Length; i++) {
                    if (string.Equals(Path.GetExtension(objects[i]), ".mp3", StringComparison.InvariantCultureIgnoreCase)
                        || (string.Equals(Path.GetExtension(objects[i]), ".wav", StringComparison.InvariantCultureIgnoreCase))) {
                        Sample m = new Sample(objects[i]);
                        SampleControl sc = new SampleControl(m, panelSample, new Point(0, verticalControlsPosition));

                        trackBar1.ValueChanged += sc.GetSamplePlayer.trackBar_ValueChanged;
                        sc.GetSamplePlayer.Volume = trackBar1.Value * 0.01f;
                        sampleControls.Add(sc);

                        verticalControlsPosition += sc.MinimumSize.Height + verticalDistanceBeetwenControls;
                    }
                }
            }
        }

        private void panelSample_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))

                e.Effect = DragDropEffects.Move;
        }

        private void buttonAddPause_Click(object sender, EventArgs e) {
            AddPause(pathToPause);
        }

        private void AddPause(string pathToPause) {
            Sample m = new Sample(pathToPause);
            SampleControl sc = new SampleControl(m, panelSample, new Point(0, verticalControlsPosition));

            trackBar1.ValueChanged += sc.GetSamplePlayer.trackBar_ValueChanged;
            sc.GetSamplePlayer.Volume = trackBar1.Value * 0.01f;
            sampleControls.Add(sc);

            verticalControlsPosition += sc.MinimumSize.Height + verticalDistanceBeetwenControls;

            MyMessageBox mmb = new MyMessageBox("Пауза загружена!", false);
            mmb.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            pathToPause = pauses[comboBox1.SelectedIndex];
        }
    }
}
