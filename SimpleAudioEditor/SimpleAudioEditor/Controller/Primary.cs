using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    class Primary
    {
        public String progPath;
        public List<Project> projects;

        public Primary()
        {
            progPath = GetProgrammPath();
            projects = new List<Project>();
            LoadProjects();
        }
        private void LoadProjects()
        {
            Console.WriteLine("[DEBUG] LoadProjects call:  progPath: "+progPath);
            if (progPath != "nopath")
            {
                String[] folders = Directory.GetDirectories(progPath);
                foreach (String folder in folders)
                {
                    projects.Add(new Project(folder));
                }
            }
        }

        /* Получает путь с проектами из реестра
         */
        public String GetProgrammPath()
        {
            RegistryKey key = Registry_GetKey();
            String str = key.GetValue("path").ToString();
            key.Close();
            return str;
        }
        /* Устанавливает путь с проектами в переменную реестра
         */
        public void SetProgrammPath(String path)
        {
            RegistryKey key = Registry_GetKey();
            key.SetValue("path", path);
            key.Close();
            this.progPath = path;
            LoadProjects();
        }
        private RegistryKey Registry_GetKey()
        {
            Microsoft.Win32.RegistryKey key;
            String[] subkeys = Microsoft.Win32.Registry.CurrentUser.GetSubKeyNames();
            try
            {
                key = Registry.CurrentUser.OpenSubKey("Software\\SimpleAudioEditor", true);
                Console.WriteLine(key.Name);
            }
            catch (NullReferenceException ex)
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\SimpleAudioEditor");
                key.SetValue("path", "nopath", RegistryValueKind.String);
            }
            return key;
        }
    }
}
