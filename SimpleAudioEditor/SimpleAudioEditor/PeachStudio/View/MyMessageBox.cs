using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio.View
{
    public partial class MyMessageBox : Form
    {
        public MyMessageBox(string message, bool isQuestion)
        {
            InitializeComponent();
            labelMes.Text = message;
            if (!isQuestion)
            {
                butCancle.Visible = false;
                butOK.Location = new Point(90, 83);
            }
        }
        public Label GetLabel()
        {
            return labelMes;
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void butCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
