using System;
using NAudio.Wave;
using System.Windows.Forms;
using System.Drawing;
using CSCore;
using System.IO;
using CSCore.Codecs;
using System.ComponentModel;
using TagLib;
using TagLib.Mpeg;


namespace SimpleAudioEditor.Controller.Editor
{
    public class SoundLineEditor
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
        int mProgressStatus;
        bool draw = false;
        bool splitPointStop = false;

        Project project;
        Label name;
        //bool splitPointStop = false;
        MaskedTextBox cursorMaskedTime;
        MaskedTextBox endMaskedTime;
        MaskedTextBox startMaskedTime;
        MaskedTextBox finalMaskedTime;
        MaskedTextBox minusMaskedTime;
        MaskedTextBox equalMaskedTime;
        BackgroundWorker backgroundWorker;

        // «Размер» объекта для мыши над целями.
        private const int object_radius = 6;
        // Мы над объектом, если квадрат расстояния
        // между мышью и объектом меньше этого.
        private const int over_dist_squared = object_radius * object_radius;

        public SoundLineEditor(string _filePath, Control parent, Point location, int playerWidth, Project _project)
        {
            project = _project;
            pictureBox = new PictureBox();
            pictureBox.Size = new System.Drawing.Size(playerWidth, 80);

            soundLineStartPoint = new Point(10, pictureBox.Size.Height / 2);
            soundLineEndPoint = new Point(pictureBox.Size.Width - 10, pictureBox.Size.Height / 2);

            splitP1 = soundLineEndPoint;
            splitP2 = soundLineStartPoint;
            markerPoint = new Point(soundLineStartPoint.X, soundLineStartPoint.Y - (int)(object_radius * 2f));

            ///
            name = new Label();
            AudioFile fileWithTags = new AudioFile(_filePath, ReadStyle.Average);
            if (fileWithTags.Tag.Performers != null && fileWithTags.Tag.Title != null)
            {
                name.Text = fileWithTags.Tag.Performers[0] + " - " + fileWithTags.Tag.Title;
            } else {
                string[] s = _filePath.Split('\\');
                name.Text = s[s.Length - 1];
            }
            name.Location = location;
            name.AutoSize = true;
            name.Parent = parent;
            name.SendToBack();
            name.FlatStyle = FlatStyle.Standard;


            buttonPlay = new Button();
            buttonPlay.Size = new System.Drawing.Size(pictureBox.Size.Height / 2, pictureBox.Size.Height / 2);
            buttonPlay.Location = location;
            buttonPlay.Parent = parent;
            buttonPlay.Click += buttonPlay_Click;
            buttonPlay.Text = ">";
            buttonPlay.BackColor = Color.OrangeRed;
            buttonPlay.FlatStyle = FlatStyle.Flat;

            buttonStop = new Button();
            buttonStop.Size = buttonPlay.Size;
            buttonStop.Location = new Point(buttonPlay.Location.X, location.Y + buttonPlay.Size.Width);
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

            pictureBox.BackColor = Color.Transparent;
            pictureBox.BorderStyle = BorderStyle.None;

            pictureBox.Invalidate();
            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 16;
            timer.Start();

            filePath = _filePath;
            buttonPlay.Enabled = false;
            buttonStop.Enabled = false;
            pictureBox.Enabled = false;
            backgroundWorker = new BackgroundWorker();

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // Start the asynchronous operation.
            backgroundWorker.RunWorkerAsync();

            //CreateOptimizedArray();

            player = new WaveOutEvent();
            var file = new AudioFileReader(filePath);
            //   trimmed = new OffsetSampleProvider(file);
            mp3Reader = new Mp3FileReader(filePath);
            soundTotalTime = file.TotalTime.TotalSeconds;
            //           player.Stop();
            player.PlaybackStopped += Player_PlaybackStopped;
            player.Init(mp3Reader);

            cursorMaskedTime = new MaskedTextBox();
            cursorMaskedTime.BackColor = Color.OrangeRed;
            cursorMaskedTime.ForeColor = Color.Black;

            cursorMaskedTime.BorderStyle = BorderStyle.None;
            cursorMaskedTime.Location = new Point(pictureBox.Location.X + 10, pictureBox.Location.Y + pictureBox.Height);
            cursorMaskedTime.Size = new Size(70, 20);
            cursorMaskedTime.AutoSize = true;
            cursorMaskedTime.Parent = parent;

            endMaskedTime = new MaskedTextBox();
            endMaskedTime.BackColor = Color.Black;
            endMaskedTime.ForeColor = Color.DarkOrange;
            endMaskedTime.BorderStyle = BorderStyle.None;

            endMaskedTime.Location = new Point(cursorMaskedTime.Location.X + cursorMaskedTime.Width, pictureBox.Location.Y + pictureBox.Height);
            endMaskedTime.Size = new Size(70, 20);
            endMaskedTime.AutoSize = true;
            endMaskedTime.Parent = parent;

            minusMaskedTime = new MaskedTextBox();
            minusMaskedTime.BackColor = Color.Black;
            minusMaskedTime.ForeColor = Color.DarkOrange;
            minusMaskedTime.BorderStyle = BorderStyle.None;

            minusMaskedTime.Location = new Point(endMaskedTime.Location.X + endMaskedTime.Width, pictureBox.Location.Y + pictureBox.Height);
            minusMaskedTime.Size = new Size(12, 20);
            minusMaskedTime.AutoSize = true;
            minusMaskedTime.Parent = parent;
            minusMaskedTime.Text = "-";



            startMaskedTime = new MaskedTextBox();
            startMaskedTime.BackColor = Color.Black;
            startMaskedTime.ForeColor = Color.DarkOrange;
            startMaskedTime.BorderStyle = BorderStyle.None;

            startMaskedTime.Location = new Point(minusMaskedTime.Location.X + minusMaskedTime.Width, pictureBox.Location.Y + pictureBox.Height);
            startMaskedTime.Size = new Size(80, 20);
            startMaskedTime.AutoSize = true;
            startMaskedTime.Parent = parent;

            equalMaskedTime = new MaskedTextBox();
            equalMaskedTime.BackColor = Color.Black;
            equalMaskedTime.ForeColor = Color.DarkOrange;
            equalMaskedTime.BorderStyle = BorderStyle.None;

            equalMaskedTime.Location = new Point(startMaskedTime.Location.X + startMaskedTime.Width, pictureBox.Location.Y + pictureBox.Height);
            equalMaskedTime.Size = new Size(12, 20);
            equalMaskedTime.AutoSize = true;
            equalMaskedTime.Parent = parent;
            equalMaskedTime.Text = "=";




            finalMaskedTime = new MaskedTextBox();
            finalMaskedTime.BackColor = Color.Black;
            finalMaskedTime.ForeColor = Color.DarkOrange;
            finalMaskedTime.BorderStyle = BorderStyle.None;

            finalMaskedTime.Location = new Point(equalMaskedTime.Location.X + equalMaskedTime.Width, pictureBox.Location.Y + pictureBox.Height);
            finalMaskedTime.Size = new Size(70, 20);
            finalMaskedTime.AutoSize = true;
            finalMaskedTime.Parent = parent;



            endMaskedTime.Text = TimeSpan.FromSeconds(SplitEndTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");
            startMaskedTime.Text = TimeSpan.FromSeconds(SplitStartTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");

            finalMaskedTime.Text = TimeSpan.FromSeconds(SplitEndTimeFromSecond() - SplitStartTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");

            cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;


            mDrawSource = CodecFactory.Instance.GetCodec(filePath).ToSampleSource().ToMono();

            long offset = 0;
            long numSamples = mDrawSource.Length;
            int x = 0;
            int y = 0;
            //Nth item holds maxVal, N+1th item holds minVal so allocate an array of double size
            mOptimizedArray = new float[((numSamples / mThresholdSample) + 1) * 2];
            float[] data = new float[mThresholdSample];
            int samplesRead = 1;
            mDrawSource.Position = 0;
            string rawFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundFactory\";
            if (!Directory.Exists(rawFilePath)) Directory.CreateDirectory(rawFilePath);
            mRawFileName = rawFilePath + Guid.NewGuid().ToString() + ".raw";
            FileStream rawFile = new FileStream(mRawFileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bin = new BinaryWriter(rawFile);
            while (offset < numSamples && samplesRead > 0)
            {
                samplesRead = mDrawSource.Read(data, 0, mThresholdSample);
                if (samplesRead > 0) //for some files file length is wrong so samplesRead may become 0 even if we did not come to the end of the file
                {
                    for (int i = 0; i < samplesRead; i++)
                    {
                        bin.Write(data[i]);
                    }

                    float maxVal = -1;
                    float minVal = 1;
                    // finds the max & min peaks for this pixel 
                    for (x = 0; x < samplesRead; x++)
                    {
                        maxVal = Math.Max(maxVal, data[x]);
                        minVal = Math.Min(minVal, data[x]);
                    }
                    mOptimizedArray[y] = minVal;
                    mOptimizedArray[y + 1] = maxVal;
                    y += 2;
                    offset += samplesRead;

                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        // if((int)(((float)offset / numSamples) * 100)>5)
                        //MessageBox.Show(""+(int)(((float)offset / numSamples) * 100)+"%");

                        //System.Threading.Thread.Sleep(10);
                        worker.ReportProgress((int)(((float)offset / numSamples) * 100));
                    }
                }
            }
            rawFile.Close();
        }

        // This event handler updates the progress.
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mProgressStatus = e.ProgressPercentage;
            pictureBox.Invalidate();
            //    resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                //        resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                //      resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                draw = true;
                pictureBox.Invalidate();
                //    MessageBox.Show("Done");
                //    resultLabel.Text = "Done!";
            }
            buttonPlay.Enabled = true;
            buttonStop.Enabled = true;
            pictureBox.Enabled = true;
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
            cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");

            pictureBox.Invalidate();

        }

        protected void buttonPlay_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            if (button.Text == ">")
            {
                if (markerPoint.X < Math.Max(splitP1.X, splitP2.X))
                {
                    mp3Reader.CurrentTime = TimeSpan.FromSeconds(CursorTime());
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

        protected void Player_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            timer.Stop();
            buttonPlay.Text = ">";
        }

        public Sample GetSample()
        {
            Rectangle section = new Rectangle(new Point(Math.Min(splitP1.X, splitP2.X) - 9, 0), new Size(Math.Max(splitP1.X, splitP2.X) - Math.Min(splitP1.X, splitP2.X), pictureBox.Height));

            Bitmap CroppedImage = CropImage(DrawWave(new Pen(Color.OrangeRed), pictureBox.Size.Width - 20, pictureBox.Size.Height), section);
            //    CroppedImage = ResizeImage(CroppedImage, new Size(cr,));
            return new Sample(SplitStartTimeFromSecond(), SplitEndTimeFromSecond(), soundTotalTime, filePath, project, CroppedImage);
        }




        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
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
                pictureBox.DoDragDrop(GetSample(), DragDropEffects.Copy);
            }

        }

        #region "Moving Marker"

        private int OffsetX;

        // Мы двигаем маркер точку.
        private void pictureBox_MouseMove_MovingMarker(object sender, MouseEventArgs e)
        {

            markerPoint =
            new Point(Clamp(e.X + OffsetX, Math.Min(splitP1.X, splitP2.X), Math.Max(splitP1.X, splitP2.X)), markerPoint.Y);

            cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");

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

            if (mp3Reader.CurrentTime.TotalSeconds == SplitEndTimeFromSecond())
            {
                player.Stop();

                //  mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());

                buttonPlay.Name = ">";
                timer.Stop();

            }
            cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");
            // Перерисовать.
            cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");
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
                cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");

                pictureBox.Invalidate();
                //DisplayPosition();
            }
            if (mp3Reader.CurrentTime.TotalSeconds >= SplitEndTimeFromSecond())
            {
                player.Stop();
                timer.Stop();

                //   mp3Reader.CurrentTime = TimeSpan.FromSeconds(SplitStartTimeFromSecond());

                buttonPlay.Name = ">";
            }
        }

        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int penSize = 3;
            Pen greyPen = new Pen(SystemColors.WindowFrame, penSize);
            Pen splitPen = new Pen(Color.OrangeRed, penSize);
            Pen cursorPen = new Pen(Color.Black, penSize);


            Pen orangePen = new Pen(Color.DarkOrange, penSize);

            if (draw) {
                e.Graphics.DrawImage(DrawWave(new Pen(Color.OrangeRed), pictureBox.Size.Width - 20, pictureBox.Size.Height), 10, 0);
                Pen orangeDarkPen = new Pen(Color.DarkOrange, 3);

                //  e.Graphics.DrawLine(greyPen, soundLineStartPoint, soundLineEndPoint);
                e.Graphics.DrawLine(new Pen(Color.Orange, penSize), splitP1, splitP2);


                if (Math.Min(splitP1.X, splitP2.X) <= markerPoint.X)
                    e.Graphics.DrawLine(cursorPen, new Point(Math.Min(splitP1.X, splitP2.X), splitP1.Y), new Point(markerPoint.X, soundLineStartPoint.Y));


                e.Graphics.DrawLine(orangePen, new Point(splitP1.X, 0), new Point(splitP1.X, pictureBox.Height));

                e.Graphics.DrawLine(orangePen, new Point(splitP2.X, 0), new Point(splitP2.X, pictureBox.Height));

                e.Graphics.DrawLine(orangePen, new Point(splitP1.X, 0 + penSize / 2), new Point(splitP2.X, 0 + penSize / 2));
                e.Graphics.DrawLine(orangePen, new Point(splitP1.X, pictureBox.Height - penSize / 2), new Point(splitP2.X, pictureBox.Height - penSize / 2));


                if (markerPoint.X - object_radius <= Math.Min(splitP1.X, splitP2.X))
                {
                    cursorPen.Color = Color.OrangeRed;

                    //Рисуем маркер
                    e.Graphics.DrawPolygon(cursorPen, new Point[] {
                new Point( Clamp( markerPoint.X - object_radius,soundLineStartPoint.X,Math.Max(splitP1.X,splitP2.X))
                , markerPoint.Y - object_radius),
                new Point( Clamp( markerPoint.X + object_radius,soundLineStartPoint.X,Math.Max(splitP1.X,splitP2.X))
                , markerPoint.Y - object_radius),
                new Point(markerPoint.X, soundLineStartPoint.Y)});
                }

                if (markerPoint.X + object_radius + penSize > Math.Min(splitP1.X, splitP2.X) && markerPoint.X <= Math.Max(splitP1.X, splitP2.X))

                {
                    //Рисуем маркер
                    cursorPen.Color = Color.Black;

                    e.Graphics.DrawPolygon(cursorPen, new Point[] {
                new Point( Clamp( markerPoint.X - object_radius,Math.Min(splitP1.X,splitP2.X),Math.Max(splitP1.X,splitP2.X))
                , markerPoint.Y - object_radius),
                new Point( Clamp( markerPoint.X + object_radius,Math.Min(splitP1.X,splitP2.X),Math.Max(splitP1.X,splitP2.X))
                , markerPoint.Y - object_radius),
                new Point(markerPoint.X, soundLineStartPoint.Y)});
                }
                /*
                else
                {
                    //Рисуем маркер
                cursorPen.Color = Color.OrangeRed;
                e.Graphics.DrawPolygon(cursorPen, new Point[] {
                new Point( markerPoint.X - object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X + object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X, soundLineStartPoint.Y)});
                


                }
                */


                /*
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
            */
            }
            else
            {
                Rectangle rect = new Rectangle(soundLineStartPoint.X, 0,
                      (pictureBox.Width - 20) * mProgressStatus / 100, pictureBox.Height);

                e.Graphics.FillRectangle(new SolidBrush(Color.OrangeRed), rect);

            }
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
            if (FindDistanceToSplitPointSquared(mouse_pt, splitP1) < Math.Sqrt(over_dist_squared))
            {
                hit_pt = splitP1;
                return true;
            }

            // Проверьте конечную точку.
            if (FindDistanceToSplitPointSquared(mouse_pt, splitP2) < Math.Sqrt(over_dist_squared))

            {

                hit_pt = splitP2;
                return true;
            }

            hit_pt = new Point(-1, -1);
            return false;
        }


        protected int FindDistanceToSplitPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt1.Y;
            return dx * dx + dy * dy;
        }

        protected int FindDistanceToPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        public static int Clamp(int value, int min, int max)
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

        private double CursorTime()
        {
            int lineLeght = soundLineEndPoint.X - soundLineStartPoint.X;
            return ((double)(markerPoint.X - soundLineStartPoint.X) / (double)lineLeght) * soundTotalTime;

        }

        // Мы двигаем конечную точку.
        private void pictureBox_MouseMove_MovingSplitPoint(object sender, MouseEventArgs e)
        {
            // Переместите точку в новое место
            if (MovingStartSplitPoint)
            {

                splitP1 =
                     new Point(Clamp(e.X + OffsetP1X, soundLineStartPoint.X, soundLineEndPoint.X), splitP1.Y);
                endMaskedTime.Text = TimeSpan.FromSeconds(SplitEndTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");
                startMaskedTime.Text = TimeSpan.FromSeconds(SplitStartTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");
                finalMaskedTime.Text = TimeSpan.FromSeconds(SplitEndTimeFromSecond() - SplitStartTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");

            }
            else
            {
                splitP2 =
                new Point(Clamp(e.X + OffsetX, soundLineStartPoint.X, soundLineEndPoint.X), splitP2.Y);
                endMaskedTime.Text = TimeSpan.FromSeconds(SplitEndTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");
                startMaskedTime.Text = TimeSpan.FromSeconds(SplitStartTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");
                finalMaskedTime.Text = TimeSpan.FromSeconds(SplitEndTimeFromSecond() - SplitStartTimeFromSecond()).ToString(@"hh\:mm\:ss\.FF");

            }
            if (player.PlaybackState != PlaybackState.Playing)
            {
                markerPoint =
             new Point(Clamp(markerPoint.X, Math.Min(splitP1.X, splitP2.X), Math.Max(splitP1.X, splitP2.X)), markerPoint.Y);

            }
            cursorMaskedTime.Text = TimeSpan.FromSeconds(CursorTime()).ToString(@"hh\:mm\:ss\.FF");

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

                //  MessageBox.Show(String.Format( "{0} - {1} - {2}", (markerPoint.X - soundLineStartPoint.X) * 1.0 / lineLeght,soundTotalTime, skipTimeFromSecond));
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(CursorTime());
                timer.Start();
            }
            if (mp3Reader.CurrentTime.TotalSeconds == SplitEndTimeFromSecond())
            {
                player.Stop();
                splitPointStop = true;
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
        private ISampleSource mDrawSource;
        private float[] mOptimizedArray;
        int mThresholdSample = 64;
        //private Bitmap DrawWave(Pen pen, int w, int h)
        //{
        //    Color defaultColor = pen.Color;
        //    long numSamples = mDrawSource.Length;

        //    int mSamplesPerPixel = (int)(mDrawSource.Length / w);
        //    int mDrawingStartOffset = 0;
        //    int mPrevSamplesPerPixel = mSamplesPerPixel;
        //    Bitmap mBitmap = null;
        //    if (mBitmap == null || ((mBitmap.Width != w) | (mBitmap.Height != h)))
        //    {
        //        if (mBitmap != null)
        //            mBitmap.Dispose();
        //        mBitmap = new Bitmap(w, h);
        //    }
        //    Graphics canvas = Graphics.FromImage(mBitmap);

        //    int prevX = 0;
        //    int prevMaxY = 0;
        //    int prevMinY = 0;
        //    float maxVal = 0;
        //    float minVal = 0;
        //    int i = 0;
        //    // index is how far to offset into the data array 
        //    long index = 0;
        //    int maxSampleToShow = (int)Math.Min((mSamplesPerPixel * w) + mDrawingStartOffset, numSamples);
        //    int sampleCount = 0;
        //    int offsetIndex = 0;
        //    if (mSamplesPerPixel > mThresholdSample)
        //    {
        //        sampleCount = (int)(mSamplesPerPixel / mThresholdSample) * 2;
        //        offsetIndex = (int)Math.Floor((decimal)(mDrawingStartOffset / mThresholdSample)) * 2;
        //    }
        //    float[] data = new float[mSamplesPerPixel];
        //    mDrawSource.Position = mDrawingStartOffset;

        //    int x = 0;

        //    while (index < maxSampleToShow)
        //    {
        //        maxVal = -1;
        //        minVal = 1;
        //        int samplesRead = 0;
        //        if (mSamplesPerPixel > mThresholdSample)
        //        {
        //            int startIndex = offsetIndex + (i * sampleCount);
        //            int endIndex = Math.Min(mOptimizedArray.Length - 1, startIndex + sampleCount - 1);
        //            for (x = startIndex; x <= endIndex; x++)
        //            {
        //                maxVal = Math.Max(maxVal, mOptimizedArray[x]);
        //                minVal = Math.Min(minVal, mOptimizedArray[x]);
        //            }
        //        }
        //        else
        //        {
        //            samplesRead = mDrawSource.Read(data, 0, data.Length);
        //            // finds the max & min peaks for this pixel 
        //            for (x = 0; x < samplesRead; x++)
        //            {
        //                maxVal = Math.Max(maxVal, data[x]);
        //                minVal = Math.Min(minVal, data[x]);
        //            }
        //        }
        //        //8-bit samples are stored as unsigned bytes, ranging from 0 to 255. 
        //        //16-bit samples are stored as 2's-complement signed integers, ranging from -32768 to 32767. 
        //        // scales based on height of window 
        //        int scaledMinVal = (int)(((minVal + 1) * h) / 2);
        //        int scaledMaxVal = (int)(((maxVal + 1) * h) / 2);
        //        // if the max/min are the same, then draw a line from the previous position, 
        //        // otherwise we will not see anything 
        //        if (prevX >= Math.Min(splitP1.X, splitP2.X) - 10 && prevX <= Math.Max(splitP1.X, splitP2.X) - 10)
        //        {
        //            pen.Color = defaultColor;
        //        }
        //        else
        //        {
        //            pen.Color = Color.Black;
        //        }
        //        if (scaledMinVal == scaledMaxVal)
        //        {
        //            if (prevMaxY != 0)
        //            {
        //                canvas.DrawLine(pen, prevX, prevMaxY, i, scaledMaxVal);
        //            }
        //        }
        //        else
        //        {
        //            if (i > prevX)
        //            {
        //                if (prevMaxY < scaledMinVal)
        //                {

        //                    canvas.DrawLine(pen, prevX, prevMaxY, i, scaledMinVal);
        //                }
        //                else
        //                {
        //                    if (prevMinY > scaledMaxVal)
        //                    {
        //                        canvas.DrawLine(pen, prevX, prevMinY, i, scaledMaxVal);
        //                    }
        //                }
        //            }

        //            canvas.DrawLine(pen, i, scaledMinVal, i, scaledMaxVal);
        //        }

        //        prevX = i;
        //        prevMaxY = scaledMaxVal;
        //        prevMinY = scaledMinVal;
        //        i += 1;
        //        index = (i * mSamplesPerPixel) + mDrawingStartOffset;
        //    }

        //    return mBitmap;

        //}

        private string mRawFileName;

        private void CreateOptimizedArray()
        {
            mDrawSource = CodecFactory.Instance.GetCodec(filePath).ToSampleSource().ToMono();
            long offset = 0;
            long numSamples = mDrawSource.Length;
            int x = 0;
            int y = 0;
            //Nth item holds maxVal, N+1th item holds minVal so allocate an array of double size
            mOptimizedArray = new float[((numSamples / mThresholdSample) + 1) * 2];
            float[] data = new float[mThresholdSample];
            int samplesRead = 1;
            mDrawSource.Position = 0;
            string rawFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundFactory\";
            if (!Directory.Exists(rawFilePath)) Directory.CreateDirectory(rawFilePath);
            mRawFileName = rawFilePath + Guid.NewGuid().ToString() + ".raw";
            FileStream rawFile = new FileStream(mRawFileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bin = new BinaryWriter(rawFile);
            while (offset < numSamples && samplesRead > 0)
            {
                samplesRead = mDrawSource.Read(data, 0, mThresholdSample);
                if (samplesRead > 0) //for some files file length is wrong so samplesRead may become 0 even if we did not come to the end of the file
                {
                    for (int i = 0; i < samplesRead; i++)
                    {
                        bin.Write(data[i]);
                    }
                    float maxVal = -1;
                    float minVal = 1;
                    // finds the max & min peaks for this pixel 
                    for (x = 0; x < samplesRead; x++)
                    {
                        maxVal = Math.Max(maxVal, data[x]);
                        minVal = Math.Min(minVal, data[x]);
                    }
                    mOptimizedArray[y] = minVal;
                    mOptimizedArray[y + 1] = maxVal;
                    y += 2;
                    offset += samplesRead;
                    //mProgressStatus = (int)(((float)offset / numSamples) * 100);
                }
            }
        }

        private Bitmap DrawWave(Pen pen, int w, int h)
        {
            Color defaultColor = pen.Color;
            long numSamples = mDrawSource.Length;

            int mSamplesPerPixel = (int)(mDrawSource.Length / w);

            int mDrawingStartOffset = 0;

            int mPrevSamplesPerPixel = mSamplesPerPixel;
            Bitmap mBitmap = null;
            if (mBitmap == null || ((mBitmap.Width != w) | (mBitmap.Height != h)))
            {
                if (mBitmap != null)
                    mBitmap.Dispose();
                mBitmap = new Bitmap(w, h);
            }
            Graphics canvas = Graphics.FromImage(mBitmap);

            int prevX = 0;
            int prevMaxY = 0;
            int prevMinY = 0;
            float maxVal = 0;
            float minVal = 0;

            int i = 0;

            // index is how far to offset into the data array 
            long index = 0;
            int maxSampleToShow = (int)Math.Min((mSamplesPerPixel * w) + mDrawingStartOffset, numSamples);

            int sampleCount = 0;
            int offsetIndex = 0;
            if (mSamplesPerPixel > mThresholdSample)
            {
                sampleCount = (int)(mSamplesPerPixel / mThresholdSample) * 2;
                offsetIndex = (int)Math.Floor((decimal)(mDrawingStartOffset / mThresholdSample)) * 2;
            }
            float[] data = new float[mSamplesPerPixel];
            mDrawSource.Position = mDrawingStartOffset;


            int x = 0;

            while (index < maxSampleToShow)
            {
                maxVal = -1;
                minVal = 1;
                int samplesRead = 0;
                if (mSamplesPerPixel > mThresholdSample)
                {
                    int startIndex = offsetIndex + (i * sampleCount);
                    int endIndex = Math.Min(mOptimizedArray.Length - 1, startIndex + sampleCount - 1);
                    for (x = startIndex; x <= endIndex; x++)
                    {
                        maxVal = Math.Max(maxVal, mOptimizedArray[x]);
                        minVal = Math.Min(minVal, mOptimizedArray[x]);
                    }
                }
                else
                {
                    samplesRead = mDrawSource.Read(data, 0, data.Length);
                    // finds the max & min peaks for this pixel 
                    for (x = 0; x < samplesRead; x++)
                    {
                        maxVal = Math.Max(maxVal, data[x]);
                        minVal = Math.Min(minVal, data[x]);
                    }
                }
                //8-bit samples are stored as unsigned bytes, ranging from 0 to 255. 
                //16-bit samples are stored as 2's-complement signed integers, ranging from -32768 to 32767. 
                // scales based on height of window 
                int scaledMinVal = (int)(((minVal + 1) * h) / 2);
                int scaledMaxVal = (int)(((maxVal + 1) * h) / 2);

                // if the max/min are the same, then draw a line from the previous position, 
                // otherwise we will not see anything 


                if (prevX >= Math.Min(splitP1.X, splitP2.X) - 10 && prevX <= Math.Max(splitP1.X, splitP2.X) - 10)
                {
                    pen.Color = defaultColor;
                }
                else
                {
                    pen.Color = Color.Black;

                }
                if (scaledMinVal == scaledMaxVal)
                {
                    if (prevMaxY != 0)
                    {
                        canvas.DrawLine(pen, prevX, prevMaxY, i, scaledMaxVal);
                    }
                }
                else
                {
                    if (i > prevX)
                    {
                        if (prevMaxY < scaledMinVal)
                        {

                            canvas.DrawLine(pen, prevX, prevMaxY, i, scaledMinVal);
                        }
                        else
                        {
                            if (prevMinY > scaledMaxVal)
                            {
                                canvas.DrawLine(pen, prevX, prevMinY, i, scaledMaxVal);
                            }
                        }
                    }

                    canvas.DrawLine(pen, i, scaledMinVal, i, scaledMaxVal);
                }

                prevX = i;
                prevMaxY = scaledMaxVal;
                prevMinY = scaledMinVal;

                i += 1;
                index = (i * mSamplesPerPixel) + mDrawingStartOffset;
            }

            return mBitmap;

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
    }
}
