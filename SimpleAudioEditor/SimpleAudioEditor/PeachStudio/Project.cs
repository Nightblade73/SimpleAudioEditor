using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
