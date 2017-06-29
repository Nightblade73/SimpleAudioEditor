using Microsoft.Win32;
using SimpleAudioEditor.Controller;
using SimpleAudioEditor.PeachStudio;
using SimpleAudioEditor.PeachStudio.View;
using SimpleAudioEditor.PeachStudio.WorkMethods;
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
        private Primary primary = new Primary();
        private System.Windows.Forms.Control tmpNew;

        public IntroForm()
        {
            InitializeComponent();
            tmpNew = layoutProjects.Controls.Find("btnNewProject", false)[0];
            if (primary.progPath != "nopath")
            {
                panelSamples.Enabled = true;
                panelPath.Visible = false;
                layoutProjects.Enabled = true;
                labelProjectsPath.Text = "Путь с проектами:  " + primary.progPath;
                DrawFolders();
            }
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            WriteProjectNameForm form = new WriteProjectNameForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                PeachEditor pe = new PeachEditor(Project.CreateTempProject(primary.progPath + "\\" + form.title));
                this.Hide();
                pe.ShowDialog();
                this.Show();
            }
        }
        private void btnExistingProject_Click(object sender, EventArgs e)
        {
            ProjectButton p = sender as ProjectButton;
            Project needP = WorkerXML.Deserialize(p.pr.GetProjectPath());
            PeachEditor pe = new PeachEditor(needP);
            this.Hide();
            pe.ShowDialog();
            this.Show();
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
            if (primary.projects.Count > 0)
            {
                //layoutProjects.Controls.Clear();
                layoutProjects.Controls.Add(tmpNew);

                foreach (Project p in primary.projects)
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
            if (Directory.Exists(primary.progPath))
            {
                f.SelectedPath = primary.progPath;
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (primary.SetProgrammPath(f.SelectedPath))
                {
                    DrawFolders();
                    labelProjectsPath.Text = "Путь с проектами:  " + primary.progPath;
                    panelSamples.Enabled = true;
                    panelPath.Visible = false;
                    layoutProjects.Enabled = true;
                } else
                {
                    MessageBox.Show("Нет доступа к папке");
                    labelProjectsPath.Text = "Путь с проектами:  none";
                    panelSamples.Enabled = false;
                    panelPath.Visible = true;
                    layoutProjects.Enabled = false;
                }
            }
        }
        private void labelProjectsPath_Resize(object sender, EventArgs e)
        {
            Label l = sender as Label;
            labelChangeProgPath.Left = l.Width;
        }
    }
}
