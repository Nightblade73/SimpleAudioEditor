using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleAudioEditor.PeachStudio.WorkMethods
{
    class WorkerXML
    {
        public static string Serialize(Project project)
        {
            try
            {
           //     XmlSerializer serializer = new XmlSerializer(typeof(Project));
               DataContractSerializer serializer = new DataContractSerializer(typeof(Project));
                using (FileStream fs = new FileStream(project.GetProjectPath() + "\\PeachStudioConfig.xml", FileMode.Create))
                {
                    using (XmlWriter writer = XmlWriter.Create(fs))
                    {
                        serializer.WriteObject(writer, project);
            //            serializer.Serialize(writer, project);
                    }
                }
            }
            catch (Exception ex)
            {
                return "не сохранил настройки в PeachStudioConfig.xml./n" + ex.ToString();
            }
            return "Сохранено";
        }

        public static Project Deserialize(string path)
        {
            Project project = null;
            try
            {
               DataContractSerializer serializer = new DataContractSerializer(typeof(Project));
         //               XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using (FileStream fs = new FileStream(path + "\\PeachStudioConfig.xml", FileMode.Open))
                {
                    using (XmlReader writer = XmlReader.Create(fs))
                    {
                        project = (Project)serializer.ReadObject(writer);
         //               project = (Project)serializer.Deserialize(writer);
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
