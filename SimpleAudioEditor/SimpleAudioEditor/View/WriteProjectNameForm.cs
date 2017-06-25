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
        public Project pr;

        public WriteProjectNameForm()
        {
            InitializeComponent();
        }
        public WriteProjectNameForm(Project pr)
        {
            this.pr = pr;
            InitializeComponent();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (pr == null)
            {
                ///что то было
            } else
            {
                pr.title = tBName.Text;
            }
            Dispose();

        }

        private void butCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Dispose();
        }
    }
}
