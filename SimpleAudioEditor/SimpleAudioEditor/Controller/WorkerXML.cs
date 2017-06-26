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
        public static string Serialize(Project project)
        {
            try {
                XmlSerializer xs = new XmlSerializer(typeof(Project));
                using (StreamWriter writer = new StreamWriter(project.path +
                "\\config.xml"))
                {
                    xs.Serialize(writer.BaseStream, project);
                }
            }
            catch(Exception ex)
            {
                return "не сериализовал настройки./n" + ex.ToString();
            }
            return "Все ок";
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
