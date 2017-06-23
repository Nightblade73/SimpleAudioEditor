using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioEditor.Controller
{
    class Sample
    {
        public String title;
        /* Путь к оригинальной звуковой дорожке
         * (не обрезанной)
         */
        public String soundPath;
        /* Путь к сэмплу (обрезанной звуковой дорожке)
         * для каждого сэмпла, нужно создавать новый _экземпляр_ оригинальной дорожки
         */
        public String samplePath;
        public int start;
        public int stop;
        
        /* Воспроизвести оригинальную звуковую дорожку
         */
        public void Play()
        {

        }
        /* Воспроизвести сэмпл, если указать start, stop обрезает и воспроизводит, но сэмпл не сохраняет
         */
        public void Play(int start = -1, int stop = -1)
        {

        }
        /* Останавливает воспроизведение звука
         */
        public void Stop()
        {

        }
        /* Обрезает оригинальную дорожку по указанным таймингам
         */
        public void Trim(int start, int stop)
        {

        }
    }
}
