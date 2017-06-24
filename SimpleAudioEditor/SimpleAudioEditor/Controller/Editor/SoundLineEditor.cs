using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Forms;
using System.Drawing;

namespace SimpleAudioEditor.Controller.Editor
{
    class SoundLineEditor
    {
        double soundTotalTime;

        string filePath;
        WaveOutEvent player;

        Mp3FileReader mp3Reader;
        PictureBox pictureBox;
        Point soundLineStartPoint;
        Point soundLineEndPoint;
        Point markerPoint;
        Timer timer;
        Button buttonPlay;
        Button buttonStop;
        Point splitP1, splitP2;

        // «Размер» объекта для мыши над целями.
        private const int object_radius = 6;

        // Мы над объектом, если квадрат расстояния
        // между мышью и объектом меньше этого.
        private const int over_dist_squared = object_radius * object_radius;

        public SoundLineEditor(string _filePath, Control parent, Point location, int playerWidth)
        {
            pictureBox = new PictureBox();
            pictureBox.Size = new System.Drawing.Size(playerWidth, 40);
            soundLineStartPoint = new Point(10, pictureBox.Size.Height / 2);
            soundLineEndPoint = new Point(pictureBox.Size.Width - 10, pictureBox.Size.Height / 2);
            splitP1 = soundLineEndPoint;
            splitP2 = soundLineStartPoint;
            markerPoint = new Point(soundLineStartPoint.X, soundLineStartPoint.Y - (int)(object_radius * 2f));

            buttonPlay = new Button();
            buttonPlay.Size = new System.Drawing.Size(pictureBox.Size.Height, pictureBox.Size.Height);
            buttonPlay.Location = location;
            buttonPlay.Parent = parent;
            buttonPlay.Click += buttonPlay_Click;
            buttonPlay.Text = ">";
            buttonPlay.BackColor = Color.OrangeRed;
            buttonPlay.FlatStyle = FlatStyle.Flat;


            buttonStop = new Button();
            buttonStop.Size = buttonPlay.Size;
            buttonStop.Location = new Point(buttonPlay.Location.X + buttonPlay.Size.Width, location.Y);
            buttonStop.Click += buttonStop_Click;
            buttonStop.Parent = parent;
            buttonStop.Text = "■";
            buttonStop.BackColor = Color.OrangeRed;
            buttonStop.FlatStyle = FlatStyle.Flat;

            pictureBox.Location = new Point(buttonStop.Location.X + buttonStop.Size.Width, location.Y);
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.Parent = parent;
            pictureBox.BackColor = Color.DarkGray;
            pictureBox.Invalidate();
            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 16;
            timer.Start();

            filePath = _filePath;
            player = new WaveOutEvent();
            var file = new AudioFileReader(filePath);
            //   trimmed = new OffsetSampleProvider(file);
            mp3Reader = new Mp3FileReader(filePath);
            soundTotalTime = file.TotalTime.TotalSeconds;
            //           player.Stop();
            player.PlaybackStopped += Player_PlaybackStopped;
            player.Init(mp3Reader);
        }

        protected void pictureBox_MouseMove_NotDown(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Arrow;

            // Посмотрим,над чем мы находимся.
            Point hit_point;

            if (MouseIsOverMarker(e.Location, out hit_point))
                new_cursor = Cursors.VSplit;
            else
            if (MouseIsOverSplitPoint(e.Location, out hit_point))
                new_cursor = Cursors.SizeWE;
            else
            if (MouseIsOverSegment(e.Location))
                new_cursor = Cursors.Hand;

            // Установим новый курсор.
            if (pictureBox.Cursor != new_cursor)
                pictureBox.Cursor = new_cursor;
        }




        protected void buttonStop_Click(object sender, EventArgs e)
        {
            mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());
            player.Stop();
            int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
            markerPoint = new Point(((soundLineStartPoint.X) + (int)(lineLeght * (mp3Reader.CurrentTime.TotalSeconds / soundTotalTime))), markerPoint.Y);
            pictureBox.Invalidate();
        }

        protected void buttonPlay_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            if (button.Text == ">")
            {
                if (markerPoint.X < Math.Max(splitP1.X, splitP2.X))
                {
                    int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
                    double skipTimeFromSecond = ((markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght) * soundTotalTime;
                    //  MessageBox.Show(String.Format( "{0} - {1} - {2}", (markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght,soundTotalTime, skipTimeFromSecond));
                    mp3Reader.CurrentTime = TimeSpan.FromSeconds(skipTimeFromSecond);

                }
                else
                {
                    mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());

                }
                button.Text = "||";

                timer.Start();

                player.Play();

            }
            else
            {
                timer.Stop();
                button.Text = ">";
                player.Pause();
            }
        }

        protected void Player_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            timer.Stop();
            buttonPlay.Text = ">";
        }

        public Segment GetSegment()
        {
            return new Segment(SplitStartTimeFromSecond(), SplitEndTimeFromSecond(), soundTotalTime, filePath);
        }

        protected void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Посмотрим, чем мы закончили.
            Point hit_point;

            if (MouseIsOverMarker(e.Location, out hit_point))
            {
                // Начните перемещать эту конечную точку.
                pictureBox.MouseMove -= pictureBox_MouseMove_NotDown;
                pictureBox.MouseMove += pictureBox_MouseMove_MovingMarker;
                pictureBox.MouseUp += pictureBox_MouseUp_MovingMarker;

                // Посмотрим, будем ли мы перемещать начальную конечную точку.
                timer.Stop();
                // Запомните смещение от мыши до точки.
                OffsetX = hit_point.X - e.X;
            }
            else
            if (MouseIsOverSplitPoint(e.Location, out hit_point))
            {
                // Начните перемещать эту конечную точку.
                pictureBox.MouseMove -= pictureBox_MouseMove_NotDown;
                pictureBox.MouseMove += pictureBox_MouseMove_MovingSplitPoint;
                pictureBox.MouseUp += pictureBox_MouseUp_MovingSplitPoint;


                // Посмотрим, будем ли мы перемещать начальную конечную точку.
                MovingStartSplitPoint = (splitP1.Equals(hit_point));

                // Запомните смещение от мыши до точки.
                OffsetP1X = hit_point.X - e.X;
                OffsetP2X = hit_point.X - e.X;
            }
            else
            if (MouseIsOverSegment(e.Location))
            {
                //   MessageBox.Show("DpDragDrop");
                pictureBox.DoDragDrop(GetSegment(), DragDropEffects.Copy);
            }

        }

        #region "Moving Marker"

        private int OffsetX;

        // Мы двигаем маркер точку.
        private void pictureBox_MouseMove_MovingMarker(object sender, MouseEventArgs e)
        {

            markerPoint =
            new Point(Clamp(e.X + OffsetX, Math.Min(splitP1.X, splitP2.X), Math.Max(splitP1.X, splitP2.X)), markerPoint.Y);


            // Перерисовать.
            pictureBox.Invalidate();
        }

        // Остановить перемещение конечной точки.
        protected void pictureBox_MouseUp_MovingMarker(object sender, MouseEventArgs e)
        {
            // Сброс обработчиков событий.
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseMove -= pictureBox_MouseMove_MovingMarker;
            pictureBox.MouseUp -= pictureBox_MouseUp_MovingMarker;

            int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
            double skipTimeFromSecond = ((markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght) * soundTotalTime;
            //  MessageBox.Show(String.Format( "{0} - {1} - {2}", (markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght,soundTotalTime, skipTimeFromSecond));
            mp3Reader.CurrentTime = TimeSpan.FromSeconds(skipTimeFromSecond);
            timer.Start();

            if (mp3Reader.CurrentTime.TotalSeconds == SplitEndTimeFromSecond())
            {
                player.Stop();
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());
                buttonPlay.Name = ">";
                timer.Stop();

            }

            // Перерисовать.
            pictureBox.Invalidate();
            //   MessageBox.Show(""+ mp3Reader.CurrentTime.TotalSeconds + " - " +soundTotalTime);
        }

        #endregion // Перемещение конечной точки


        protected double SplitStartTimeFromSecond()
        {
            int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
            double splitTimeFromSecond = ((Math.Min(splitP1.X, splitP2.X) - soundLineStartPoint.X) * 1.0 / lineLeght) * soundTotalTime;


            return splitTimeFromSecond;
        }

        protected double SplitEndTimeFromSecond()
        {
            int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
            double splitTimeFromSecond = ((Math.Max(splitP1.X, splitP2.X) - soundLineStartPoint.X) * 1.0 / lineLeght) * soundTotalTime;


            return splitTimeFromSecond;
        }

        protected void timer_Tick(object sender, EventArgs e)
        {

            if (player != null && player.PlaybackState != PlaybackState.Stopped)
            {

                int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;

                markerPoint = new Point(((soundLineStartPoint.X) + (int)(lineLeght * (mp3Reader.CurrentTime.TotalSeconds / soundTotalTime))), markerPoint.Y);
                pictureBox.Invalidate();
                //DisplayPosition();
            }
            if (mp3Reader.CurrentTime.TotalSeconds >= SplitEndTimeFromSecond())
            {
                player.Stop();
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());
                buttonPlay.Name = ">";
            }
        }

        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Pen greyPen = new Pen(Color.Gray, 3);
            Pen orangePen = new Pen(Color.OrangeRed, 3);
            Pen orangeDarkPen = new Pen(Color.DarkOrange, 3);

            e.Graphics.DrawLine(greyPen, soundLineStartPoint, soundLineEndPoint);


            // Нарисуем сегмент.
            e.Graphics.DrawLine(orangeDarkPen, splitP1, splitP2);


            
            //Рисуем маркер
            e.Graphics.DrawPolygon(orangePen, new Point[] {
                new Point(markerPoint.X - object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X + object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X, soundLineStartPoint.Y)});

            //Рисуем точки обрезки 
            int object_radius_point = object_radius / 3;
            Rectangle rect = new Rectangle(
                    splitP1.X - object_radius_point, splitP1.Y - object_radius_point,
                       2 * object_radius_point + 1, 2 * object_radius_point + 1);
            e.Graphics.FillEllipse(Brushes.Orange, rect);
            e.Graphics.DrawEllipse(Pens.Black, rect);


            rect = new Rectangle(
            splitP2.X - object_radius_point, splitP2.Y - object_radius_point,
            2 * object_radius_point + 1, 2 * object_radius_point + 1);
            e.Graphics.FillEllipse(Brushes.Orange, rect);
            e.Graphics.DrawEllipse(Pens.Black, rect);

            /*
            Rectangle rect = new Rectangle(
                        markerPoint.X - object_radius, markerPoint.Y - object_radius,
                        2 * object_radius + 1, 2 * object_radius + 1);
                        */
            //    e.Graphics.FillEllipse(Brushes.OrangeRed, rect);
            //e.Graphics.DrawEllipse(Pens.Gray, rect);

        }

        protected bool MouseIsOverMarker(Point mouse_pt, out Point hit_pt)
        {


            if (FindDistanceToPointSquared(mouse_pt, markerPoint) < over_dist_squared)
            {
                hit_pt = markerPoint;
                return true;
            }
            else
            {
                hit_pt = new Point(-1, -1);
                return false;
            }

        }

        // Посмотрим, находится ли мышь над конечной точкой.
        private bool MouseIsOverSplitPoint(Point mouse_pt, out Point hit_pt)
        {
            // Проверьте начальную точку.
            if (FindDistanceToPointSquared(mouse_pt, splitP1) < Math.Sqrt(over_dist_squared))
            {
                hit_pt = splitP1;
                return true;
            }

            // Проверьте конечную точку.
            if (FindDistanceToPointSquared(mouse_pt, splitP2) < Math.Sqrt(over_dist_squared))
            {
                hit_pt = splitP2;
                return true;
            }

            hit_pt = new Point(-1, -1);
            return false;
        }

        protected int FindDistanceToPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        public int Clamp(int value, int min, int max)
        {
            value = Math.Min(max, value);
            value = Math.Max(min, value);

            return value;
        }

        #region "Moving End Point"

        // Конечная точка, с которой мы движемся.
        private bool MovingStartSplitPoint = false;

        // Смещение от мыши до перемещаемого объекта.
        private int OffsetP1X, OffsetP2X;

        // Мы двигаем конечную точку.
        private void pictureBox_MouseMove_MovingSplitPoint(object sender, MouseEventArgs e)
        {
            // Переместите точку в новое место
            if (MovingStartSplitPoint)
            {

                splitP1 =
                     new Point(Clamp(e.X + OffsetP1X, soundLineStartPoint.X, soundLineEndPoint.X), splitP1.Y);
            }
            else
            {
                splitP2 =
                new Point(Clamp(e.X + OffsetX, soundLineStartPoint.X, soundLineEndPoint.X), splitP2.Y);

            }
            if (player.PlaybackState == PlaybackState.Stopped)
            {
                markerPoint =
             new Point(Clamp(markerPoint.X, Math.Min(splitP1.X, splitP2.X), Math.Max(splitP1.X, splitP2.X)), markerPoint.Y);

            }

            // Перерисовать.
            pictureBox.Invalidate();
        }

        // Остановить перемещение конечной точки.
        private void pictureBox_MouseUp_MovingSplitPoint(object sender, MouseEventArgs e)
        {
            // Сброс обработчиков событий.
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseMove -= pictureBox_MouseMove_MovingSplitPoint;
            pictureBox.MouseUp -= pictureBox_MouseUp_MovingSplitPoint;

            int markerX = markerPoint.X;
            if (markerX < Math.Min(splitP1.X, splitP2.X) || markerX > Math.Max(splitP1.X, splitP2.X))
            {
                markerPoint =
              new Point(Clamp(markerPoint.X, Math.Min(splitP1.X, splitP2.X), Math.Max(splitP1.X, splitP2.X)), markerPoint.Y);

                int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
                double skipTimeFromSecond = ((markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght) * soundTotalTime;
                //  MessageBox.Show(String.Format( "{0} - {1} - {2}", (markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght,soundTotalTime, skipTimeFromSecond));
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(skipTimeFromSecond);
                timer.Start();
            }
            if (mp3Reader.CurrentTime.TotalSeconds == SplitEndTimeFromSecond())
            {
                player.Stop();
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());
                buttonPlay.Name = ">";
                timer.Stop();

            }
            // Перерисовать.
            pictureBox.Invalidate();
        }

        #endregion // Перемещение конечной точки

        public void Delete()
        {
            MessageBox.Show("~~");
            player.Dispose();
            mp3Reader.Dispose();
            pictureBox.Dispose();
            buttonPlay.Dispose();
            buttonStop.Dispose();
            timer.Dispose();

        }

        private bool MouseIsOverSegment(Point mouse_pt)
        {
            // Посмотрим, перешли ли мы над сегментом.
            PointF closest;
            if (FindDistanceToSegmentSquared(
                mouse_pt, splitP1, splitP2, out closest)
                    < over_dist_squared)
            {
                return true;
            }
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

    }
}
