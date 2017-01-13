using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void Parallel_Safety_Conllection()
        {
            ConcurrentBag<int> safe_list = new ConcurrentBag<int>();

            Parallel.For(0, 100000, i =>
            {
                safe_list.Add(i);
            });

            Console.WriteLine("safe_list count is: {0}", safe_list.Count);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("safe_list[{0}] = {1}", i, safe_list.ElementAt(i));
            }

            // list 是 非线程安全的,此处用多线程访问list 会导致结果错误,或直接报错.
            List<int> list = new List<int>();

            Parallel.For(0, 100000, i =>
            {
                list.Add(i);
            });

            Console.WriteLine("list count is : {0}", list.Count);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("list[{0}] = {1}", i, list.ElementAt(i));
            }
        }

        [TestMethod]
        public void Parallel_Safety_Dictionary()
        {
            Stopwatch t = new Stopwatch();

            ConcurrentDictionary<int, string> safe_dic = new ConcurrentDictionary<int, string>();

            t.Start();
            Parallel.For(0, 1000000, i =>
            {
                safe_dic.TryAdd(i, i.ToString());
            });
            t.Stop();

            Console.WriteLine("safe_dic count is: {0}, 耗时: {1}", safe_dic.Count, t.ElapsedMilliseconds);

            for (int i = 10000; i < 10300; i++)
            {
                Console.WriteLine("safe_dic[{0}] = {1}", i, safe_dic[i]);
            }

            //dic 非线程安全,有几率直接报错.
            Dictionary<int, string> dic = new Dictionary<int, string>();

            Parallel.For(0, 1000000, i =>
            {
                dic.Add(i, i.ToString());
            });

            Console.WriteLine("dic count is: {0}", dic.Count);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("dic[{0}] = {1}", i, dic.ElementAt(i));
            }

        }

        [TestMethod]
        public void MyTestMethod()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();

            Parallel.For(1, 10, i =>
            {
                list.Add(i);
            });

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("count : {0}", list.Count);


            ConcurrentDictionary<int, string> dic = new ConcurrentDictionary<int, string>();

            Parallel.For(1, 10, i =>
            {
                dic.TryAdd(i, i.ToString());
            });

            foreach (var item in dic)
            {
                Console.WriteLine("key: {0}, val: {1}", item.Key, item.Value);
            }
            Console.WriteLine("count : {0}", dic.Count);


            Console.WriteLine("------ s - e ---------");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("start: {0}, end: {1}", (i * 5 + 1), ((i + 1) * 5 + 1));
            }
        }

        [TestMethod]
        public void ConCurrentDictionary_Desc_Test()
        {
            //ConcurrentDictionary<int, string> safe_dic = new ConcurrentDictionary<int, string>();

            //safe_dic.TryAdd(5, "5");
            //safe_dic.TryAdd(3, "3");
            //safe_dic.TryAdd(2, "2");
            //safe_dic.TryAdd(4, "4");
            //safe_dic.TryAdd(1, "1");


            //foreach (var item in safe_dic)
            //{
            //    Console.WriteLine("key : {0}, val : {1}", item.Key, item.Value);
            //}

            //var list = safe_dic.Values.ToList();

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}


            //Dictionary<int, string> dic = new Dictionary<int, string>();

            //dic.Add(5, "5");
            //dic.Add(3, "3");
            //dic.Add(2, "2");
            //dic.Add(4, "4");
            //dic.Add(1, "1");


            //foreach (var item in dic)
            //{
            //    Console.WriteLine("key : {0}, val : {1}", item.Key, item.Value);
            //}

            //var aaa = dic.Values.ToList();

            //foreach (var item in aaa)
            //{
            //    Console.WriteLine(item);
            //}

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random(1).Next(1, 1000));
            }

            ConcurrentDictionary<int, List<int>> diclist = new ConcurrentDictionary<int, List<int>>();

            diclist.TryAdd(4, new List<int> { 10, 11, 12 });
            diclist.TryAdd(3, new List<int> { 7, 8, 9 });
            diclist.TryAdd(1, new List<int> { 1, 2, 3 });
            diclist.TryAdd(2, new List<int> { 4, 5, 6 });

            var re = diclist.Values.Aggregate((a, b) => a.Concat(b).ToList());

            foreach (var item in re)
            {
                Console.WriteLine(item);
            }
        }
    }
}
