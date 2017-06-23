using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    class Project
    {
        public String title;
        public String path;
        public Dictionary<int,Sample> samples;

        /* Создает пустой проект
         */
        public Project()
        {

        }
        /* Загрузить проект из папки path
         */
        public Project(String path)
        {

        }
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
        /* Объеденить сэмплы в 1 муз. файл
         * String filename  - название результирующего файла, сохранится по пути path
         */
        public void Combine(String filename)
        {

        }
        /* Сохраняет проект в папку path
         */
        public void Save()
        {

        }
    }
}
