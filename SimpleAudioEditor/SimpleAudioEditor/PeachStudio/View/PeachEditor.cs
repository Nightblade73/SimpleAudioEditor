using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio.View
{
    public partial class PeachEditor : Form
    {
        List<SampleControl> sampleControls;
        public Project project;

        public PeachEditor(Project _project)
        {
            InitializeComponent();

            project = _project;
            panelSample.HorizontalScroll.Enabled = false;
            panelSample.HorizontalScroll.Visible = false;
            panelSample.HorizontalScroll.Visible = false;
            panelSample.HorizontalScroll.Maximum = 0;
            panelSample.HorizontalScroll.LargeChange = 0;
            panelSample.HorizontalScroll.SmallChange = 0;
            panelSample.AutoScroll = true;
            sampleControls = new List<SampleControl>();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        int y = 0;
        private void buttonAddSample_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Cursor Files|*.mp3;*.wav";
        //    ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Sample m = new Sample(ofd.FileName);
                SampleControl sc = new SampleControl(m, panelSample, new Point(0, y), new Size(400, 93));

                trackBar1.ValueChanged += sc.GetSamplePlayer.trackBar_ValueChanged;
                sc.GetSamplePlayer.Volume = trackBar1.Value * 0.01f;
                sampleControls.Add(sc);

                y += 99;
            }
        }

        private void panelSample_ControlAdded(object sender, ControlEventArgs e)
        {


        }

        private void panelSample_Layout(object sender, LayoutEventArgs e)
        {
            panelSupport.Size = new Size(panelSupport.Size.Width,panelSample.Size.Height+2);
        }

        private void PeachEditor_Load(object sender, EventArgs e) {
            trackBar1.BackColor = Color.FromArgb(249,245,224);
        }

        private void PeachEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
           //this.Parent.Show();
        }
    }
}
