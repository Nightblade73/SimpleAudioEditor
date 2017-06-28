using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.PeachStudio.WorkMethods
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
            SampleController sc = new SampleController();
            if (s.SoundPath.ToString().Contains(".wav"))
            {
                sc.TrimWavFile(SampleController.Converter(s.SoundPath), s.SoundPath, s.SplitStartTime, s.TotalTime - s.SplitEndTime);
            }
            else
            {
                sc.TrimWavFile(s.SoundPath, s.SoundPath, s.SplitStartTime, s.TotalTime- s.SplitEndTime);
            }
        }

        public static string Save(Project project)
        {
            List<string> list = new List<string>();
            foreach (var sample in project.GetSampleList())
            {
                try
                {
                    CreateSampleFile(sample);

                }
                catch (Exception ex)
                {
                    return "Не удалось создать файл-отрезок./n" + ex.ToString();
                }
                list.Add(SampleController.Resemple(sample.SoundPath, project.GetProjectPath() + "\\" + "result.mp3"));
                //SampleController.Combine(sample.SamplePath, path + "\\" + "result.wav");

            }
            SampleController.Concatenate(list, project.GetProjectPath() + "\\" + "result.mp3");

            return "Сохранено";
        }
    }
}
