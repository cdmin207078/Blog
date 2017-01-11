using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace JIF.Core.Test
{
    [TestClass]
    public class ParallelStudyTest
    {

        [TestMethod]
        public void Create_Thread()
        {
            Thread t = new Thread(WriteY);
            t.Start();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("x-" + i);
            }
        }

        private void WriteY()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("y-" + i);
            }
        }
    }
}
