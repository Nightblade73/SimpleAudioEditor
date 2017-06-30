using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio.View {
    public partial class PeachEditor : Form {
        List<SampleControl> sampleControls;
        public Project project;
        string pathToPause = "";
        int verticalDistanceBeetwenControls = 5;
        int verticalControlsPosition = 0;
        private List<string> pauses;


        public PeachEditor(Project _project) {
            project = _project;
                   
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
            project = new Project();

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

        private void getPauses() {

            //System.Resources.ResourceManager RM = new System.Resources.ResourceManager("SimpleAudioEditor.Properties.Resources", typeof(Properties.Resources).Assembly);

            //var file = (System.IO.MemoryStream)RM.GetObject("Aero");

            try {
                pauses = new List<string>();
                List<UnmanagedMemoryStream> pausesMS = new List<UnmanagedMemoryStream>();
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.Aero);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.bmw);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.kolokol);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.sec5);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.sec51);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.sec7);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.vinil);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.vinil2);
                pausesMS.Add(SimpleAudioEditor.Properties.Resources.wind);

                int counter = 0;

                foreach(UnmanagedMemoryStream i in pausesMS) {
                    string path = "pause" + counter + ".wav";

                    using (FileStream file = new FileStream(path, FileMode.Create, System.IO.FileAccess.Write)) {
                        byte[] bytes = new byte[i.Length];
                        i.Read(bytes, 0, (int)i.Length);
                        file.Write(bytes, 0, bytes.Length);
                        i.Close();
                    }
                    
                    counter++;

                    comboBox1.Items.Add(path);
                    pauses.Add(path);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            projectControl.ProjectPlayerVolume = trackBar1.Value * 0.01f;
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
            this.Text = "PeachStudio: " + project.title;

            trackBar1.BackColor = Color.FromArgb(43, 43, 43);
            projectControl.ProjectPlayerVolume = trackBar1.Value * 0.01f;

            try {
                getPauses();
                comboBox1.SelectedIndex = 0;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
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
            pathToPause = pauses[comboBox1.SelectedIndex].ToString();
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
           pathToPause = pauses[comboBox1.SelectedIndex].ToString();

        }

        private void PeachEditor_FormClosing(object sender, FormClosingEventArgs e) {
            if (project.isChanged) {
                DialogResult dialog = MessageBox.Show("Сохранить композицию перед закрытием?", "Сохранение перед закрытием", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dialog) {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes: {
                            WorkMethods.WorkMethods.CleanRAWFiles();
                            WorkMethods.WorkMethods.Save(project);
                            WorkMethods.WorkerXML.Serialize(project);
                            MyMessageBox mmb = new MyMessageBox("Сохранено!", false);
                            mmb.ShowDialog();
                            stopAllContolls();
                            Close();
                            break;
                        }
                    case DialogResult.No: break;
                }
            } else {
                stopAllContolls();
            }
        }

        private void stopAllContolls() {
            projectControl.ProjectPlayerStop();
            foreach (var s in sampleControls) {
                s.GetSamplePlayer.Stop();
            }
        }
    }
}
