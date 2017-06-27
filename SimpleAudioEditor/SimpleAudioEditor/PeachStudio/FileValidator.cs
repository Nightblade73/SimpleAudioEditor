using System;
using System.IO;

namespace SimpleAudioEditor.PeachStudio {
    public static class FileValidator {
        public static bool IsValid(String filePath) {
            if (!File.Exists(filePath)) {
                //throw new FileNotFoundException();
                return false;
            }
            String ext = Path.GetExtension(filePath);
            if (ext != "wav" || ext != "mp3") {
                //throw new InvalidDataException("Неверное расширение сэмпла");
                return false;
            }
            return true;
        }
    }
}
