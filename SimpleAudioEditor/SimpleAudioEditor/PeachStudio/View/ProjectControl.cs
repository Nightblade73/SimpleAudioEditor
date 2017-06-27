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
        int OffsetX;
        TimeSpan currentTime;

        public Project CurrentProject
        {
            set { project = value; }
            get { return project; }
        }

        private int TimePerPoint(TimeSpan time)
        {
            return 0;
        }

        private int PlayerLineWidth {
            get { return pbWaveViewer.Width; }
        }

        public ProjectControl() {
            InitializeComponent();
            project = new Project();
            //markerPoint = new Point(startPos.X, startPos.Y - object_radius * 2);
            UpdatePointPos();
            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(startPos.X, PlayerLineWidth, GetAllTotalTime()), new TimeSpan(), GetAllTotalTime()), GetAllTotalTime(), PlayerLineWidth), startPos.Y - object_radius * 2);

            pbWaveViewer.Invalidate();
        }

        public ProjectControl(Project project) {
            InitializeComponent();
            this.project = project;
            UpdatePointPos();
            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(startPos.X, PlayerLineWidth, GetAllTotalTime()), new TimeSpan(), GetAllTotalTime()), GetAllTotalTime(), PlayerLineWidth), startPos.Y - object_radius * 2);

            pbWaveViewer.Invalidate();
        }

        private void UpdatePointPos() {
            startPos = new Point(0, pbWaveViewer.Height / 2);
            endPos = new Point(pbWaveViewer.Width, pbWaveViewer.Height / 2);
            
            if (projectPlayer != null)
                markerPoint = new Point(Mathf.TimeToPos(Mathf.Clamp(currentTime, new TimeSpan(), GetAllTotalTime()), GetAllTotalTime(), PlayerLineWidth), startPos.Y - object_radius * 2);
        }

        public void SplitAll() { }

        private void pbWaveViewer_Paint(object sender, PaintEventArgs e) {            
            int penSize = 3;
            Pen grayPen = new Pen(Color.Gray, penSize);

            

            Graphics canvas = e.Graphics;

            TimeSpan time = new TimeSpan();
            foreach (var s in project.GetSampleList())
            {
                canvas.DrawLine(new Pen(Color.Blue, 5),
                   new Point(Mathf.TimeToPos(time, GetAllTotalTime(), PlayerLineWidth), startPos.Y),
                   new Point(Mathf.TimeToPos(time + s.SplitEndTime - s.SplitStartTime, GetAllTotalTime(), PlayerLineWidth), startPos.Y));
                time += s.SplitEndTime - s.SplitStartTime;
            }

            canvas.DrawLine(grayPen, startPos, endPos);

            Pen cursorPen = new Pen(Color.Black, penSize);
            //Рисуем маркер
            cursorPen.Color = Color.Black;
            canvas.DrawLine(cursorPen, startPos, new Point(markerPoint.X, startPos.Y));

            canvas.DrawPolygon(cursorPen, new Point[] {
                new Point( markerPoint.X - object_radius, markerPoint.Y - object_radius),
                new Point( markerPoint.X + object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X,startPos.Y)});        
        }

        private void ProjectControl_Load(object sender, EventArgs e)
        {
            currentTime = new TimeSpan(0, 0, 12);
            pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_NotDown;
            pbWaveViewer.MouseDown += pbWaveViewer_MouseDown;
            pbWaveViewer.DragEnter += pbWaveViewer_DragEnter;
            pbWaveViewer.DragDrop += pbWaveViewer_DragDrop;

            UpdatePointPos();
            (pbWaveViewer as Control).AllowDrop = true;
            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(startPos.X, PlayerLineWidth, GetAllTotalTime()), new TimeSpan(), GetAllTotalTime()), GetAllTotalTime(), PlayerLineWidth), startPos.Y - object_radius * 2);

            pbWaveViewer.Invalidate();
        }

        #region "Moving Marker"
        // Мы двигаем маркер точку.
        private void pbWaveViewer_MouseMove_MovingMarker(object sender, MouseEventArgs e) {

            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(e.X + OffsetX, PlayerLineWidth, GetAllTotalTime()), new TimeSpan(), GetAllTotalTime()), GetAllTotalTime(), PlayerLineWidth), startPos.Y - object_radius * 2);
            //UpdateMaskedTimeValue();
            // Перерисовать.
            pbWaveViewer.Invalidate();
        }

        // Остановить перемещение конечной точки.
        protected void pbWaveViewer_MouseUp_MovingMarker(object sender, MouseEventArgs e) {
            // Сброс обработчиков событий.
            pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_NotDown;
            pbWaveViewer.MouseMove -= pbWaveViewer_MouseMove_MovingMarker;
            pbWaveViewer.MouseUp -= pbWaveViewer_MouseUp_MovingMarker;
            currentTime = Mathf.PosToTime(markerPoint.X, PlayerLineWidth, GetAllTotalTime());
            maskedTextBoxCurrentTime.Text = currentTime.ToString(@"hh\:mm\:ss\.FF");

            markerMoving = false;
            // Перерисовать.
            pbWaveViewer.Invalidate();
        }

        private void pbWaveViewer_MouseMove_NotDown(object sender, MouseEventArgs e) {
            Cursor new_cursor = Cursors.Arrow;

            Point hit_point;
            if (MouseIsOverMarker(e.Location, out hit_point))
                new_cursor = Cursors.VSplit;  
            // Установим новый курсор.
            if (pbWaveViewer.Cursor != new_cursor)
                pbWaveViewer.Cursor = new_cursor;
        }

        private void pbWaveViewer_MouseDown(object sender, MouseEventArgs e) {

            Point hit_point;

            if (MouseIsOverMarker(e.Location, out hit_point)) {
                // Начните перемещать эту конечную точку.
                pbWaveViewer.MouseMove -= pbWaveViewer_MouseMove_NotDown;
                pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_MovingMarker;
                pbWaveViewer.MouseUp += pbWaveViewer_MouseUp_MovingMarker;
                markerMoving = true;
                //UpdateMaskedTimeValue();
                // Запомните смещение от мыши до точки.
                OffsetX = hit_point.X - e.X;
            }
        }

        protected bool MouseIsOverMarker(Point mouse_pt, out Point hit_pt) {

            if (FindDistanceToPointSquared(mouse_pt, markerPoint) < over_dist_squared * 2) {
                hit_pt = markerPoint;
                return true;
            } else {
                hit_pt = new Point(-1, -1);
                return false;
            }

        }      

        protected int FindDistanceToPointSquared(Point pt1, Point pt2) {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        protected int FindDistanceToSplitPointSquared(Point pt1, Point pt2) {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt1.Y;
            return dx * dx + dy * dy;
        }
        #endregion // Перемещение конечной точки

        protected void pbWaveViewer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        protected void pbWaveViewer_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Sample)) as Sample != null)
            {
                Sample s = e.Data.GetData(typeof(Sample)) as Sample;
                project.GetSampleList().Add(s);
            }
            pbWaveViewer.Invalidate();
        }


        private void pbWaveViewer_Layout(object sender, LayoutEventArgs e)
        {
            UpdatePointPos();
            pbWaveViewer.Invalidate();
        }

        private TimeSpan GetAllTotalTime() {
            TimeSpan totalTime = new TimeSpan();
            foreach(var i in project.GetSampleList()) {
                totalTime += i.SplitEndTime-i.SplitStartTime;
            }
            return totalTime;
        }



    }
}
