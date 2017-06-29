using SimpleAudioEditor.PeachStudio.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                sc.TrimWavFile(SampleController.Converter(s.SoundPath), s.SamplePath, s.SplitStartTime, s.TotalTime - s.SplitEndTime);
            }
            else
            {
                sc.TrimWavFile(s.SoundPath, s.SamplePath, s.SplitStartTime, s.TotalTime- s.SplitEndTime);
            }
        }

        public static string Save(Project project)
        {
            //DeleteOldDirectory(project);

            List<string> list = new List<string>();
            int count = 0;
            foreach (var sample in project.GetSampleList())
            {
                sample.SamplePath = sample.CreateSamplePath(project.GetProjectPath(), count);
                try
                {
                    
                    CreateSampleFile(sample);

                }
                catch (Exception ex)
                {
                    return "Не удалось создать файл-отрезок./n" + ex.ToString();
                }
                list.Add(SampleController.Resemple(sample.SamplePath, project.GetProjectPath() + "\\" + "result.mp3"));
                //SampleController.Combine(sample.SamplePath, path + "\\" + "result.wav");
                count++;
            }
            SampleController.Concatenate(list, project.GetProjectPath() + "\\" + "result.mp3");

            return "Сохранено";
        }

        public static void DeleteOldDirectory(Project project)
        {
            try {
                Directory.Delete(project.GetProjectPath(), true);
            }
            catch (Exception ex)
            {
                MyMessageBox mmb = new MyMessageBox(ex.Message, false);
                mmb.ShowDialog();
            }
            while (Directory.Exists(project.GetProjectPath()))
            {
                try
                {
                    Directory.CreateDirectory(project.GetProjectPath());
                }
                catch (Exception ex)
                {
                    MyMessageBox mmb = new MyMessageBox(ex.Message, false);
                    mmb.ShowDialog();
                    return;
                }
            }
        }
    }
}
