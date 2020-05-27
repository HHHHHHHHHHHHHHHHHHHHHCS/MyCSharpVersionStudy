using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread6
    {
        //线程池最多管理线程数量 = 处理器数 * 250
        //2个2核心CPU 默认上限为1000

        public void Test00()
        {
            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
            //ThreadPool.SetMaxThreads(int workerThreads, int completionPortThreads);
            ThreadPool.GetAvailableThreads(out int _workerThreads, out int _completionPortThreads);
            Console.WriteLine($"GetMaxThreads : workerThreads:{workerThreads} , completionPortThreads:{completionPortThreads}");
            Console.WriteLine($"GetAvailableThreads : workerThreads:{workerThreads} , completionPortThreads:{completionPortThreads}");

            int threadNum = 10;
            ManualResetEvent[] resetEvents = new ManualResetEvent[threadNum];
            for (int i = 0; i < threadNum; ++i)
            {
                resetEvents[i] = new ManualResetEvent(false);
                //ThreadPool.QueueUserWorkItem(RunThread, resetEvents[i]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(RunThread), resetEvents[i]);
            }
            WaitHandle.WaitAll(resetEvents);
            Console.WriteLine("End");
        }

        public void RunThread(object val)
        {
            ManualResetEvent e = (ManualResetEvent)val;

            for (int i = 0; i < 5000; ++i)
            {
                if (i % 500 == 0)
                {
                    Thread.Sleep(1);
                    Console.WriteLine(i.ToString());
                }
            }

            e.Set();//set 让信号释放掉
        }
    }
}
