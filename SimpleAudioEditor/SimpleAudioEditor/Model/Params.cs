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
        public static readonly int CoefNewSamplesToMainSample = 2;
        public static readonly int NewSamplesWidth = 600;
        private static int indexCutFile = 0;
        public static readonly string ExceptionError = "Вы не можете получить доступ к файлу, " +
            "попробуйте через 5 секунд";

        public static string GetResultCuttedIndexedSoundsPathMP3(int index) {
            return ResultCuttedSoundsPath + index + FileFormatMP3;
        }

        public static string GetResultCuttedIndexedSoundsPathWAV() {
            return ResultCuttedSoundsPath + (indexCutFile) + FileFormatWAV;
        }

        public static void IndexCutFilePlus()
        {
            indexCutFile++;
        }

    }
}
