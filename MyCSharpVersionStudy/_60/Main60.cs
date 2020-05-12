using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
//using static 增强功能可用于导入单个类的静态方法
using static System.String;
//静态导入可以导入扩展方法
using static System.Linq.Enumerable;
using System.Threading.Tasks;

namespace MyCSharpVersionStudy._60
{
    public class Main60
    {
        /// <summary>
        /// 只读自动属性
        /// </summary>
        public class Student
        {
            public string FirstName { get; }
            public string LastName { get; }

            public Student(string firstName, string lastName)
            {
                if (string.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentException(message: "Cannot be blank", paramName: nameof(lastName));
                FirstName = firstName;
                LastName = lastName;
            }

            public void ChangeName(string newLastName)
            {
                //只读不能修改
                // Generates CS0200: Property or indexer cannot be assigned to -- it is read only
                //LastName = newLastName;
            }

            /// <summary>
            /// Expression-bodied 函数成员
            /// 字符串内插
            /// </summary>
            public string FullName => $"{FirstName} {LastName}";
        }

        /// <summary>
        /// 自动属性初始化表达式
        /// </summary>
        public ICollection<double> Grades { get; } = new List<double>();

        public void Act(object o, EventArgs args)
        {

        }

        /// <summary>
        /// Null 条件运算符
        /// </summary>
        public void Test01()
        {
            Student person = new Student("a", "b");
            var first = person?.FirstName ?? "Unspecified";


            EventArgs args = new EventArgs();
            Action<object, EventArgs> act = Act;
            act?.Invoke(this, args);

        }

        public void DoMain()
        {
            //字符串内插
            float xx = 123.45678f;
            Console.WriteLine($"{xx:F2}");

            //CultureInfo 切换国家符合习惯
            FormattableString str = $"Average grade is {xx}";
            var gradeStr = str.ToString(new System.Globalization.CultureInfo("de-DE"));
            Console.WriteLine(gradeStr);

            //nameof 表达式
            Student student = new Student("A", "B");
            Console.WriteLine(nameof(student.FirstName));

        }

        /// <summary>
        /// 异常筛选器
        /// </summary>
        /// <returns></returns>
        public static async Task<string> MakeRequest()
        {
            HttpMessageHandler httpMessageHandler = null;
            using (HttpClient client = new HttpClient(httpMessageHandler))
            {
                var stringTask = client.GetStringAsync("https://docs.microsoft.com/en-us/dotnet/about/");
                try
                {
                    var responseText = await stringTask;
                    return responseText;
                }
                catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
                {
                    return "Site Moved";
                }
            }
        }

        public static async Task LogMethodEntrance()
        {
            await Task.Delay(1);
        }

        public static async Task LogError(string str, HttpRequestException e)
        {
            await Task.Delay(1);
        }

        public static async Task LogMethodExit()
        {
            await Task.Delay(1);
        }


        /// <summary>
        /// Catch 和 Finally 块中的 Await
        /// </summary>
        /// <returns></returns>
        public static async Task<string> MakeRequestAndLogFailures()
        {
            //await 可能存在丢失异常
            //鉴于此行为，建议仔细编写 catch 和 finally 子句，避免引入新的异常

            await LogMethodEntrance();
            var client = new System.Net.Http.HttpClient();
            var streamTask = client.GetStringAsync("https://localHost:10000");
            try
            {
                var responseText = await streamTask;
                return responseText;
            }
            catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
            {
                await LogError("Recovered from redirect", e);
                return "Site Moved";
            }
            finally
            {
                await LogMethodExit();
                client.Dispose();
            }
        }

        /// <summary>
        /// 使用索引器初始化关联集合
        /// 需要容器扩展了 Add 方法
        /// </summary>
        private Dictionary<int, string> webErrors = new Dictionary<int, string>
        {
            [404] = "Page not Found",
            [302] = "Page moved, but left a forwarding address.",
            [500] = "The web server can't come out to play today."
        };


        public static Task Things()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 改进了重载解析
        /// 可以区分更多
        /// </summary>
        public static void DoThings()
        {
            Task.Run(DoThings);
        }
    }
}
