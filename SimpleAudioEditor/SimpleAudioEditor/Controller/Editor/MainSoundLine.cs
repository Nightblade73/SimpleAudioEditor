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
using SimpleAudioEditor.Model;

namespace SimpleAudioEditor.Controller.Editor
{
    class MainSoundLine
    {
        public List<Segment> listSegment;

        PictureBox pictureBox;
        Point lineStartPos, lineEndPos;
        int leghtLine;
        double maxLeghtOutFromSecond = 300;
        Random r = new Random();
        public MainSoundLine(int width, Control parent, Point localtion)
        {
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(width, 75);
            pictureBox.Location = localtion;
            pictureBox.Parent = parent;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.DragEnter += pictureBox_DragEnter;
            pictureBox.DragDrop += pictureBox_DragDrop;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseDoubleClick += pictureBox_MouseDoubleClick;

            (pictureBox as Control).AllowDrop = true;
            lineStartPos = new Point(0, pictureBox.Size.Height - 10);
            lineEndPos = new Point(width, pictureBox.Size.Height - 10);
            listSegment = new List<Segment>();
            leghtLine = lineEndPos.X - lineStartPos.X;
            pictureBox.Invalidate();
        }



        protected int SetIndexQueue(Point pt1)
        {
            int index = 0;
            int indexQueue = 0;

            SetSegmentEndPoints();
            int dx = int.MaxValue;
            if (listSegment.Count > 0)
            {
                for (int i = 0; i < listSegment.Count; i++)
                {
                    if (dx >= Math.Max(pt1.X, listSegment[i].segmentStartPos.X) - Math.Min(pt1.X, listSegment[i].segmentStartPos.X))
                    {
                        dx = Math.Max(pt1.X, listSegment[i].segmentStartPos.X) - Math.Min(pt1.X, listSegment[i].segmentStartPos.X);
                        index = i;
                    }
                }


                if (Math.Max(listSegment[index].segmentStartPos.X, pt1.X) - Math.Min(listSegment[index].segmentStartPos.X, pt1.X) <
                    Math.Max(listSegment[index].segmentEndPos.X, pt1.X) - Math.Min(listSegment[index].segmentEndPos.X, pt1.X))
                {

                    indexQueue = listSegment[index].indexQueue;
                    for (int i = index; i < listSegment.Count; i++)
                    {
                        listSegment[i].indexQueue += 1;
                    }
                }
                else
                {
                    indexQueue = listSegment[index].indexQueue + 1;
                    for (int i = index + 1; i < listSegment.Count; i++)
                    {
                        listSegment[i].indexQueue += 2;
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
            for (int i = 0; i < listSegment.Count(); i++)
            {
                sum += listSegment[i].LeghtFromSecond;
            }

            return sum;
        }

        protected void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            SetSegmentEndPoints();
            int index = SetIndexQueue(pictureBox.PointToClient(new Point(e.X, e.Y)));
            Segment s = e.Data.GetData(typeof(Segment)) as Segment;

            if (FinalLeght() + s.LeghtFromSecond <= maxLeghtOutFromSecond)
            {
                // MessageBox.Show(""+s.indexQueue);
                listSegment.Add(s);
                s.indexQueue = index;
                SetSegmentEndPoints();
                try
                {
                    CreateSampleFile(s.getFilePath, s.SplitStartTimeFromSecond, s.SplitEndTimeFromSecond, s.getAllTimeFromSecond);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось создать файл-отрезок./n" + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show(String.Format("Выходной файл не может привышать {0} минут! Лимит привышен на {1} секунд!", maxLeghtOutFromSecond / 60, FinalLeght() + s.LeghtFromSecond - maxLeghtOutFromSecond));
            }
            pictureBox.Invalidate();
        }

        private void CreateSampleFile(string fileName, double startPosSample, double endPosSample, double allTime)
        {
            TimeSpan start = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(startPosSample * 1000));
            TimeSpan end = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(endPosSample * 1000));
            TimeSpan all = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(allTime * 1000));
            SampleController sc = new SampleController();
            if (fileName.ToString().Contains(".wav"))
            {
                sc.TrimWavFile(sc.Converter(fileName.ToString()), Params.GetResultCuttedIndexedSoundsPathWAV(), start, all - end);
            }
            else
            {
                sc.TrimWavFile(fileName.ToString(), Params.GetResultCuttedIndexedSoundsPathWAV(), start, all - end);
            }
            Params.IndexCutFilePlus();
        }

        private void SetSegmentEndPoints()
        {
            listSegment.Sort();

            int x = 0;
            for (int i = 0; i < listSegment.Count(); i++)
            {
                int x1 = x;
                listSegment[i].segmentStartPos = new Point(x, lineStartPos.Y);
                x = x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond);
                listSegment[i].segmentEndPos = new Point(x, lineStartPos.Y);

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
                listSegment.RemoveAt(MovingSegment);
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
            //int new_x1 = e.X + OffsetX;

            //int dx = new_x1 - listSegment[MovingSegment].segmentStartPos.X;

            int index = 0;
            int indexQueue = 0;

            SetSegmentEndPoints();
            int dx = int.MaxValue;
            if (listSegment.Count > 0)
            {
                for (int i = 0; i < listSegment.Count; i++)
                {
                    if (i != MovingSegment)
                        if (dx >= Math.Max(pt1.X, listSegment[i].segmentStartPos.X) - Math.Min(pt1.X, listSegment[i].segmentStartPos.X))
                        {
                            dx = Math.Max(pt1.X, listSegment[i].segmentStartPos.X) - Math.Min(pt1.X, listSegment[i].segmentStartPos.X);
                            index = i;
                        }
                }


                if (Math.Max(listSegment[index].segmentStartPos.X, pt1.X) - Math.Min(listSegment[index].segmentStartPos.X, pt1.X) <
                    Math.Max(listSegment[index].segmentEndPos.X, pt1.X) - Math.Min(listSegment[index].segmentEndPos.X, pt1.X))
                {

                    indexQueue = listSegment[index].indexQueue;
                    for (int i = index; i < listSegment.Count; i++)
                    {
                        listSegment[i].indexQueue += 1;
                    }
                }
                else
                {
                    indexQueue = listSegment[index].indexQueue + 1;
                    for (int i = index + 1; i < listSegment.Count; i++)
                    {
                        listSegment[i].indexQueue += 2;
                    }
                }
            }
            /*
                        int dx = pt1.X - pt2.X;
                        int dy = pt1.Y - pt2.Y;
                        return dx * dx + dy * dy;
                        */

            listSegment[MovingSegment].indexQueue = indexQueue;

            SetSegmentEndPoints();
            for (int i = 0; i < listSegment.Count; i++)
            {
                if (listSegment[i].indexQueue == indexQueue)
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
            for (int i = 0; i < listSegment.Count; i++)
            {
                // Посмотрим, перешли ли мы над сегментом.
                PointF closest;
                if (FindDistanceToSegmentSquared(
                    mouse_pt, listSegment[i].segmentStartPos, listSegment[i].segmentEndPos, out closest)
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


            for (int i = 0; i < listSegment.Count(); i++)
            {
                //   MessageBox.Show("" + listSegment[i].indexQueue);
                // listSegment[i].LeghtFromSecond;
                //     MessageBox.Show(""+(listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond));
                if (MovingSegment != i)
                {
                    e.Graphics.DrawLine(orangePen, new Point(x, lineStartPos.Y)

                    , new Point(x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond), lineStartPos.Y));
                    e.Graphics.DrawLine(orangePen, new Point(x + penWidth / 2, lineStartPos.Y + 5), new Point(x + penWidth / 2, lineStartPos.Y - 5));
                    e.Graphics.DrawLine(darkGreyPen, new Point(x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y + 5), new Point(x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y - 5));


                }
                else
                {
                    Pen colorRed = new Pen(Color.OrangeRed, 3);
                    e.Graphics.DrawLine(colorRed, new Point(x, lineStartPos.Y)
                                    , new Point(x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond), lineStartPos.Y));

                    e.Graphics.DrawLine(colorRed, new Point(x + penWidth / 2, lineStartPos.Y + 5), new Point(x + penWidth / 2, lineStartPos.Y - 5));
                    e.Graphics.DrawLine(colorRed, new Point(x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y + 5), new Point(x + (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond) - penWidth / 2, lineStartPos.Y - 5));

                }



                x += (int)(leghtLine * listSegment[i].LeghtFromSecond / maxLeghtOutFromSecond);

            }
        }
    }
}
