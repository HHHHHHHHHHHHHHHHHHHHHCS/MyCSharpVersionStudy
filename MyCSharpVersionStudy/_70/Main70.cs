using System;
using System.Threading.Tasks;

namespace MyCSharpVersionStudy._70
{
    public class Main70
    {
        /// <summary>
        /// out 变量
        /// </summary>
        public void Test01()
        {
            if (int.TryParse("23", out var answer))
            {
                Console.WriteLine(answer);
            }
            else
            {
                Console.WriteLine("Can't parse");
            }
        }

        public class Point
        {
            public double X { get; }
            public double Y { get; }

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public Point(double x, double y) => (X, Y) = (x, y);

            /// <summary>
            /// 解元
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
        }

        /// <summary>
        /// 元组
        /// </summary>
        public void Test02()
        {
            (string Alpha, string Beta) namedLetters = ("a", "b");
            Console.WriteLine($"{namedLetters.Alpha}, {namedLetters.Beta}");

            var alphaBeta = (Alpha: "a", Beta: "b");
            Console.WriteLine($"{alphaBeta.Alpha}, {alphaBeta.Beta}");

            var p = new Point(3.14, 2.71);
            //解元
            (double x, double y) = p;


            var left = (a: 5, b: 10);
            var right = (a: 5, b: 10);
            Console.WriteLine(left == right); // displays 'true'


            (int a, int b)? nullableTuple = right;
            Console.WriteLine(left == nullableTuple); // Also true

        }

        public (string, double, int, int, int) QiYuan(string name, double d, int x, int y, int z)
        {
            return (name, d, x, y, z);
        }

        /// <summary>
        /// 弃元
        /// </summary>
        public void Test03()
        {
            var (_, _, _, y, z) = QiYuan("a", 3d, 4, 5, 6);
            Console.WriteLine($"{y},{z}");
        }


        /// <summary>
        /// 模式匹配
        /// </summary>
        public void Test04()
        {
            object[] obj = new object[] { 3d, null, 4d, 5d, 6, 0, 7, 8, 9, 3f, 2f };
            int count = 0;
            foreach (var item in obj)
            {
                if (item is int c)
                {
                    count += c;
                }
            }

            Console.WriteLine("count : " + count);

            foreach (var i in obj)
            {
                switch (i)
                {
                    case 0:
                        {
                            Console.WriteLine("Input Is Zero");
                            break;
                        }
                    case int n when n >= 8:
                        {
                            Console.WriteLine("Input >= 8 ---->" + n);
                            break;
                        }
                    case null:
                        {
                            Console.WriteLine("Input is null");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(i);
                            break;
                        }
                }
            }
        }


        public static ref int Find(int[] arr, int index)
        {
            return ref arr[index];
        }

        /// <summary>
        /// Ref 局部变量和返回结果
        /// </summary>
        public void Test05()
        {
            int[] xx = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            ref int i = ref Find(xx, 4);

            i = 99;

            Console.WriteLine(xx[4]);
        }


        /// <summary>
        /// 本地函数
        /// </summary>
        public void Test06()
        {
            Act0();

            void Act0()
            {
                int[] xx = new int[] { 1, 2, 3, 4, 5 };
                Act1(xx);
                Act1(xx);
                Act1(xx);

                foreach (int z in xx)
                {
                    Console.WriteLine(z);
                }


                void Act1(int[] objs)
                {
                    for (int i = 0; i < objs.Length; ++i)
                    {
                        objs[i]++;
                    }
                }
            }
        }

        /// <summary>
        /// 异步本地函数
        /// </summary>
        public Task<string> PerformLongRunningWork(string address, int index, string name)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(message: "An address is required", paramName: nameof(address));
            if (index < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "The index must be non-negative");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(message: "You must supply a name", paramName: nameof(name));

            return LongRunningWorkImplementation();

            async Task<string> LongRunningWorkImplementation()
            {
                var interimResult = await FirstWork(address);
                var secondResult = await SecondStep(index, name);
                return $"The results are {interimResult} and {secondResult}. Enjoy.";
            }

            async Task<int> FirstWork(string _a)
            {
                await Task.Run(() => Console.WriteLine(_a));
                return 0;
            }

            async Task<double> SecondStep(int _i, string _n)
            {
                return (double)_i;
            }
        }



        public class ExpressionMembersExample
        {
            // Expression-bodied constructor
            public ExpressionMembersExample(string label) => this.Label = label;

            // Expression-bodied finalizer
            ~ExpressionMembersExample() => Console.Error.WriteLine("Finalized!");

            private string label;

            // Expression-bodied get / set accessors.
            public string Label
            {
                get => label;
                set => this.label = value ?? "Default label";
            }
        }

        /// <summary>
        /// 更多的 expression-bodied 成员
        /// </summary>
        public void Test07()
        {
            new ExpressionMembersExample(null);
        }

        /// <summary>
        /// 引发表达式
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        DateTime ToDateTime(IFormatProvider provider) =>
         throw new InvalidCastException("Conversion to a DateTime is not supported.");


        /// <summary>
        /// 通用的异步返回类型
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> Func()
        {
            await Task.Delay(100);
            return 5;
        }

        /// <summary>
        /// 数字文本语法改进
        /// </summary>
        public const int Sixteen = 0b0001_0000;
        public const int ThirtyTwo = 0b0010_0000;
        public const int SixtyFour = 0b0100_0000;
        public const int OneHundredTwentyEight = 0b1000_0000;
        public const long BillionsAndBillions = 100_000_000_000;
        public const double AvogadroConstant = 6.022_140_857_747_474e23;
        public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;
    }

}