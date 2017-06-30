using SimpleAudioEditor.Controller;
using SimpleAudioEditor.PeachStudio;
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
        public Project pr;
        public ProjectButton(Project pr) : base()
        {
            this.pr = pr;
            this.Text = pr.title;
            this.BackgroundImage = Resources.icons8_Folder_104;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Height = 100;
            this.Width = 100;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.MouseOverBackColor = Color.FromArgb(17, 255, 255, 255);
            this.FlatAppearance.BorderSize = 0;
            this.ForeColor = Color.White;
            this.Font = new Font(this.Font, FontStyle.Bold);
            Padding margin = this.Margin;
            margin.Top = 5;
            margin.Right = 40;
            this.Margin = margin;
        } 
    }
}
