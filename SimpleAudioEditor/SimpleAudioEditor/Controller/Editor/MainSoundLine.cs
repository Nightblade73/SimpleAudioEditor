using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Forms;
using System.Drawing;
using SimpleAudioEditor.Model;

namespace SimpleAudioEditor.Controller.Editor
{
    class MainSoundLine
    {
        public List<Segment> listSegment;

        PictureBox pictureBox;
        Point lineStartPos, lineEndPos;
        int leghtLine;
        int playerLeghtPerPixel;
        WaveOutEvent player;
        Point endPlayerPos;
        Point markerPoint;
        Timer timer;
        Button buttonPlay;
        Button buttonStop;
        int currentPlayed = -1;
        Mp3FileReader mp3Reader;

        Random r = new Random();
        public MainSoundLine(int width, Control parent, Point localtion)
        {

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(width, 52);
            pictureBox.Parent = parent;
            pictureBox.Location = new Point(localtion.X, localtion.Y + 40);

            pictureBox.Paint += pictureBox_Paint;
            pictureBox.DragEnter += pictureBox_DragEnter;
            pictureBox.DragDrop += pictureBox_DragDrop;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseDoubleClick += pictureBox_MouseDoubleClick;

            (pictureBox as Control).AllowDrop = true;
            lineStartPos = new Point(0, pictureBox.Size.Height / 2);
            lineEndPos = new Point(width, lineStartPos.Y);
            listSegment = new List<Segment>();
            leghtLine = lineEndPos.X - lineStartPos.X;

            buttonPlay = new Button();
            buttonPlay.Size = new System.Drawing.Size(40, 40);
            buttonPlay.Location = localtion;
            buttonPlay.Parent = parent;
            buttonPlay.Click += buttonPlay_Click;
            buttonPlay.Text = ">";
            buttonPlay.BackColor = Color.OrangeRed;
            buttonPlay.FlatStyle = FlatStyle.Flat;

            buttonStop = new Button();
            buttonStop.Size = buttonPlay.Size;
            buttonStop.Location = new Point(buttonPlay.Location.X + buttonPlay.Size.Width, localtion.Y);
            //    buttonStop.Click += buttonStop_Click;
            buttonStop.Parent = parent;
            buttonStop.Text = "■";
            buttonStop.BackColor = Color.OrangeRed;
            buttonStop.FlatStyle = FlatStyle.Flat;
            buttonStop.Click+= buttonStop_Click;
            markerPoint = new Point(lineStartPos.X, lineStartPos.Y - (int)(object_radius * 2f));


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
            if (listSegment.Count > 0)
            {
                if (player.PlaybackState == PlaybackState.Playing)
                {
                    currentPlayed = IndexSegmentUnderCursor(markerPoint);
                    player.Stop();
                    mp3Reader = new Mp3FileReader(listSegment[currentPlayed].getFilePath);

                    int leghtSegmentPerPixel = (listSegment[currentPlayed].segmentEndPos.X - listSegment[currentPlayed].segmentStartPos.X);
                    TimeSpan currentTime = TimeSpan.FromSeconds
                        (
                            listSegment[currentPlayed].SplitStartTimeFromSecond
                            + (((double)(markerPoint.X - listSegment[currentPlayed].segmentStartPos.X)) / leghtSegmentPerPixel)
                            * listSegment[currentPlayed].LeghtFromSecond
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



        private Segment CurrentSegment()
        {

            return listSegment[listSegment.Count - 1];
        }

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
                if (listSegment.Count > 0)
                {

                    int leghtSegmentPerPixel = (listSegment[currentPlayed].segmentEndPos.X - listSegment[currentPlayed].segmentStartPos.X);
                    double leghtSound = listSegment[currentPlayed].SplitEndTimeFromSecond - listSegment[currentPlayed].SplitStartTimeFromSecond;


                    int lineLeght = leghtSegmentPerPixel;

                    markerPoint = new Point(((listSegment[currentPlayed].segmentStartPos.X)
                        + (int)(lineLeght * (((mp3Reader.CurrentTime.TotalSeconds - listSegment[currentPlayed].SplitStartTimeFromSecond) /
                        (leghtSound))))), markerPoint.Y);
                    if (markerPoint.X >= listSegment[listSegment.Count-1].segmentEndPos.X)
                    {
                        markerPoint = new Point(SoundLineEditor.Clamp(markerPoint.X, lineStartPos.X, listSegment[listSegment.Count - 1].segmentEndPos.X), markerPoint.Y);
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
                            mp3Reader = new Mp3FileReader(listSegment[currentPlayed].getFilePath);

                            TimeSpan currentTime = TimeSpan.FromSeconds
                                (
                                    listSegment[currentPlayed].SplitStartTimeFromSecond
                                    + (((double)(markerPoint.X - listSegment[currentPlayed].segmentStartPos.X)) / leghtSegmentPerPixel)
                                    * listSegment[currentPlayed].LeghtFromSecond
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
            if (listSegment.Count > 0)
            {

                if (button.Text == ">")
                {
                    if (markerPoint.X >= listSegment[listSegment.Count - 1].segmentEndPos.X)
                    {
                        markerPoint = new Point(SoundLineEditor.Clamp(lineStartPos.X, lineStartPos.X, listSegment[listSegment.Count - 1].segmentEndPos.X), markerPoint.Y);
                    }

                    currentPlayed = IndexSegmentUnderCursor(markerPoint);
                    player.Stop();
                    mp3Reader = new Mp3FileReader(listSegment[currentPlayed].getFilePath);

                    int leghtSegmentPerPixel = (listSegment[currentPlayed].segmentEndPos.X - listSegment[currentPlayed].segmentStartPos.X);
                    TimeSpan currentTime = TimeSpan.FromSeconds
                        (
                            listSegment[currentPlayed].SplitStartTimeFromSecond
                            + (((double)(markerPoint.X - listSegment[currentPlayed].segmentStartPos.X)) / leghtSegmentPerPixel)
                            * listSegment[currentPlayed].LeghtFromSecond
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
            if (listSegment.Count > 0)
            {
                for (int i = 0; i < listSegment.Count; i++)
                {
                    if (pt1.X >= listSegment[i].segmentStartPos.X && pt1.X <= listSegment[i].segmentEndPos.X)
                    {
                        index = i;
                    }
                }
                if (pt1.X <= listSegment[0].segmentEndPos.X)
                {
                    index = 0;
                }
                else if (pt1.X >= listSegment[listSegment.Count - 1].segmentStartPos.X)
                {
                    index = listSegment.Count - 1;
                }
            }

            return index;
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

            if (FinalLeght() + s.LeghtFromSecond <= Params.maxLeghtOutFromSecond)
            {
                listSegment.Add(s);
                s.indexQueue = index;
                SetSegmentEndPoints();
                s.ResizeBitMap(new Size(s.segmentEndPos.X - s.segmentStartPos.X, 40));
            }
            else
            {
                MessageBox.Show(String.Format("Выходной файл не может привышать {0} минут! Лимит привышен на {1} секунд!", Params.maxLeghtOutFromSecond / 60, FinalLeght() + s.LeghtFromSecond - Params.maxLeghtOutFromSecond));
            }
            setPlayerLeghtPrePixel();
            markerPoint =
            new Point(SoundLineEditor.Clamp(markerPoint.X, lineStartPos.X, playerLeghtPerPixel), markerPoint.Y);
            updatePlayingSegment();
            pictureBox.Invalidate();
        }

        private void CreateSampleFile(string fileName, double startPosSample, double endPosSample, double allTime)
        {/*
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
            Params.IndexCutFilePlus();*/
        }

        private void SetSegmentEndPoints()
        {
            listSegment.Sort();

            int x = 0;
            for (int i = 0; i < listSegment.Count(); i++)
            {
                int x1 = x;
                listSegment[i].segmentStartPos = new Point(x, lineStartPos.Y);
                x = x + (int)(leghtLine * listSegment[i].LeghtFromSecond / Params.maxLeghtOutFromSecond);
                listSegment[i].segmentEndPos = new Point(x, lineStartPos.Y);
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
                listSegment.RemoveAt(MovingSegment);
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
            if (listSegment.Count > 0)
            {
                SetSegmentEndPoints();
                playerLeghtPerPixel = listSegment[listSegment.Count - 1].segmentEndPos.X;
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
            updatePlayingSegment();
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
            for (int i = 0; i < listSegment.Count(); i++)
            {

                if (MovingSegment == i)
                {
                    index = i;
                }
                else
                {

                    e.Graphics.DrawLine(darkGreyPen, new Point(listSegment[i].segmentStartPos.X, 0 + penWidth)
                    , new Point(listSegment[i].segmentEndPos.X, 0 + penWidth));

                    e.Graphics.DrawLine(darkGreyPen, new Point(listSegment[i].segmentStartPos.X, pictureBox.Height - 1)
                    , new Point(listSegment[i].segmentEndPos.X, pictureBox.Height - 1));

                    e.Graphics.DrawLine(darkGreyPen, new Point(listSegment[i].segmentStartPos.X, 0)
                        , new Point(listSegment[i].segmentStartPos.X, pictureBox.Height));

                    e.Graphics.DrawLine(darkGreyPen, new Point(listSegment[i].segmentEndPos.X, 0)
                        , new Point(listSegment[i].segmentEndPos.X, pictureBox.Height));

                    e.Graphics.DrawImage(listSegment[i].BitMap, listSegment[i].segmentStartPos.X, lineStartPos.Y - listSegment[i].BitMap.Height / 2);

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
                e.Graphics.DrawImage(listSegment[index].BitMap, listSegment[index].segmentStartPos.X, lineStartPos.Y - listSegment[index].BitMap.Height / 2);

                e.Graphics.DrawLine(orangePen, new Point(listSegment[index].segmentStartPos.X, 0 + penWidth)
                , new Point(listSegment[index].segmentEndPos.X, 0 + penWidth));

                e.Graphics.DrawLine(orangePen, new Point(listSegment[index].segmentStartPos.X, pictureBox.Height - 1)
                , new Point(listSegment[index].segmentEndPos.X, pictureBox.Height - 1));

                e.Graphics.DrawLine(orangePen, new Point(listSegment[index].segmentStartPos.X, 0), new Point(listSegment[index].segmentStartPos.X, pictureBox.Height));

                e.Graphics.DrawLine(orangePen, new Point(listSegment[index].segmentEndPos.X, 0)
                    , new Point(listSegment[index].segmentEndPos.X, pictureBox.Height));


            }
            e.Graphics.DrawLine(greyPen, lineStartPos, new Point(playerLeghtPerPixel, lineStartPos.Y));

            int penSize = 3;
            Pen splitPen = new Pen(Params.myColor, penSize);
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
