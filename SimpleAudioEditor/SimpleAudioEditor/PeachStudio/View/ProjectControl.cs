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
        // «Размер» объекта для мыши над целями.
        private const int object_radius = 6;
        // Мы над объектом, если квадрат расстояния
        // между мышью и объектом меньше этого.
        private const int over_dist_squared = object_radius * object_radius;

        Project project;
        ProjectPlayer projectPlayer;

        Point startPos, endPos;
        Point markerPoint;
        bool markerMoving;

        public Project CurrentProject
        {
            set { project = value; }
            get { return project; }
        }
        
        public ProjectControl() {
            InitializeComponent();
            project = new Project();
            markerPoint = new Point(startPos.X, startPos.Y - object_radius * 2);
        }

        public ProjectControl(Project project) {
            InitializeComponent();
            this.project = project;
            markerPoint = new Point(startPos.X, startPos.Y - object_radius * 2);
        }

        public void SplitAll() { }

        private void ProjectControl_Load(object sender, EventArgs e)
        {

        }
    }
}
