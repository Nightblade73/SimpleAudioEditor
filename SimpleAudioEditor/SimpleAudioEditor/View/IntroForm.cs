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

        public IntroForm(NewPlayerForm main)
        {
            InitializeComponent();
            this.main = main;

            if (main.primary.progPath != "nopath")
            {
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
                labelProjectsPath.Text = "Путь с проектами:  " + main.primary.progPath;
                DrawFolders();
            }
        }
        private void btnNewProject_Click(object sender, EventArgs e)
        {
            WriteProjectNameForm form = new WriteProjectNameForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Project pr = new Project(form.title, main.primary);
                main.project = pr;
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }
        private void btnExistingProject_Click(object sender, EventArgs e)
        {
            ProjectButton p = sender as ProjectButton;
            main.project = p.pr; // сюда десириалайзер
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                main.primary.SetProgrammPath(f.SelectedPath);
                DrawFolders();
                labelProjectsPath.Text = "Путь с проектами:  " + main.primary.progPath;
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
            }
        }
        private void btnPlaySample_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.AccessibleName == "stop")
            {
                btn.BackgroundImage = new Bitmap(Resources.icons8_Pause_48);
                btn.AccessibleName = "play";
            }
            else
            {
                btn.AccessibleName = "stop";
                btn.BackgroundImage = new Bitmap(Resources.icons8_Play_26);
            }
        }
        private void DrawFolders()
        {
            if (main.primary.projects.Count > 0)
            {
                foreach (Project p in main.primary.projects)
                {
                    Console.WriteLine(p.title);
                    ProjectButton btn = new ProjectButton(p);

                    btn.Click += btnExistingProject_Click;
                    layoutProjects.Controls.Add(btn);
                    layoutProjects.Refresh();
                }
            }
        }
    }
}
