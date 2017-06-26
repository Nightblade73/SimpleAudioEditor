using SimpleAudioEditor.Controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.View
{
    public class SampleLineEditor : Panel
    {
        Sample samp;

        public SampleLineEditor(Sample s)
        {
            samp = s;
            this.Size = new System.Drawing.Size(675, 100);
            this.BorderStyle = BorderStyle.Fixed3D;
            

            Label l = new Label();
            l.Width = 400;
            l.Text = s.SoundPath;
            Console.WriteLine(s.SoundPath);
            this.Controls.Add(l);

            Button btnPlayPause = new Button();
            btnPlayPause.Location = new System.Drawing.Point(5, 20);
            btnPlayPause.Height = 30;
            btnPlayPause.Width = 30;
            btnPlayPause.Text = "▶";
            btnPlayPause.AccessibleName = "play";
            btnPlayPause.BackColor = Color.OrangeRed;
            btnPlayPause.Parent = this;
            btnPlayPause.Click += btnPlayPause_Click;
            this.Controls.Add(btnPlayPause);

            Button btnStop = new Button();
            btnStop.Location = new System.Drawing.Point(5, 55);
            btnStop.Height = 30;
            btnStop.Width = 30;
            btnStop.Text = "■";
            btnStop.Click += btnStop_Click;
            btnStop.BackColor = Color.OrangeRed;
            this.Controls.Add(btnStop);

        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SampleLineEditor s = btn.Parent as SampleLineEditor;
            s.samp.Stop();
        }
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SampleLineEditor s = btn.Parent as SampleLineEditor;
            if(btn.AccessibleName == "play")
            {
                btn.AccessibleName = "pause";
                btn.Text = "Ⅱ";
                s.samp.Play();
            } else
            {
                btn.AccessibleName = "play";
                btn.Text = "▶";
                s.samp.Pause();
            }
            
            //MessageBox.Show(s.samp.SoundPath);
        }
    }
}
