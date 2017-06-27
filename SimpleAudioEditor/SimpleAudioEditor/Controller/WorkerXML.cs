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
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using (FileStream fs = new FileStream(project.path + "\\config.xml", FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        serializer.Serialize(writer, project);
                    }
                }
            }
            catch (Exception ex)
            {
                return "не сериализовал настройки./n" + ex.ToString();
            }
            return "Все ок";
        }

        public static Project Deserialize(string path)
        {
            Project project = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using (FileStream fs = new FileStream(path + "\\config.xml", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        project = (Project)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //  if (project == null) project = new Project();
            return project;
        }
    }
}
