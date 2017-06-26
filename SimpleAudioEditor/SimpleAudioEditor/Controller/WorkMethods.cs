using SimpleAudioEditor.Controller.WaveController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    class WorkMethods
    {
        public static string CleanRAWFiles()
        {
            try
            {
                Directory.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundFactory", true);
                return "ok";
            }
            catch (Exception ex)
            {
                return "не удалось удалить папку SoundFactory. \n" + ex.Message;
            }
        }


        private static void CreateSampleFile(Sample s)
        {
            TimeSpan start = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(s.SplitStartTimeFromSecond * 1000));
            TimeSpan end = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(s.SplitEndTimeFromSecond * 1000));
            TimeSpan all = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(s.AllTimeFromSecond * 1000));
            SampleController sc = new SampleController();
            if (s.SoundPath.ToString().Contains(".wav"))
            {
                sc.TrimWavFile(SampleController.Converter(s.SoundPath), s.SamplePath, start, all - end);
            }
            else
            {
                sc.TrimWavFile(s.SoundPath, s.SamplePath, start, all - end);
            }
        }

        public static string Save(Project project)
        {
            List<string> list = new List<string>();
            foreach (var sample in project.listSamples)
            {
                try
                {
                    CreateSampleFile(sample);

                }
                catch (Exception ex)
                {
                    return "Не удалось создать файл-отрезок./n" + ex.ToString();
                }
                list.Add(SampleController.Resemple(sample.SamplePath, project.path + "\\" + "result.mp3"));
                //SampleController.Combine(sample.SamplePath, path + "\\" + "result.wav");

            }
            SampleController.Concatenate(list, project.path + "\\" + "result.mp3");

            return "Сохранено";
        }
    }
}
