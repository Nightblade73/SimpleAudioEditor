using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SimpleAudioEditor.Properties;
using SimpleAudioEditor.Controller.WaveController;

namespace SimpleAudioEditor.PeachStudio.View
{
    public partial class LatestSample : UserControl
    {
        public String file;
        private Sample sample;
        private SamplePlayer player;
        public LatestSample(String file)
        {
            this.file = file;
            //player = new SamplePlayer(file);
            InitializeComponent();
            String text = Path.GetFileName(file);
            //text = text.Substring(0, Math.Min(text.Length, 20));
            this.labelName.Text = text;
            btnPlayStop.Enabled = false;
            btnPlayStop.Visible = false;
        }

        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if(btn.AccessibleName == "play")
            {
                btn.BackgroundImage = new Bitmap(Resources.icons8_Pause_48);
                btn.AccessibleName = "stop";
                player.Play();
            } else
            {
                btn.BackgroundImage = new Bitmap(Resources.icons8_Play_26);
                btn.AccessibleName = "play";
                player.Stop();
            }
        }
    }
}
