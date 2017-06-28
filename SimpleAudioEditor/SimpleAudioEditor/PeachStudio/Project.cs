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
            /*String[] files = Directory.GetFiles(path);
            foreach(string file in files)
            {
                pr.samples.Add(Sample.CreateSample(file));
            }*/
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
        public void ClearSamples()
        {
            this.samples.Clear();
        }
        public void AddSample(Sample addedSample)
        {
            samples.Add(addedSample);
        }
        public void RemoveSample(Sample removedSample)
        {
            samples.Remove(removedSample);
        }

        public void Save()
        {
            List<string> list = new List<string>();
            int order = 0;
            foreach (Sample sample in samples)
            {
                order++;
                String newSamplePath = projectPath + "\\" + Path.GetFileName(sample.SoundPath);
                newSamplePath = newSamplePath.Replace(".wav", "_" + order + ".wav");
                newSamplePath = newSamplePath.Replace(".mp3", "_" + order + ".mp3");
                try
                {
                    SampleController sc = new SampleController();
                    if (sample.SoundPath.ToString().Contains(".wav"))
                    {
                        sc.TrimWavFile(SampleController.Converter(sample.SoundPath), newSamplePath, sample.SplitStartTime, sample.TotalTime - sample.SplitEndTime);
                    } else {
                        Console.WriteLine("TrimWavFile " + sample.SoundPath);
                        sc.TrimWavFile(sample.SoundPath, newSamplePath, sample.SplitStartTime, sample.TotalTime - sample.SplitEndTime);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось создать файл-отрезок./n" + ex.ToString());
                }
                list.Add(SampleController.Resemple(newSamplePath, newSamplePath));

            }
            
            SampleController.Concatenate(list, projectPath + "\\" + "result.mp3");
            Console.WriteLine(title +"saved");
        }
    }
}
