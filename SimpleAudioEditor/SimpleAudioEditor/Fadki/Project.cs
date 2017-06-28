using SimpleAudioEditor.Controller.WaveController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAudioEditor.Controller
{
    [Serializable]
    public class Project
    {
        [XmlIgnore]
        public string title;
        [XmlIgnore]
        public string path;
        [XmlIgnore]
        public List<Sample> listSamples = new List<Sample>();

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        
        public List<Sample> ListSamples
        {
            get { return listSamples; }
            set { listSamples = value; }
        }

        public Project(String title, Primary prim)
        {
            this.title = title;
            this.path = prim.progPath + "\\" + title;
        }

        public Project() { }   
    }
}
