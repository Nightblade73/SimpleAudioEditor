using Microsoft.Win32;
using SimpleAudioEditor.Controller;
using SimpleAudioEditor.PeachStudio;
using SimpleAudioEditor.PeachStudio.View;
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
        public PeachEditor main;

        public IntroForm(PeachEditor main)
        {
            InitializeComponent();
            this.main = main;

            //labelProjectsPath.Controls.Add(labelChangeProgPath);
            //labelChangeProgPath.Parent = labelProjectsPath;
           // labelChangeProgPath.Dock = DockStyle.Right;
            //labelChangeProgPath.Anchor = AnchorStyles.Right;
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
                main.project = Project.CreateTempProject(main.primary + "\\" + form.title);
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }
        private void btnExistingProject_Click(object sender, EventArgs e)
        {
            ProjectButton p = sender as ProjectButton;
            main.project = p.pr;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            ChooseProgramPath();
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
                layoutProjects.Controls.Clear();
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

        private void IntroForm_Load(object sender, EventArgs e)
        {

        }

        private void labelChangeProgPath_Click(object sender, EventArgs e)
        {
            ChooseProgramPath();
        }
        private void ChooseProgramPath()
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
        private void labelProjectsPath_Resize(object sender, EventArgs e)
        {
            Label l = sender as Label;
            labelChangeProgPath.Left = l.Width;
        }
    }
}
