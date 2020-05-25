using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread1
    {
        //https://www.cnblogs.com/wwj1992/p/5976096.html

        /*
         * 　Abort()方法用来终止线程
         *   ResetAbort()方法可以取消Abort()的操作
         *   Sleep()方法调已阻塞线程
         *   Join()方法主要是用来阻塞调用线程，直到某个线程终止或经过了指定时间为止
         */


        public struct EventArgs
        {
            public int start;
            public int count;
        }

        public static int count = 0;


        public void Test01()
        {
            count = 5;

            for (int i = 0; i < count; ++i)
            {
                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(DoThing);

                Thread th = new Thread(ParStart);
                //线程的优先级
                th.Priority = (ThreadPriority)(i % 5);
                EventArgs args = new EventArgs() { start = i * 10, count = 10 };

                th.Start(args);
            }


            while (count != 0)
            {
                Thread.Sleep(10);
            }

            Console.WriteLine("End");
        }


        private void DoThing(object o)
        {
            if (o is EventArgs args)
            {
                for (int i = args.start; i < args.start + args.count; ++i)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(i);
                }

                Interlocked.Decrement(ref count);
            }
        }

        public void Test02()
        {
            count = 5;

            for (int i = 0; i < count; ++i)
            {
                ThreadOut to = new ThreadOut(i * 10, 10);
                Thread thread = new Thread(new ThreadStart(to.Out));
                thread.Start();
            }

            while (count != 0)
            {
                Thread.Sleep(10);
            }

            Console.WriteLine("End");
        }


        public class ThreadOut
        {
            public int Start { get; set; }
            public int Count { get; set; }

            public ThreadOut(int s, int c) => (Start, Count) = (s, c);

            public void Out()
            {
                for (int i = Start; i < (Start + Count); ++i)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(i);
                }

                Interlocked.Decrement(ref count);
            }

        }
    }
}
