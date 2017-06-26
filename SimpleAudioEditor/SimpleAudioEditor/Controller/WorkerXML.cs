using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAudioEditor.Controller
{
    class WorkerXML
    {
        public static void Serialize(Project project)
        {
            //XmlSerializationWriter wr = new XmlSerializationWriter(typeof Project);
        }

        public static Project Deserialize()
        {
            Project project = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Project));
            StreamReader reader = new StreamReader(new Primary().GetProgrammPath());
            reader.ReadToEnd();
            project = (Project)serializer.Deserialize(reader);
            reader.Close();
            return project;
        }
    }
}
