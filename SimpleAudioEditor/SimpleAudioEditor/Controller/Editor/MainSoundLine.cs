using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Forms;
using System.Drawing;
using SimpleAudioEditor.Controller.WaveController;
using System.IO;

namespace SimpleAudioEditor.Controller.Editor
{
    class MainSoundLine
    {
        PictureBox pictureBox;
        Point lineStartPos, lineEndPos;
        int leghtLine;
        double maxLeghtOutFromSecond = 300;
        Button buttonPlay;
        Button buttonStop;
        Button buttonOK;
        Button buttonDelete;
        Random r = new Random();
        Project project;

        public MainSoundLine(int width, int height, Control parent, Point location, Project _project)
        {     
            project = _project;

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(width - height * 3 - 10 - 10, height);

            pictureBox.Parent = parent;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.DragEnter += pictureBox_DragEnter;
            pictureBox.DragDrop += pictureBox_DragDrop;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseDoubleClick += pictureBox_MouseDoubleClick;

            buttonPlay = new Button();
            buttonPlay.Size = new System.Drawing.Size(pictureBox.Size.Height / 2, pictureBox.Size.Height / 2);
            buttonPlay.Location = new Point(location.X, location.Y + height - buttonPlay.Size.Width - 20);
            buttonPlay.Parent = parent;
            //buttonPlay.Click += buttonPlay_Click;
            buttonPlay.Text = ">";
            buttonPlay.BackColor = Color.Coral;
            buttonPlay.FlatStyle = FlatStyle.Flat;


            buttonStop = new Button();
            buttonStop.Size = buttonPlay.Size;
            buttonStop.Location = new Point(buttonPlay.Location.X + buttonPlay.Size.Width, buttonPlay.Location.Y);
            // buttonStop.Click += buttonStop_Click;
            buttonStop.Parent = parent;
            buttonStop.Text = "■";
            buttonStop.BackColor = Color.Coral;
            buttonStop.FlatStyle = FlatStyle.Flat;

            buttonOK = new Button();
            buttonOK.Size = buttonPlay.Size;
            buttonOK.Location = new Point(buttonStop.Location.X + buttonPlay.Size.Width + pictureBox.Size.Width + 20, buttonPlay.Location.Y);
            buttonOK.Click += buttonOK_Click;
            buttonOK.Parent = parent;
            buttonOK.Text = "✔";
            buttonOK.BackColor = Color.Coral;
            buttonOK.FlatStyle = FlatStyle.Flat;

            buttonDelete = new Button();
            buttonDelete.Size = buttonPlay.Size;
            buttonDelete.Location = new Point(buttonOK.Location.X + buttonPlay.Size.Width, buttonPlay.Location.Y);
            buttonDelete.Click += buttonDelete_Click;
            buttonDelete.Parent = parent;
            buttonDelete.Text = "×";
            buttonDelete.BackColor = Color.Coral;
            buttonDelete.FlatStyle = FlatStyle.Flat;

            pictureBox.Location = new Point(buttonStop.Location.X + buttonStop.Size.Width + 10, location.Y - 25);

            (pictureBox as Control).AllowDrop = true;
            lineStartPos = new Point(0, pictureBox.Size.Height - 10);
            lineEndPos = new Point(width, pictureBox.Size.Height - 10);
            leghtLine = lineEndPos.X - lineStartPos.X;
            pictureBox.Invalidate();
        }

        protected int SetIndexQueue(Point pt1)
        {
            int index = 0;
            int indexQueue = 0;

            SetSegmentEndPoints();
            int dx = int.MaxValue;
            if (project.listSamples.Count > 0)
            {
                for (int i = 0; i < project.listSamples.Count; i++)
                {
                    if (dx >= Math.Max(pt1.X, project.listSamples[i].startPos.X) - Math.Min(pt1.X, project.listSamples[i].startPos.X))
                    {
                        dx = Math.Max(pt1.X, project.listSamples[i].startPos.X) - Math.Min(pt1.X, project.listSamples[i].startPos.X);
                        index = i;
                    }
                }


                if (Math.Max(project.listSamples[index].startPos.X, pt1.X) - Math.Min(project.listSamples[index].startPos.X, pt1.X) <
                    Math.Max(project.listSamples[index].endPos.X, pt1.X) - Math.Min(project.listSamples[index].endPos.X, pt1.X))
                {

                    indexQueue = project.listSamples[index].IndexQueue;
                    for (int i = index; i < project.listSamples.Count; i++)
                    {
                        project.listSamples[i].IndexQueue += 1;
                    }
                }
                else
                {
                    indexQueue = project.listSamples[index].IndexQueue + 1;
                    for (int i = index + 1; i < project.listSamples.Count; i++)
                    {
                        project.listSamples[i].IndexQueue += 2;
                    }
                }

            }


            return indexQueue;

            /*
                        int dx = pt1.X - pt2.X;
                        int dy = pt1.Y - pt2.Y;
                        return dx * dx + dy * dy;
                        */
        }

        protected void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        public double FinalLeght()
        {
            double sum = 0;
            for (int i = 0; i < project.listSamples.Count(); i++)
            {
                sum += project.listSamples[i].LeghtFromSecond;
            }

            return sum;
        }

        protected void buttonOK_Click(object sender, EventArgs e)
        {
            MessageBox.Show(project.Save());

        }

        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            project.listSamples.Clear();
            //
            Directory.Delete(project.path, true);
            Directory.CreateDirectory(project.path);
            pictureBox.Invalidate();
        }

        protected void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            SetSegmentEndPoints();
            int index = SetIndexQueue(pictureBox.PointToClient(new Point(e.X, e.Y)));
            Sample s = e.Data.GetData(typeof(Sample)) as Sample;

            if (FinalLeght() + s.LeghtFromSecond <= maxLeghtOutFromSecond)
            {
                // MessageBox.Show(""+s.indexQueue);
                project.listSamples.Add(s);
                s.IndexQueue = index;
                SetSegmentEndPoints();
            }
            else
            {
                MessageBox.Show(String.Format("Выходной файл не может привышать {0} минут! Лимит привышен на {1} секунд!", maxLeghtOutFromSecond / 60, FinalLeght() + s.LeghtFromSecond - maxLeghtOutFromSecond));
            }
            pictureBox.Invalidate();
        }

        private void SetSegmentEndPoints()
        {
            project.listSamples.Sort();

            int x = 0;
            for (int i = 0; i < project.listSamples.Count(); i++)
            {
                int x1 = x;
                project.listSamples[i].startPos = new Point(x, lineStartPos.Y);
                x = x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond);
                project.listSamples[i].endPos = new Point(x, lineStartPos.Y);

            }


        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Посмотрим, чем мы закончили.
            int segment_number;

            if (MouseIsOverSegment(e.Location, out segment_number))
            {

                // Начните перемещение этого сегмента.
                pictureBox.MouseMove -= pictureBox_MouseMove_NotDown;
                pictureBox.MouseMove += pictureBox_MouseMove_MovingSegment;
                pictureBox.MouseUp += pictureBox_MouseUp_MovingSegment;

                // Запомните номер сегмента.
                MovingSegment = segment_number;
            }

        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Посмотрим, чем мы закончили.
            int segment_number;

            if (MouseIsOverSegment(e.Location, out segment_number))
            {
                // Запомните номер сегмента.
                MovingSegment = segment_number;
                project.listSamples.RemoveAt(MovingSegment);
                SetSegmentEndPoints();
                pictureBox.Invalidate();
            }

        }


        // The mouse is up. Посмотрите, находимся ли мы над конечной точкой или сегментом.
        private void pictureBox_MouseMove_NotDown(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Arrow;

            // Посмотрим,над чем мы находимся.
            int segment_number;

            if (MouseIsOverSegment(e.Location, out segment_number))
                new_cursor = Cursors.Hand;

            // Установим новый курсор.
            if (pictureBox.Cursor != new_cursor)
                pictureBox.Cursor = new_cursor;
        }

        #region "Moving Segment"
        // «Размер» объекта для мыши над целями.
        private const int object_radius = 3;

        // Мы над объектом, если квадрат расстояния
        // между мышью и объектом меньше этого.
        private const int over_dist_squared = object_radius * object_radius;

        // Сегмент, который мы двигаем, или сегмент, конечная точка которого мы двигаемся.
        private int MovingSegment = -1;
        public int OffsetX;

        // We're moving a segment.
        private void pictureBox_MouseMove_MovingSegment(object sender, MouseEventArgs e)
        {

            // See how far the first point will move.
            Point pt1 = new System.Drawing.Point(e.X, e.Y);


            int indexQueue = SetIndexQueue(pt1);

            project.listSamples[MovingSegment].IndexQueue = indexQueue;

            SetSegmentEndPoints();
            for (int i = 0; i < project.listSamples.Count; i++)
            {
                if (project.listSamples[i].IndexQueue == indexQueue)
                {
                    MovingSegment = i;
                }
            }
            // Move the segment to its new location.
            // Redraw.
            pictureBox.Invalidate();
        }
        // Stop moving the segment.
        private void pictureBox_MouseUp_MovingSegment(object sender, MouseEventArgs e)
        {
            // Reset the event handlers.
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseMove -= pictureBox_MouseMove_MovingSegment;
            pictureBox.MouseUp -= pictureBox_MouseUp_MovingSegment;
            SetSegmentEndPoints();
            MovingSegment = -1;
            // Redraw.
            pictureBox.Invalidate();
        }

        #endregion // Moving End Point
        private bool MouseIsOverSegment(Point mouse_pt, out int segment_number)
        {
            for (int i = 0; i < project.listSamples.Count; i++)
            {
                // Посмотрим, перешли ли мы над сегментом.
                PointF closest;
                if (FindDistanceToSegmentSquared(
                    mouse_pt, project.listSamples[i].startPos, project.listSamples[i].endPos, out closest)
                        < over_dist_squared)
                {
                    // Мы над этим сегментом.
                    segment_number = i;
                    return true;
                }
            }

            segment_number = -1;
            return false;
        }

        private double FindDistanceToSegmentSquared(Point pt, Point p1, Point p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // Это точка, а не сегмент линии.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return dx * dx + dy * dy;
            }

            // Вычислим t, который минимизирует расстояние.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // Посмотрим, представляет ли это один из сегментов
            // конечные точки или точка в середине.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return dx * dx + dy * dy;
        }

        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int penWidth = 3;
            Pen greyPen = new Pen(Color.Gray, 3);
            Pen darkGreyPen = new Pen(Color.Black, 3);

            Pen orangePen = new Pen(Color.Orange, penWidth);
            e.Graphics.DrawLine(greyPen, lineStartPos, lineEndPos);

            //SetSegmentEndPoints();
            int x = 0;

            if (project.listSamples.Count() > 0)
            {
                for (int i = 0; i < project.listSamples.Count(); i++)
                {
                    //   MessageBox.Show("" + listSegment[i].indexQueue);
                    // listSegment[i].LeghtFromSecond;
                    //     MessageBox.Show(""+(listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond));
                    if (MovingSegment != i)
                    {
                        e.Graphics.DrawLine(orangePen, new Point(x, lineStartPos.Y)

                        , new Point(x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond), lineStartPos.Y));
                        e.Graphics.DrawLine(orangePen, new Point(x + penWidth / 2, lineStartPos.Y + 5), new Point(x + penWidth / 2, lineStartPos.Y - 5));
                        e.Graphics.DrawLine(darkGreyPen, new Point(x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y + 5), new Point(x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y - 5));


                    }
                    else
                    {
                        Pen colorRed = new Pen(Color.OrangeRed, 3);
                        e.Graphics.DrawLine(colorRed, new Point(x, lineStartPos.Y)
                                        , new Point(x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond), lineStartPos.Y));

                        e.Graphics.DrawLine(colorRed, new Point(x + penWidth / 2, lineStartPos.Y + 5), new Point(x + penWidth / 2, lineStartPos.Y - 5));
                        e.Graphics.DrawLine(colorRed, new Point(x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y + 5), new Point(x + (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y - 5));

                    }



                    x += (int)(leghtLine * project.listSamples[i].LeghtFromSecond / maxLeghtOutFromSecond);

                }
            }
        }
    }
}
