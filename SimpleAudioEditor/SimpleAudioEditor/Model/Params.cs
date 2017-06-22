using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Model {
    static class Params {

        public static readonly string ResultSoundsPath = "Results";
        public static readonly string ResultCuttedSoundsPath = "Results\\cut";
        public static readonly string ResultFileName = "result.mp3";
        public static readonly string FileFormatMP3 = ".mp3";
        public static readonly string FileFormatWAV = ".wav";
        public static readonly int CoefNewSamplesToMainSample = 3;
        public static readonly int NewSamplesWidth = 600;

        public static string GetResultCuttedIndexedSoundsPathMP3(int index) {
            return ResultCuttedSoundsPath + index + FileFormatMP3;
        }

        public static string GetResultCuttedIndexedSoundsPathWAV(int index) {
            return ResultCuttedSoundsPath + index + FileFormatWAV;
        }

    }
}
