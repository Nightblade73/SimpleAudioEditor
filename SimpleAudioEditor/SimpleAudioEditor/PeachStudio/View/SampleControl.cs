using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio {
    public partial class SampleControl : UserControl {
        Sample sample;
        SamplePlayer samplePlayer;

        public SampleControl() {
            InitializeComponent();
        }
    }
}
