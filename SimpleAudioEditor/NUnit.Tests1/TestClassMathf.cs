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
        public void TestMethodClamp1True()
        {
            Assert.AreEqual(Mathf.Clamp((int)50, (int)0, (int)10), 10);
        }

        [Test]
        public void TestMethodClamp1False()
        {
            Assert.AreNotEqual(Mathf.Clamp((int)50, (int)0, (int)10), 50);
        }

        [Test]
        public void TestMethodClamp2True()
        {
            Assert.AreEqual(Mathf.Clamp((double)4, (double)10, (double)50), 10);
        }

        [Test]
        public void TestMethodClamp2False()
        {
            Assert.AreNotEqual(Mathf.Clamp((double)4, (double)0, (double)50), 10);
        }

        [Test]
        public void TestMethodClamp3True()
        {

            Assert.AreEqual(Mathf.Clamp(new TimeSpan(0, 0, 50), new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 40)), new TimeSpan(0, 0, 40));
        }

        [Test]
        public void TestMethodClamp3False()
        {

            Assert.AreEqual(Mathf.Clamp(new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 15)), new TimeSpan(0, 0, 15));
        }


    }
}
