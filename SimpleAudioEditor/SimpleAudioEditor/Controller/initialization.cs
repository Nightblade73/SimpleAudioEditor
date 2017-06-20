using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    class Initialization
    {
        public IWaveSource initializationWaveSource(string fname)
        {
            return GetSoundSource(fname);
        }

        public void initializationSoundOut(IWaveSource soundSource)
        {
            MainForm.soundOut = GetSoundOut();
            MainForm.soundOut.Initialize(soundSource);
        }

        private ISoundOut GetSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();
            else
                return new DirectSoundOut();
        }

        private IWaveSource GetSoundSource(string fname)
        {
            //return any source ... in this example, we'll just play a mp3 file
            return CodecFactory.Instance.GetCodec(@fname);
        }

    }
}
