using SimpleAudioEditor.Controller.Editor;
using System;
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
        public NewPlayerForm()
        {
            InitializeComponent();
            m = new MainSoundLine(660, panelMain, new Point(0, 0));
        }
        int x = 6;
        private void buttonAddSample_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "MP3 Files|*.mp3";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SoundLineEditor s = new SoundLineEditor(ofd.FileName, panelSamples, new Point(6, x), 500);
                x += 46;
            }
        }
    }
}
