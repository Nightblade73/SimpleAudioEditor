using CSCore;
using CSCore.Codecs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.PeachStudio
{
    class Mathf
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
            return Convert.ToInt16(lineWidth * (currentTime.TotalSeconds / totalTime.TotalSeconds));
        }

        public static TimeSpan PosToTime(int currentPos, int lineWidth, TimeSpan totalTime)
        {
            return Clamp(TimeSpan.FromSeconds(totalTime.TotalSeconds * ((double)currentPos / (double)lineWidth)), new TimeSpan(), totalTime);
        }



        public static float[] CreateOptimizedArray(string filePath)
        {
            string mRawFileName;
            ISampleSource mDrawSource;
            float[] mOptimizedArray;
            int mThresholdSample = 64;
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
                }
            }
            return mOptimizedArray;
        }

    }
}
