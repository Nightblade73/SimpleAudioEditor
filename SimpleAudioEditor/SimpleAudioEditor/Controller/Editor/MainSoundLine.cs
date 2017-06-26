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
        int playerLeghtPerPixel;
        WaveOutEvent player;
        Point endPlayerPos;
        Point markerPoint;
        Timer timer;

        int currentPlayed = -1;
        Mp3FileReader mp3Reader;

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
            pictureBox.Size = new Size(width - height - 10 - 10, height);

            pictureBox.Parent = parent;
            

            pictureBox.Paint += pictureBox_Paint;
            pictureBox.DragEnter += pictureBox_DragEnter;
            pictureBox.DragDrop += pictureBox_DragDrop;
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseDoubleClick += pictureBox_MouseDoubleClick;



            //            pictureBox.Location = new Point(buttonStop.Location.X + buttonStop.Size.Width + 10, location.Y - 25);

            (pictureBox as Control).AllowDrop = true;

            lineStartPos = new Point(0, pictureBox.Size.Height / 2);
            lineEndPos = new Point(width, lineStartPos.Y);
//
            //lineStartPos = new Point(0, pictureBox.Size.Height - 10);
            //lineEndPos = new Point(width, pictureBox.Size.Height - 10);

            leghtLine = lineEndPos.X - lineStartPos.X;

            buttonPlay = new Button();
            buttonPlay.Size = new System.Drawing.Size(40, 40);
            buttonPlay.Location = location;
            buttonPlay.Parent = parent;
            buttonPlay.Click += buttonPlay_Click;
            buttonPlay.Text = ">";
            buttonPlay.BackColor = Color.OrangeRed;
            buttonPlay.FlatStyle = FlatStyle.Flat;

            buttonStop = new Button();
            buttonStop.Size = buttonPlay.Size;
            buttonStop.Location = new Point(buttonPlay.Location.X, location.Y + buttonPlay.Size.Width);
            buttonStop.Parent = parent;
            buttonStop.Text = "■";
            buttonStop.BackColor = Color.OrangeRed;
            buttonStop.FlatStyle = FlatStyle.Flat;
            buttonStop.Click+= buttonStop_Click;
            markerPoint = new Point(lineStartPos.X, lineStartPos.Y - (int)(object_radius * 2f));

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
            buttonDelete.Location = new Point(buttonOK.Location.X , buttonPlay.Location.Y + buttonPlay.Size.Width);
            buttonDelete.Click += buttonDelete_Click;
            buttonDelete.Parent = parent;
            buttonDelete.Text = "×";
            buttonDelete.BackColor = Color.Coral;
            buttonDelete.FlatStyle = FlatStyle.Flat;

            pictureBox.Location = new Point(location.X + buttonPlay.Size.Width + 10, location.Y);

            player = new WaveOutEvent();
            //mp3Reader = new Mp3FileReader(@"C:\Users\egork\Downloads\mozart-horn-concerto4-3-rondo.mp3");
            // player.Init(mp3Reader);
            // player.Play();

            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 16;

            pictureBox.Invalidate();

        }



        #region "Moving Marker"


        // Мы двигаем маркер точку.
        private void pictureBox_MouseMove_MovingMarker(object sender, MouseEventArgs e)
        {

            markerPoint =
            new Point(SoundLineEditor.Clamp(e.X + OffsetX, lineStartPos.X, playerLeghtPerPixel), markerPoint.Y);

            // Перерисовать.
            pictureBox.Invalidate();
        }

        void updatePlayingSegment()
        {
            if (project.listSamples.Count > 0)
            {
                if (player.PlaybackState == PlaybackState.Playing)
                {
                    currentPlayed = IndexSegmentUnderCursor(markerPoint);
                    player.Stop();
                    mp3Reader = new Mp3FileReader(project.listSamples[currentPlayed].SoundPath);

                    int leghtSegmentPerPixel = (project.listSamples[currentPlayed].endPos.X - project.listSamples[currentPlayed].startPos.X);
                    TimeSpan currentTime = TimeSpan.FromSeconds
                        (
                            project.listSamples[currentPlayed].SplitStartTimeFromSecond
                            + (((double)(markerPoint.X - project.listSamples[currentPlayed].startPos.X)) / leghtSegmentPerPixel)
                            * project.listSamples[currentPlayed].LeghtFromSecond
                       );

                    mp3Reader.CurrentTime = currentTime;
                    player.Init(mp3Reader);

                    player.Play();
                    timer.Start();
                }
            }
            else
            {
                buttonPlay.Text = ">";
                timer.Stop();
                player.Stop();
            }
        }

        // Остановить перемещение конечной точки.
        protected void pictureBox_MouseUp_MovingMarker(object sender, MouseEventArgs e)
        {
            // Сброс обработчиков событий.
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseMove -= pictureBox_MouseMove_MovingMarker;
            pictureBox.MouseUp -= pictureBox_MouseUp_MovingMarker;

            //            mp3Reader.CurrentTime = TimeSpan.FromSeconds(skipTimeFromSecond);
            //   timer.Start();
            updatePlayingSegment();
            // Перерисовать.
            pictureBox.Invalidate();
            //   MessageBox.Show(""+ mp3Reader.CurrentTime.TotalSeconds + " - " +soundTotalTime);
        }

        #endregion // Перемещение конечной точки

        public static double Clamp(double value, double min, double max)
        {
            value = Math.Min(max, value);
            value = Math.Max(min, value);

            return value;
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            if (player != null && player.PlaybackState != PlaybackState.Stopped)
            {
                if (project.listSamples.Count > 0)
                {

                    int leghtSegmentPerPixel = (project.listSamples[currentPlayed].endPos.X - project.listSamples[currentPlayed].startPos.X);
                    double leghtSound = project.listSamples[currentPlayed].SplitEndTimeFromSecond - project.listSamples[currentPlayed].SplitStartTimeFromSecond;


                    int lineLeght = leghtSegmentPerPixel;

                    markerPoint = new Point(((project.listSamples[currentPlayed].startPos.X)
                        + (int)(lineLeght * (((mp3Reader.CurrentTime.TotalSeconds - project.listSamples[currentPlayed].SplitStartTimeFromSecond) /
                        (leghtSound))))), markerPoint.Y);
                    if (markerPoint.X >= project.listSamples[project.listSamples.Count-1].endPos.X)
                    {
                        markerPoint = new Point(SoundLineEditor.Clamp(markerPoint.X, lineStartPos.X, project.listSamples[project.listSamples.Count - 1].endPos.X), markerPoint.Y);
                        buttonPlay.Text = ">";
                        player.Stop();
                        timer.Stop();
                    }else

                    if (IndexSegmentUnderCursor(markerPoint) != currentPlayed)
                    {
                        if (IndexSegmentUnderCursor(markerPoint) != -1)
                        {
                            currentPlayed = IndexSegmentUnderCursor(markerPoint);
                            player.Stop();
                            mp3Reader = new Mp3FileReader(project.listSamples[currentPlayed].SoundPath);

                            TimeSpan currentTime = TimeSpan.FromSeconds
                                (
                                    project.listSamples[currentPlayed].SplitStartTimeFromSecond
                                    + (((double)(markerPoint.X - project.listSamples[currentPlayed].startPos.X)) / leghtSegmentPerPixel)
                                    * project.listSamples[currentPlayed].LeghtFromSecond
                               );

                            //MessageBox.Show(currentTime.ToString());

                            mp3Reader.CurrentTime = currentTime;
                            player.Init(mp3Reader);

                            player.Play();
                            timer.Start();

                           
                        }
                        else
                        {
                            //   markerPoint = new Point(SoundLineEditor.Clamp(markerPoint.X, lineStartPos.X, listSegment[listSegment.Count - 1].segmentEndPos.X), markerPoint.Y);

                            buttonPlay.Text = ">";
                            player.Stop();
                            timer.Stop();
                        }

                    }

                    pictureBox.Invalidate();
                }
            }
        }


        protected void buttonPlay_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            if (project.listSamples.Count > 0)
            {

                if (button.Text == ">")
                {
                    if (markerPoint.X >= project.listSamples[project.listSamples.Count - 1].endPos.X)
                    {
                        markerPoint = new Point(SoundLineEditor.Clamp(lineStartPos.X, lineStartPos.X, project.listSamples[project.listSamples.Count - 1].endPos.X), markerPoint.Y);
                    }

                    currentPlayed = IndexSegmentUnderCursor(markerPoint);
                    player.Stop();
                    mp3Reader = new Mp3FileReader(project.listSamples[currentPlayed].SoundPath);

                    int leghtSegmentPerPixel = (project.listSamples[currentPlayed].endPos.X - project.listSamples[currentPlayed].startPos.X);
                    TimeSpan currentTime = TimeSpan.FromSeconds
                        (
                            project.listSamples[currentPlayed].SplitStartTimeFromSecond
                            + (((double)(markerPoint.X - project.listSamples[currentPlayed].startPos.X)) / leghtSegmentPerPixel)
                            * project.listSamples[currentPlayed].LeghtFromSecond
                       );

                    //MessageBox.Show(currentTime.ToString());

                    mp3Reader.CurrentTime = currentTime;
                    player.Init(mp3Reader);

                    player.Play();
                    timer.Start();
                    button.Text = "||";

                }
                else
                {
                    timer.Stop();
                    button.Text = ">";
                    player.Pause();
                }
            }
        }


        protected void buttonStop_Click(object sender, EventArgs e)
        {
            markerPoint = new Point(lineStartPos.X,markerPoint.Y);
            player.Stop();
            timer.Stop();
            buttonPlay.Text = ">";
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

        }

        protected void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private int IndexSegmentUnderCursor(Point pt1)
        {
            SetSegmentEndPoints();
            pt1 = new Point(SoundLineEditor.Clamp(pt1.X, lineStartPos.X, playerLeghtPerPixel), pt1.Y);
            int index = -1;
            if (project.listSamples.Count > 0)
            {
                for (int i = 0; i < project.listSamples.Count; i++)
                {
                    if (pt1.X >= project.listSamples[i].startPos.X && pt1.X <= project.listSamples[i].endPos.X)
                    {
                        index = i;
                    }
                }
                if (pt1.X <= project.listSamples[0].endPos.X)
                {
                    index = 0;
                }
                else if (pt1.X >= project.listSamples[project.listSamples.Count - 1].startPos.X)
                {
                    index = project.listSamples.Count - 1;
                }
            }

            return index;
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
                s.ResizeBitMap(new Size(s.endPos.X - s.startPos.X, pictureBox.Height));

            }
            else
            {
                MessageBox.Show(String.Format("Выходной файл не может привышать {0} минут! Лимит привышен на {1} секунд!", maxLeghtOutFromSecond / 60, FinalLeght() + s.LeghtFromSecond - maxLeghtOutFromSecond));
            }
            setPlayerLeghtPrePixel();
            markerPoint =
            new Point(SoundLineEditor.Clamp(markerPoint.X, lineStartPos.X, playerLeghtPerPixel), markerPoint.Y);
            updatePlayingSegment();
            pictureBox.Invalidate();
        }

        //public void DoLikeDragDrop()
        //{
        //    SetSegmentEndPoints();

        //    Sample s = new Sample(0.0,  )
        //    if (FinalLeght() + s.LeghtFromSecond <= maxLeghtOutFromSecond)
        //    {
        //        // MessageBox.Show(""+s.indexQueue);
        //        project.listSamples.Add(s);
        //        s.IndexQueue = index;
        //        SetSegmentEndPoints();
        //    }
        //    else
        //    {
        //        MessageBox.Show(String.Format("Выходной файл не может привышать {0} минут! Лимит привышен на {1} секунд!", maxLeghtOutFromSecond / 60, FinalLeght() + s.LeghtFromSecond - maxLeghtOutFromSecond));
        //    }
        //    pictureBox.Invalidate();
        //}

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
                x++;

            }


        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Посмотрим, чем мы закончили.
            int segment_number;

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

                setPlayerLeghtPrePixel();
                markerPoint =
            new Point(SoundLineEditor.Clamp(markerPoint.X, lineStartPos.X, playerLeghtPerPixel), markerPoint.Y);

                updatePlayingSegment();

                pictureBox.Invalidate();

            }

        }

        public void setPlayerLeghtPrePixel()
        {
            playerLeghtPerPixel = 0;
            if (project.listSamples.Count > 0)
            {
                SetSegmentEndPoints();
                playerLeghtPerPixel = project.listSamples[project.listSamples.Count - 1].endPos.X;
            }
        }


        // The mouse is up. Посмотрите, находимся ли мы над конечной точкой или сегментом.
        private void pictureBox_MouseMove_NotDown(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Arrow;

            // Посмотрим,над чем мы находимся.
            int segment_number;
            Point hit_point;
            if (MouseIsOverMarker(e.Location, out hit_point))
                new_cursor = Cursors.VSplit;
            else if (MouseIsOverSegment(e.Location, out segment_number))
                new_cursor = Cursors.Hand;

            // Установим новый курсор.
            if (pictureBox.Cursor != new_cursor)
                pictureBox.Cursor = new_cursor;
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

        protected int FindDistanceToPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        #region "Moving Segment"
        // «Размер» объекта для мыши над целями.
        private const int object_radius = 6;

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
            updatePlayingSegment();
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

            return dx * dx;
        }



        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int penWidth = 1;
            Pen greyPen = new Pen(Color.DarkOrange, 3);
            Pen darkGreyPen = new Pen(Color.Black, 1);

            Pen orangePen = new Pen(Color.DarkOrange, penWidth);
            penWidth = 0;
            SetSegmentEndPoints();
            e.Graphics.DrawLine(greyPen, lineStartPos, new Point(playerLeghtPerPixel, lineStartPos.Y));

            int index = -1;
            for (int i = 0; i < project.listSamples.Count(); i++)
            {

                if (MovingSegment == i)
                {
                    index = i;
                }
                else
                {

                    e.Graphics.DrawLine(darkGreyPen, new Point(project.listSamples[i].startPos.X, 0 + penWidth)
                    , new Point(project.listSamples[i].endPos.X, 0 + penWidth));

                    e.Graphics.DrawLine(darkGreyPen, new Point(project.listSamples[i].startPos.X, pictureBox.Height - 1)
                    , new Point(project.listSamples[i].endPos.X, pictureBox.Height - 1));

                    e.Graphics.DrawLine(darkGreyPen, new Point(project.listSamples[i].startPos.X, 0)
                        , new Point(project.listSamples[i].startPos.X, pictureBox.Height));

                    e.Graphics.DrawLine(darkGreyPen, new Point(project.listSamples[i].endPos.X, 0)
                        , new Point(project.listSamples[i].endPos.X, pictureBox.Height));

                    e.Graphics.DrawImage(project.listSamples[i].FrequencyBitMap, project.listSamples[i].startPos.X, lineStartPos.Y - project.listSamples[i].FrequencyBitMap.Height / 2);

                    }

            }
            //    e.Graphics.DrawLine(greyPen, lineStartPos, lineEndPos);

            e.Graphics.DrawLine(darkGreyPen, new Point(0, 0)
                       , new Point(pictureBox.Width, 0));
            e.Graphics.DrawLine(darkGreyPen, new Point(0, pictureBox.Height - 1)
                       , new Point(pictureBox.Width, pictureBox.Height - 1));

            e.Graphics.DrawLine(darkGreyPen, new Point(0, 0)
                       , new Point(0, pictureBox.Height));

            e.Graphics.DrawLine(darkGreyPen, new Point(pictureBox.Width - 1, 0)
                       , new Point(pictureBox.Width - 1, pictureBox.Height));


            if (index != -1)
            {
                e.Graphics.DrawImage(project.listSamples[index].FrequencyBitMap, project.listSamples[index].startPos.X, lineStartPos.Y - project.listSamples[index].FrequencyBitMap.Height / 2);

                e.Graphics.DrawLine(orangePen, new Point(project.listSamples[index].startPos.X, 0 + penWidth)
                , new Point(project.listSamples[index].endPos.X, 0 + penWidth));

                e.Graphics.DrawLine(orangePen, new Point(project.listSamples[index].startPos.X, pictureBox.Height - 1)
                , new Point(project.listSamples[index].endPos.X, pictureBox.Height - 1));

                e.Graphics.DrawLine(orangePen, new Point(project.listSamples[index].startPos.X, 0), new Point(project.listSamples[index].startPos.X, pictureBox.Height));

                e.Graphics.DrawLine(orangePen, new Point(project.listSamples[index].endPos.X, 0)
                    , new Point(project.listSamples[index].endPos.X, pictureBox.Height));
            
            }
            e.Graphics.DrawLine(greyPen, lineStartPos, new Point(playerLeghtPerPixel, lineStartPos.Y));

            int penSize = 3;
            Pen splitPen = new Pen(Color.OrangeRed, penSize);
            Pen cursorPen = new Pen(Color.Black, penSize);

            //Рисуем маркер
            cursorPen.Color = Color.Black;
            e.Graphics.DrawLine(cursorPen, lineStartPos, new Point(markerPoint.X, lineStartPos.Y));

            e.Graphics.DrawPolygon(cursorPen, new Point[] {
                new Point( SoundLineEditor.Clamp( markerPoint.X - object_radius,lineStartPos.X,lineEndPos.X)
                , markerPoint.Y - object_radius),
                new Point( SoundLineEditor.Clamp( markerPoint.X + object_radius,Math.Min(lineStartPos.X,lineEndPos.X),Math.Max(lineStartPos.X,lineEndPos.X))
                , markerPoint.Y - object_radius),
                new Point(markerPoint.X,lineStartPos.Y)});
            //  }

        }
    }
}
