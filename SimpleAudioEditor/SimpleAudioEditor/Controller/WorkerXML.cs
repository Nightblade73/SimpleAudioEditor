﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleAudioEditor.Controller
{
    class WorkerXML
    {
        public static string Serialize(Project project)
        {
            try
            {
         //       XmlSerializer serializer = new XmlSerializer(typeof(Project));
                DataContractSerializer serializer = new DataContractSerializer(typeof(Project));
                using (FileStream fs = new FileStream(project.path + "\\config.xml", FileMode.Create))
                {
                    using (XmlWriter writer = XmlWriter.Create(fs))
                    {
                        serializer.WriteObject(writer, project);
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
                DataContractSerializer serializer = new DataContractSerializer(typeof(Project));
                //        XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using (FileStream fs = new FileStream(path + "\\config.xml", FileMode.Open))
                {
                    using (XmlReader writer = XmlReader.Create(fs))
                    {
                        project = (Project)serializer.ReadObject(writer);
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
