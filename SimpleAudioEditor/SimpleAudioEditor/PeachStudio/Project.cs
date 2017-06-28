using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio {
    public class Project {
        string projectPath;
        public string title;
        List<Sample> samples;

        public static Project CreateTempProject(String path)
        {
            Project pr = new Project();
            pr.projectPath = path;
            pr.title = Path.GetFileName(path);
            pr.samples = new List<Sample>();
            String[] files = Directory.GetFiles(path);
            foreach(string file in files)
            {
                pr.samples.Add(Sample.CreateSample(file));
            }
            return pr;
        }
        
        public Project()
        {
            samples = new List<Sample>();
        }
        public String GetPath()
        {
            return projectPath;
        }
        public List<Sample> GetSampleList() {
            return samples;
        }
        public void AddSample(Sample addedSample)
        {
            samples.Add(addedSample);
        }
        public void RemoveSample(Sample removedSample)
        {
            samples.Remove(removedSample);
        }

        public string Save()
        {
            List<string> list = new List<string>();
            foreach (Sample sample in samples)
            {
                try
                {
                    SampleController sc = new SampleController();
                    if (sample.SoundPath.ToString().Contains(".wav"))
                    {
                        sc.TrimWavFile(SampleController.Converter(sample.SoundPath), sample.SoundPath, sample.SplitStartTime, sample.TotalTime - sample.SplitEndTime);
                    } else {
                        sc.TrimWavFile(sample.SoundPath, sample.SoundPath, sample.SplitStartTime, sample.TotalTime - sample.SplitEndTime);
                    }
                }
                catch (Exception ex)
                {
                    return "Не удалось создать файл-отрезок./n" + ex.ToString();
                }
                list.Add(SampleController.Resemple(sample.SoundPath, this.projectPath + "\\" + "result.mp3"));

            }
            SampleController.Concatenate(list, projectPath + "\\" + "result.mp3");
            return "Сохранено";
        }
    }
}
