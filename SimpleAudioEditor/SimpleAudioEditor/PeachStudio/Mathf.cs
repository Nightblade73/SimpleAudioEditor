using CSCore;
using CSCore.Codecs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.PeachStudio
{
    public class Mathf
    {
        public static int Clamp(int value, int min, int max)
        {
            value = Math.Min(value, max);
            value = Math.Max(value, min);
            return value;
        }

        public static double Clamp(double value, double min, double max)
        {
            value = Math.Min(value, max);
            value = Math.Max(value, min);
            return value;
        }


        public static TimeSpan Clamp(TimeSpan value, TimeSpan min, TimeSpan max)
        {
            value = TimeSpan.FromSeconds(Math.Min(value.TotalSeconds, max.TotalSeconds));
            value = TimeSpan.FromSeconds(Math.Max(value.TotalSeconds, min.TotalSeconds));
            return value;
        }

        public static int TimeToPos(TimeSpan currentTime, TimeSpan totalTime, int lineWidth)
        {
            if (totalTime != new TimeSpan()) {
                return Convert.ToInt16(lineWidth * (currentTime.TotalSeconds / totalTime.TotalSeconds));
            }
            return 0;
        }

        public static TimeSpan PosToTime(int currentPos, int lineWidth, TimeSpan totalTime)
        {
            return Clamp(TimeSpan.FromSeconds(totalTime.TotalSeconds * ((double)currentPos / (double)lineWidth)), new TimeSpan(), totalTime);
        }


        public static int mThresholdSample = 64;

        public static ISampleSource CreateDrawSource(string filePath)
        {
            ISampleSource mDrawSource = CodecFactory.Instance.GetCodec(filePath).ToSampleSource().ToMono();
            return mDrawSource;
        }

        public static float[] CreateOptimizedArray(string filePath, ISampleSource drawSource)
        {
            string mRawFileName;
            
            float[] mOptimizedArray;

            drawSource = CodecFactory.Instance.GetCodec(filePath).ToSampleSource().ToMono();
            long offset = 0;
            long numSamples = drawSource.Length;
            int x = 0;
            int y = 0;
            //Nth item holds maxVal, N+1th item holds minVal so allocate an array of double size
            mOptimizedArray = new float[((numSamples / mThresholdSample) + 1) * 2];
            float[] data = new float[mThresholdSample];
            int samplesRead = 1;
            drawSource.Position = 0;
            string rawFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundFactory\";
            if (!Directory.Exists(rawFilePath)) Directory.CreateDirectory(rawFilePath);
            mRawFileName = rawFilePath + Guid.NewGuid().ToString() + ".raw";
            FileStream rawFile = new FileStream(mRawFileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bin = new BinaryWriter(rawFile);
            while (offset < numSamples && samplesRead > 0)
            {
                samplesRead = drawSource.Read(data, 0, mThresholdSample);
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
                }
            }
            return mOptimizedArray;
        }

        public static Bitmap DrawWave(float[] mOptimizedArray, ISampleSource mDrawSource, Pen penSplit, int w, int h, int splitStart, int splitEnd, Pen penBack)
        {
            Color defaultColor = penSplit.Color;
            long numSamples = mDrawSource.Length;

            int mSamplesPerPixel = (int)((double)mDrawSource.Length / (double)w);

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
                Pen pen = new Pen(Color.Transparent);
        
                if (prevX >= Math.Min(splitStart, splitEnd) && prevX <= Math.Max(splitStart, splitEnd) )
                {
                    pen = penSplit;
                }
                else
                {
                    pen = penBack;

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


        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }




    }
}
