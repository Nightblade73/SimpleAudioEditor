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
        ProjectPlayer projectPlayer;

        public ProjectControl() {
            InitializeComponent();
        }

        public ProjectControl(Project project) {
            InitializeComponent();
            this.project = project;
        }

        public void SplitAll() { }

        private void ProjectControl_Load(object sender, EventArgs e)
        {

        }
    }
}
