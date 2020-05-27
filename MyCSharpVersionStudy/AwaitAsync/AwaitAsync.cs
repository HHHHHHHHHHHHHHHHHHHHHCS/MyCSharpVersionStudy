using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCSharpVersionStudy.AwaitAsync
{
    public class AwaitAsync0
    {
        public void Test00()
        {
            Console.WriteLine("000 Id:" + Thread.CurrentThread.ManagedThreadId);
            var t = AsyncTask();
            Console.WriteLine("001 Id:" + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Console.WriteLine("IsCompleted : " + t.IsCompleted);
        }

        private async Task AsyncTask()
        {
            var method = RunTask();
            string result = await method + " AsyncTask ID : " + Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(result);
        }

        private Task<string> RunTask()
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("Task:" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);
                Console.WriteLine("Task Thread.Sleep(5000) :" + Thread.CurrentThread.ManagedThreadId);
                return "I am end";
            });

            return task;
        }
    }
}
