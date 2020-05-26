using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread3
    {
        public static object obj = new object();

        //Enter(Object) 在指定对象上获取排他锁。
        //Exit(Object) 释放指定对象上的排他锁。 
        //Pulse 通知等待队列中的线程锁定对象状态的更改。
        //PulseAll 通知所有的等待线程对象状态的更改。
        //TryEnter(Object) 试图获取指定对象的排他锁。
        //TryEnter(Object, Boolean) 尝试获取指定对象上的排他锁，并自动设置一个值，指示是否得到了该锁。
        //Wait(Object) 释放对象上的锁并阻止当前线程，直到它重新获取该锁。
        public void Test00()
        {
            Thread thA = new Thread(RunThread0);
            thA.Name = "AAA";
            Thread thB = new Thread(RunThread0);
            thB.Name = "BBB";
            thA.Start();
            thB.Start();
            Thread.CurrentThread.Name = "CCC";
            RunThread0();
            Console.ReadLine();
        }

        /// <summary>
        /// Monitor.Enter(obj);
        /// Monitor.Exit(obj);
        /// 类似于lock(obj){}
        /// </summary>
        public void RunThread0()
        {
            Monitor.Enter(obj);

            try
            {
                for(int i =0;i<10;++i)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(Thread.CurrentThread.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }


        public void Test01()
        {
            Thread thA = new Thread(RunThread1);
            thA.Name = "AAA";
            Thread thB = new Thread(RunThread1);
            thB.Name = "BBB";
            thA.Start();
            thB.Start();
            Thread.CurrentThread.Name = "CCC";
            RunThread1();
            Console.ReadLine();
        }

        public void RunThread1()
        {
            bool flag = Monitor.TryEnter(obj, 1000);
            //设置1S的超时时间，如果在1S之内没有获得同步锁，则返回false
            //上面的代码设置了锁定超时时间为1秒，也就是说，在1秒后，
            //lockObj还未被解锁，TryEntry方法就会返回false，如果在1秒之内，lockObj被解锁，TryEntry返回true。我们可以使用这种方法来避免死锁

            try
            {
                if (flag)
                {
                    for (int i = 0; i < 500; i++)
                    {
                        Thread.Sleep(10);
                        Console.Write(Thread.CurrentThread.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Time Out!");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (flag)
                    Monitor.Exit(obj);
            }
        }
    }
}
