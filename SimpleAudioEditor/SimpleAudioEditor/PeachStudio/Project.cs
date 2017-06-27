using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.PeachStudio {
    public class Project {
        string projectPath;
        string title;
        List<Sample> samples;
        
        public Project()
        {
            samples = new List<Sample>();
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
