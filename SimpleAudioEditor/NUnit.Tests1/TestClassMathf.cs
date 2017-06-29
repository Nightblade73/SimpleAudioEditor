using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAudioEditor.PeachStudio;

namespace NUnit.Tests1
{
    [TestFixture]
    public class TestClassMathf
    {
        [Test]
        public void TestFirst()
        {
            Assert.Pass("Hello");
        }

        [Test]
        ///есть отрезок (0;10) и точка А(50). 
        ///если точка входит в отрезок, возвращается ее координата. 
        ///если нет - координата конца отрезка, за которой лежит эта точка.
        ///ожидаемый результат 10(последняя переменная)
        ///т.к. 50 лежит вне отрезка, функция возвращает верхнюю границу - 10.
        /// 10 = 10.положительный тест пройден
        public void TestMethodClamp1True()
        {
            Assert.AreEqual(Mathf.Clamp((int)50, (int)0, (int)10), 10);
        }

        [Test]
        ///аналогичный тест, но на отрицание условия.
        /// (0;10), А(50), ожидаемый результат - 50
        /// т.к. функция вернет 10, то 10 != 50.
        /// отрицательный тест пройден
        public void TestMethodClamp1False()
        {
            Assert.AreNotEqual(Mathf.Clamp((int)50, (int)0, (int)10), 50);
        }

        [Test]
        ///аналогичный тест +
        public void TestMethodClamp2True()
        {
            Assert.AreEqual(Mathf.Clamp((double)4, (double)10, (double)50), 10);
        }

        [Test]
        ///аналогичный тест - 
        public void TestMethodClamp2False()
        {
            Assert.AreNotEqual(Mathf.Clamp((double)4, (double)0, (double)50), 10);
        }

        [Test]
        ///аналогичный тест +
        public void TestMethodClamp3True()
        {

            Assert.AreEqual(Mathf.Clamp(new TimeSpan(0, 0, 50), new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 40)), new TimeSpan(0, 0, 40));
        }

        [Test]
        ///аналогичный тест -
        public void TestMethodClamp3False()
        {

            Assert.AreNotEqual(Mathf.Clamp(new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 15)), new TimeSpan(0, 0, 15));
        }

        [Test]
        ///1 переменная - сколько секунд прошло (2)
        ///2 переменная - сколько секунд всего (5)
        ///3 переменная - сколько всего пикселей (20)
        ///ожидаемый результат - сколкьо пикселей прошло (8)
        /// позитивный тест. уравнение с одной неизвестной х = 20 / 5 * 2
        /// х = 8
        /// 8 = 8. тест пройден
        public void TestMethodTimeToPosTrue()
        {

            Assert.AreEqual(Mathf.TimeToPos(new TimeSpan(0, 0, 2), new TimeSpan(0, 0, 5), (int) 20), (int) 8);
        }

        [Test]
        ///аналогичный тест -       
        ///ожидаемый результат - сколкьо пикселей прошло (20)
        ///20 != 8
        ///тест пройден
        public void TestMethodTimeToPosFalse()
        {

            Assert.AreNotEqual(Mathf.TimeToPos(new TimeSpan(0, 0, 2), new TimeSpan(0, 0, 5), (int)20), (int)20);
        }

        [Test]
        ///обратный тест.
        ///1 переменная - сколько пикселей прошло (10)
        ///2 переменная - сколько пикселей всего (40)
        ///3 переменная - сколько всего секунд (4)
        ///ожидаемый результат - сколкьо секунд прошло (1)
        /// позитивный тест. уравнение с одной неизвестной х = 4 / 40 * 10 
        /// х = 1
        /// 1 = 1. тест пройден
        public void TestMethodPosToTimeTrue()
        {

            Assert.AreEqual(Mathf.PosToTime((int) 10, (int)40, new TimeSpan(0, 0, 4)), new TimeSpan(0, 0, 1));
        }

        [Test]
        ///аналогичный тест -   
        ///ожидаемый результат - сколкьо секунд прошло (40)
        ///1 != 40
        ///тест пройден
        public void TestMethodPosToTimeFalse()
        {

            Assert.AreNotEqual(Mathf.PosToTime((int)10, (int)40, new TimeSpan(0, 0, 8)), new TimeSpan(0, 0, 40));
        }
    }
}
