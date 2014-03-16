using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNA.Patterns.Commands;

namespace DNA.Patterns.Test
{
    [TestClass]
    public class CommandPatternTest
    {
        [TestMethod]
        public void MarcoTest()
        {
            var a = 1;
            var b = 2;
            var actual = 0;
            var expected = 5;

            var cmdA = new Command((parameters) =>
            {
                a++;
            });

            var cmdB = new Command((parameters) =>
            {
                b++;
            });

            var cmdC = new Command((parameters) =>
            {
                actual = a + b;
            });

            /// Another way to execute dynamic command: 
            /// var maco=new Macro();
            /// macro.Add(() => { a++; }, () => { b++; }, () => { actual = a + b; });

            var macro = new Macro(cmdA, cmdB, cmdC);
            macro.Call();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TranstractionTest()
        {
        }
    }


}
