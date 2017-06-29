using Microsoft.Win32;
using SimpleAudioEditor.PeachStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    public class Primary
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
                projects = new List<Project>();
                String[] folders = Directory.GetDirectories(progPath);
                foreach (String folder in folders)
                {
                    projects.Add(Project.CreateTempProject(folder));
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
        public bool SetProgrammPath(String path)
        {
            RegistryKey key = Registry_GetKey();
            String fpath = path + "\\PeachEditor";
            if (!Directory.Exists(path) || !IsDirectoryWritable(path))
            {
                return false;
            } else
            {
                if(!Directory.Exists(fpath))
                {
                    Directory.CreateDirectory(fpath);
                }
            }
            key.SetValue("path", fpath);
            key.Close();
            this.progPath = fpath;
            LoadProjects();
            return true;
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
        /* Source: https://stackoverflow.com/questions/1410127/c-sharp-test-if-user-has-write-access-to-a-folder 
           Author: priit && JonD
        */
        private bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(
                    Path.Combine(
                        dirPath,
                        Path.GetRandomFileName()
                    ),
                    1,
                    FileOptions.DeleteOnClose)
                )
                { }
                return true;
            } catch {
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }
    }
}
