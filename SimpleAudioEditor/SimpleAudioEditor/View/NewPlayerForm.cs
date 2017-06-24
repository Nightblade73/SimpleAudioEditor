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
            m = new MainSoundLine(660, 75, panelMain, new Point(0, 0));
        }
        int x = 6;
        private void buttonAddSample_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Cursor Files|*.mp3;*.wav";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    SoundLineEditor s = new SoundLineEditor(ofd.FileNames[i], panelSamples, new Point(6, x), 500);
                    x += 46;
                }
                MessageBox.Show("загружено");

            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            if ((new IntroForm(this).ShowDialog()) != DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
