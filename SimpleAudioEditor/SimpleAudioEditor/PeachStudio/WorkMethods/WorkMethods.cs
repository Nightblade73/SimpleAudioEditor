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


        private static void CreateSampleFile(Sample s, string cutPath)
        {
            SampleController sc = new SampleController();
            if (s.SoundPath.ToString().Contains(".wav"))
            {
                sc.TrimWavFile(SampleController.Converter(s.SoundPath), cutPath, s.SplitStartTime, s.TotalTime - s.SplitEndTime);
            }
            else
            {
                sc.TrimWavFile(s.SoundPath, cutPath, s.SplitStartTime, s.TotalTime- s.SplitEndTime);
            }
        }

        public static string Save(Project project)
        {
            List<string> list = new List<string>();
            int count = 0;
            foreach (var sample in project.GetSampleList())
            {
                string cutPath = sample.SamplePath(project.GetProjectPath(), count);
                try
                {
                    
                    CreateSampleFile(sample, cutPath);

                }
                catch (Exception ex)
                {
                    return "Не удалось создать файл-отрезок./n" + ex.ToString();
                }
                list.Add(SampleController.Resemple(cutPath, project.GetProjectPath() + "\\" + "result.mp3"));
                //SampleController.Combine(sample.SamplePath, path + "\\" + "result.wav");
                count++;
            }
            SampleController.Concatenate(list, project.GetProjectPath() + "\\" + "result.mp3");

            return "Сохранено";
        }
    }
}
