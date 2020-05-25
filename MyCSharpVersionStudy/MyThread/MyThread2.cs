using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread2
    {
        public class Singleton
        {
            private static Singleton instance;
            private static object mut = new object();

            private Singleton() //私有函数,防止实例化
            {
            }

            public static Singleton GetInstance()
            {
                lock (mut)//通过Lock关键字实现同步 , 但是不能lock null
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
                return instance;
            }
        }


        public void Test01()
        {
            Thread threadA = new Thread(RunThread01);
            threadA.Name = "AAA";
            Thread threadB = new Thread(RunThread01);
            threadB.Name = "BBB";
            threadA.Start();
            threadB.Start();
            Console.ReadLine();
        }

        public void RunThread01(object arg)
        {
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"我是{Thread.CurrentThread.Name},Number{i}");
                Thread.Sleep(300);
            }
        }

        public void Test02()
        {
            Thread threadA = new Thread(RunThread02);
            threadA.Name = "AAA";
            Thread threadB = new Thread(RunThread02);
            threadB.Name = "BBB";
            threadA.Start();
            threadB.Start();
            Console.ReadLine();
        }

        public void RunThread02(object arg)
        {
            lock (this)
            {
                for (int i = 0; i < 10; ++i)
                {
                    Console.WriteLine($"我是{Thread.CurrentThread.Name},Number{i}");
                    Thread.Sleep(300);
                }
            }
        }


        public class ThreadCls
        {
            public void RunThread()
            {
                lock (Singleton.GetInstance())
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        Console.WriteLine($"我是{Thread.CurrentThread.Name},Number{i}");
                        Thread.Sleep(300);
                    }
                }
            }
        }


        public void Test03()
        {
            ThreadCls cls0 = new ThreadCls();
            ThreadCls cls1 = new ThreadCls();


            Thread threadA = new Thread(cls0.RunThread);
            threadA.Name = "AAA";
            Thread threadB = new Thread(cls1.RunThread);
            threadB.Name = "BBB";
            threadA.Start();
            threadB.Start();
            Console.ReadLine();
        }
    }
}
