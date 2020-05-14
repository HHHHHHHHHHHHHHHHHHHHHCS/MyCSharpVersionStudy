using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpVersionStudy._71
{
    public class Main71
    {

        /// <summary>
        /// 异步 main 方法
        /// </summary>
        /// <returns></returns>
        static async Task<int> Main()
        {
            return await Task.Run(() => { return 0; });
        }

        /*
        static async Task Main()
        {
            await Task.Run(() => { Console.WriteLine(1); });
        }
        */


        /// <summary>
        /// 默认文本表达式
        /// </summary>
        public void Test01()
        {
            //老的
            //Func<string, bool> whereClause = default(Func<string, bool>);

            //新的
            Func<string, bool> whereClause = default;
        }


        public void Test02()
        {
            int count = 5;
            string label = "Colors used in the map";

            //老的
            //var pair = (count: count, label: label);

            var pair = (count, label);

            Console.WriteLine(pair);
        }

        /// <summary>
        /// 泛型类型参数的模式匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Test03<T>()
        {
            int x = 0;
            switch(x)
            {
                case T z:
                    break;
            }
        }


        //引用程序集生成
        //-refout 和 -refonly 
    }
}
