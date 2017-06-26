using SimpleAudioEditor.Controller;
using SimpleAudioEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.View
{
    class ProjectButton : Button
    {
        public string path;
        
        public ProjectButton(string prName) : base()
        {
            
            this.Text = prName.Split('\\')[prName.Split('\\').Length-1];
            this.path = prName;
            this.BackgroundImage = Resources.icons8_Folder_104;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Height = 100;
            this.Width = 100;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.ForeColor = Color.White;
            this.Font = new Font(this.Font, FontStyle.Bold);
            Padding margin = this.Margin;
            margin.Top = 5;
            margin.Left = 40;
            this.Margin = margin;
        } 
    }
}
