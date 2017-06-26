using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.PeachStudio {
    class Sample {
        string soundPath;
        TimeSpan splitStartTime;
        TimeSpan splitEndTime;
        TimeSpan currentTime;
        TimeSpan totalTime;
        float[] optimizedArray;
    }
}
