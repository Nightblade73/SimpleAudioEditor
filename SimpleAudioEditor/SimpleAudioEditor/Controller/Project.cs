using SimpleAudioEditor.Controller.WaveController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{

    public class Project
    {
        public string title;
        public string path;
        public List<Sample> listSamples = new List<Sample>();

        /* Создает пустой проект
         */
        public Project(String title, Primary prim)
        {
            this.title = title;
            this.path = prim.progPath + "\\" + title;
        }
        /* Загрузить проект из папки path
         */
        public Project(String path)
        {
            this.title = Path.GetFileName(path);
            this.path = path;
        }
        public Project() { }

        /* Проиграть текущий проект
         * int start    - секунда начала воспроизведения
         * int stop     - секунда конца воспроизведения
         */
        public void Play(int start = 0, int stop = -1)
        {

        }
        /* Добавляет в проект сэмпл(оригинал+сэмпл) в указанную позицию
         */
        public void AddSample(Sample s, int pos)
        {

        }
        /* Удаляет из проекта сэмпл на указанной позиции
         */
        public void RemoveSample(Sample s, int pos)
        {

        }

        public void SwapSample(int pos1, int pos2)
        {

        }
        /* Объеденить сэмплы в 1 муз. файл
         * String filename  - название результирующего файла, сохранится по пути path
         */


        private void CreateSampleFile(Sample s)
        {
            TimeSpan start = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(s.SplitStartTimeFromSecond * 1000));
            TimeSpan end = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(s.SplitEndTimeFromSecond * 1000));
            TimeSpan all = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(s.AllTimeFromSecond * 1000));
            SampleController sc = new SampleController();
            if (s.SoundPath.ToString().Contains(".wav"))
            {
                sc.TrimWavFile(SampleController.Converter(s.SoundPath), s.SamplePath, start, all - end);
            }
            else
            {
                sc.TrimWavFile(s.SoundPath, s.SamplePath, start, all - end);
            }
        }

        /* Сохраняет проект в папку path
         */
        public string Save()
        {
            List<string> list = new List<string>();
            foreach (var sample in listSamples)
            {
                try
                {
                    CreateSampleFile(sample);
                    
                }
                catch (Exception ex)
                {
                    return "Не удалось создать файл-отрезок./n" + ex.ToString();
                }
                list.Add(SampleController.Resemple(sample.SamplePath, path + "\\" + "result.mp3"));
                //SampleController.Combine(sample.SamplePath, path + "\\" + "result.wav");
                
            }
            SampleController.Concatenate(list, path + "\\" + "result.mp3");
            WorkerXML.Serialize(this);
            return "Сохранено";
        }
    }
}
