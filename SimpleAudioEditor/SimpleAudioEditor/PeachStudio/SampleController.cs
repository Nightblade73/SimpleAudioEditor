using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.PeachStudio
{
    class SampleController
    {
        public static void Combine(string inPath, string outPath)
        {
            using (FileStream fs = new FileStream(outPath, FileMode.Append))
            {
                inPath = Converter(inPath);
                File.Delete(inPath.Split('.')[0] + ".wav");
                using (Mp3FileReader reader = new Mp3FileReader(inPath))
                {
                    if ((fs.Position == 0) && (reader.Id3v2Tag != null))
                    {
                        fs.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                    }
                    Mp3Frame frame;
                    while ((frame = reader.ReadNextFrame()) != null)
                    {
                        fs.Write(frame.RawData, 0, frame.RawData.Length);
                    }
                }
            }
        }

        public void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            WaveFormat MyWaveFormat = new WaveFormat(44100, 2);
            using (Mp3FileReader reader = new Mp3FileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

                    int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                    int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    int endPos = (int)reader.Length - endBytes;

                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
        }

        private static void TrimWavFile(Mp3FileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;

            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public static string Converter(string inPath)
        {
            using (WaveFileReader mpbacground = new WaveFileReader(inPath))
            {
                using (WaveStream background = WaveFormatConversionStream.CreatePcmStream(mpbacground))
                {
                    using (var mixer = new WaveMixerStream32())
                    {
                        mixer.AutoStop = true;
                        var messageOffset = background.TotalTime;
                        var background32 = new WaveChannel32(background);
                        background32.PadWithZeroes = false;
                        mixer.AddInputStream(background32);
                        using (var wave32 = new Wave32To16Stream(mixer))
                        {
                            var mp3Stream = ConvertWavToMp3(wave32);
                            inPath = inPath.Split('.')[0] + ".mp3";
                            File.WriteAllBytes(inPath, mp3Stream.ToArray());
                        }
                    }
                }
            }
            return inPath;
        }

        public static MemoryStream ConvertWavToMp3(Wave32To16Stream wavFile)
        {
            using (var retMs = new MemoryStream())
            using (var wtr = new LameMP3FileWriter(retMs, wavFile.WaveFormat, 128))
            {
                wavFile.CopyTo(wtr);
                return retMs;
            }
        }

         public static void Concatenate(List<string> sourceFiles, string outPath)
         {
             byte[] buffer = new byte[1024];
             WaveFileWriter waveFileWriter = null;
             try
             {
                 foreach (string sourceFile in sourceFiles)
                 {
                     using (WaveFileReader reader = new WaveFileReader(sourceFile))
                     {
                         if (waveFileWriter == null)
                         {
                             waveFileWriter = new WaveFileWriter(outPath, reader.WaveFormat);
                         }
                         else
                         {
                             if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                             {
                                 throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                             }
                         }
                         int read;
                         while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                         {
                             waveFileWriter.Write(buffer, 0, read);
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                Console.WriteLine(ex.StackTrace);
             }
             finally
             {
                 if (waveFileWriter != null)
                 {
                     waveFileWriter.Dispose();
                 }
             }
         }
         public static string Resemple(string inPath, string outPath)
         {
             int outRate = 44100;
             var outFile = outPath;
             using (var reader = new WaveFileReader(inPath))
             {
                 if (reader.WaveFormat.Equals(new WaveFormat(outRate, 2)))
                 {
                     return inPath;
                 }
                 else
                 {
                     var outFormat = new WaveFormat(outRate, reader.WaveFormat.Channels);
                     using (var resampler = new MediaFoundationResampler(reader, outFormat))
                     {
                         // resampler.ResamplerQuality = 60;
                         WaveFileWriter.CreateWaveFile(outFile, resampler);
                     }
                 }
             }
             File.Delete(inPath);
             File.Move(outFile, inPath);
             return inPath;
         }
    }
}
