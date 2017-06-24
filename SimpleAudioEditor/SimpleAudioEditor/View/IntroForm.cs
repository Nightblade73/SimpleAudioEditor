using Microsoft.Win32;
using SimpleAudioEditor.Properties;
using SimpleAudioEditor.View;
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
        public NewPlayerForm main;
        private bool btnSampleState = false;

        public IntroForm(NewPlayerForm main)
        {
            this.main = main;
            InitializeComponent();
            String str = Registry_GetPath();
            if (Registry_GetPath() == "nopath")
            {

            }
            else
            {
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
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

        private void IntroForm_Load(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundFactory", true);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Registry_SetPath(f.SelectedPath);
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
            }
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            main.Show();
            this.Dispose();
        }

        private void btnPlaySample_Click(object sender, EventArgs e)
        {
            if (btnSampleState)
            {
                btnSampleState = !btnSampleState;
                btnPlaySample.BackgroundImage = new Bitmap(Resources.icons8_Pause_48);
            }
            else
            {
                btnSampleState = !btnSampleState;
                btnPlaySample.BackgroundImage = new Bitmap(Resources.icons8_Play_26);
            }
        }

        private RegistryKey Registry_GetKey()
        {
            Microsoft.Win32.RegistryKey key;
            String[] subkeys = Microsoft.Win32.Registry.CurrentUser.GetSubKeyNames();
            try
            {
                key = Registry.CurrentUser.OpenSubKey("Software\\SimpleAudioEditor", true);
                Console.WriteLine(key.Name);
            }
            catch (NullReferenceException ex)
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\SimpleAudioEditor");
                key.SetValue("path", "nopath", RegistryValueKind.String);
            }
            return key;
        }
        private String Registry_GetPath()
        {
            RegistryKey key = Registry_GetKey();
            String str = key.GetValue("path").ToString();
            key.Close();
            return str;
        }
        private void Registry_SetPath(String path)
        {
            RegistryKey key = Registry_GetKey();
            key.SetValue("path", path);
            key.Close();
        }
    }
}
