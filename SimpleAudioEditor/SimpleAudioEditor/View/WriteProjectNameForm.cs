using SimpleAudioEditor.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.View
{
    public partial class WriteProjectNameForm : Form
    {
        public String title;

        public WriteProjectNameForm()
        {
            InitializeComponent();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            if(tBName.Text.Equals(""))
            {
                t.Show("Имя проекта не может быть пустым или содержать символы: '.', '*', '/'", tBName);  //какие ещё символы?
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.title = tBName.Text;
            }
        }

        private void butCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void WriteProjectNameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
