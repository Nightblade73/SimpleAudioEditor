using SimpleAudioEditor.Model;
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
        public WriteProjectNameForm()
        {
            InitializeComponent();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Params.SetProjectName(tBName.Text);
            Dispose();
        }

        private void butCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Dispose();
        }
    }
}
