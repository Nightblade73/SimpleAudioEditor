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

        public void AddSample(Sample addedSample) { }
        public void RemoveSample(Sample removedSample) { }

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
    }
}
