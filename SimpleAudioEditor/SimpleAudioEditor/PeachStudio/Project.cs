using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace SimpleAudioEditor.PeachStudio {
    [Serializable]
    public class Project {

        public string projectPath;
        public string title;        
        public List<Sample> samples;

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
        
        public string GetProjectPath()
        {
            return projectPath;
        }
    }
}
