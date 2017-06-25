using Microsoft.Win32;
using SimpleAudioEditor.Controller;
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
        private Primary pr = new Primary();

        public IntroForm(NewPlayerForm main)
        {
            this.main = main;
            InitializeComponent();

            Primary pr = new Primary();
            if(pr.progPath == "nopath")
            {

            } else
            {
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
                labelProjectsPath.Text = "Путь с проектами:  "+pr.progPath;
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
                //Directory.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundFactory", true);
                DrawFolders();
            }
            catch (Exception ex)
            {

            }
        }
        private void DrawFolders()
        {
            if (pr.projects.Count > 0)
            {
                foreach (Project p in pr.projects)
                {
                    Console.WriteLine(p.title);
                    ProjectButton btn = new ProjectButton(p);
                    
                    btn.Click += btnExistingProject_Click;
                    layoutProjects.Controls.Add(btn);
                    layoutProjects.Refresh();
                }
            }
        }
        private void btnExistingProject_Click(object sender, EventArgs e)
        {
            ProjectButton p = sender as ProjectButton;
            MessageBox.Show(p.pr.title);
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                pr.SetProgrammPath(f.SelectedPath);
                DrawFolders();
                labelProjectsPath.Text = "Путь с проектами:  " + pr.progPath;
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
            }
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            Project pr = new Project();

            if ((new WriteProjectNameForm(pr).ShowDialog()) == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                main.project = pr;
                main.Show();
                this.Dispose();
            }
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
