using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread5
    {
        private static Mutex mut = new Mutex();

        public void Test00()
        {
            Thread[] thread = new Thread[3];
            for(int i=0;i<3;++i)
            {
                thread[i] = new Thread(ThreadRun);
                thread[i].Name = i.ToString();
            }

            for(int i=0;i<3;++i)
            {
                thread[i].Start();
            }
            Console.ReadLine();
        }

        public void ThreadRun(object val)
        {
            mut.WaitOne();
            for(int i=0;i<500;++i)
            {
                Thread.Sleep(1);
                Console.Write(Thread.CurrentThread.Name);
            }
            mut.ReleaseMutex();
        }
    }
}
