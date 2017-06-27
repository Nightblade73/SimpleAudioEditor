using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio {
    public partial class ProjectControl : UserControl {
        Project project;

        public Project CurrentProject
        {
            set { project = value; }
            get { return project; }
        }
        ProjectPlayer projectPlayer;
        public ProjectControl() {
            InitializeComponent();
            project = new Project();
        }

        public void SplitAll() { }
    }
}
