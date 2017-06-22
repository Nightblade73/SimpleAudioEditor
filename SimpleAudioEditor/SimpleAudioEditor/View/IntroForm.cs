using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor
{
    public partial class IntroForm : Form
    {
        public MainForm main;

        public IntroForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void IntroForm_Load(object sender, EventArgs e)
        {
            try
            {
                File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SoundFactory");
            }
            catch (Exception ex)
            {
                MessageBox.Show("не удалось удалить папку SoundFactory. " + ex.ToString());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonToMain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            main.Show();
            this.Dispose();
        }

        private void IntroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
