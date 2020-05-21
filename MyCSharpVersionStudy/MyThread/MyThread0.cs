using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread0
    {
        public void Test01()
        {
            ThreadStart childRef = new ThreadStart(DoChild);
            Thread child = new Thread(childRef);
            child.Name = "Child";
            child.Start();

            Thread.Sleep(5000);

            Thread th = Thread.CurrentThread;
            th.Name = "MainThread";
            Console.WriteLine(th.Name);
        }

        public void DoChild()
        {
            Thread.Sleep(2500);

            Console.WriteLine(Thread.CurrentThread.Name);
        }


        public void Test02()
        {
            ThreadStart ts = new ThreadStart(OutNumber);
            Thread th = new Thread(ts);

            th.Start();

            Thread.Sleep(5000);

            //.net core 不支持
            th.Abort();
        }

        public void OutNumber()
        {
            try
            {
                for (int i = 0; i < 10; ++i)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(i);
                }
            }
            catch(ThreadAbortException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {

            }
        }

        public void Test03()
        {
            Thread th = new Thread(OutNumber);
            th.Start();
        }
    }
}
