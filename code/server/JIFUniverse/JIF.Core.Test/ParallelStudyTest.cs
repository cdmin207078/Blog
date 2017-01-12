using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;

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


        [TestMethod]
        public void Parallel_Invoke_Test()
        {
            var m1 = new Action(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("m1 use 1s");
            });

            var m2 = new Action(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("m2 use 2s");
            });


            Stopwatch t = new Stopwatch();

            t.Start();
            Parallel.Invoke(m1, m2);
            t.Stop();

            Console.WriteLine("并行执行时长: " + t.ElapsedMilliseconds + "ms");

            t.Restart();
            m1();
            m2();
            t.Stop();

            Console.WriteLine("串行执行时长: " + t.ElapsedMilliseconds + "ms");
        }

        [TestMethod]
        public void Parallel_For_Test()
        {
            Stopwatch t = new Stopwatch();

            t.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            t.Stop();

            Console.WriteLine("串行循环执行时长: " + t.ElapsedMilliseconds + "ms");

            t.Restart();

            Parallel.For(0, 10000, item =>
            {
                for (int i = 0; i < 60000; i++)
                {
                    int sum = 0;
                    sum += item;
                }
            });

            t.Stop();

            Console.WriteLine("并行循环执行时长:" + t.ElapsedMilliseconds + "ms");
        }

        [TestMethod]
        public void Parallel_For_Concurrentbag_Test()
        {
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("\n第{0}次比较", i);

                ConcurrentBag<int> bag = new ConcurrentBag<int>();

                Stopwatch t = new Stopwatch();

                t.Start();
                for (int j = 0; j < 20000000; j++)
                {
                    bag.Add(j);
                }
                t.Stop();


                Console.WriteLine("串行执行时长:" + t.ElapsedMilliseconds + "ms, 集合有: " + bag.Count);

                GC.Collect();


                bag = new ConcurrentBag<int>();

                t.Restart();
                Parallel.For(0, 20000000, j =>
                {
                    bag.Add(j);
                });
                t.Stop();

                Console.WriteLine("并行执行时长:" + t.ElapsedMilliseconds + "ms, 集合有: " + bag.Count);

                GC.Collect();

            }
        }

        [TestMethod]
        public void Parallel_For_Lock_Object_Test()
        {
            var loc = new object();
            var num = 0;


            Stopwatch t = new Stopwatch();

            t.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    num++;
                }
            }

            t.Stop();

            Console.WriteLine("串行循环执行时长: " + t.ElapsedMilliseconds + "ms, num = " + num);

            num = 0;
            t.Restart();

            Parallel.For(0, 10000, item =>
            {
                for (int i = 0; i < 60000; i++)
                {
                    lock (loc)  // 不lock, 数据紊乱
                    {
                        num++;
                    }
                }
            });

            t.Stop();

            Console.WriteLine("并行循环执行时长:" + t.ElapsedMilliseconds + "ms, num = " + num);
        }

        [TestMethod]
        public void Parallel_Foreach_Test()
        {
            List<int> pages = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                pages.Add(i);
            }

            Parallel.ForEach(pages, page =>
            {
                Console.WriteLine(page);
            });
        }

        [TestMethod]
        public void Parallel_Break_Stop_Test()
        {
            for (int i = 0; i < 10000; i++)
            {
                ConcurrentBag<int> bag = new ConcurrentBag<int>();

                Stopwatch t = new Stopwatch();

                t.Start();
                Parallel.For(0, 1000, (j, state) =>
                {
                    if (bag.Count == 300)
                    {
                        state.Break();
                        return;
                    }

                    bag.Add(j);
                });
                t.Stop();

                //Console.WriteLine("第{0}次 break 运行, bag.count = {1}", i, bag.Count);

                bag = new ConcurrentBag<int>();

                t.Restart();
                Parallel.For(0, 1000, (j, state) =>
                {
                    if (bag.Count == 300)
                    {
                        state.Stop();
                        return;
                    }

                    bag.Add(j);
                });
                t.Stop();

                Console.WriteLine("第{0}次 stop 运行, bag.count = {1}", i, bag.Count);

            }
        }

        [TestMethod]
        public void Parallel_Exception_Test()
        {
            var m1 = new Action(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("m1 use 1s");
                throw new Exception("Exception in m1");
            });

            var m2 = new Action(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("m2 use 2s");
                throw new Exception("Exception in m2");
            });

            Stopwatch t = new Stopwatch();

            t.Start();
            try
            {
                Parallel.Invoke(m1, m2);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            t.Stop();

            Console.WriteLine("并行执行时长: " + t.ElapsedMilliseconds + "ms");

            t.Restart();
            try
            {
                m1();
                m2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            t.Stop();

            Console.WriteLine("串行执行时长: " + t.ElapsedMilliseconds + "ms");
        }
    }
}
