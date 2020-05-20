using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpVersionStudy._80
{
    public class Main80
    {

        /// <summary>
        /// Readonly 成员
        /// 不会产生副本
        /// </summary>
        public struct Point
        {
            /// <summary>
            ///  Getter 视为 readonly
            /// </summary>
            public double X { get; set; }
            public double Y { get; set; }

            /// <summary>
            /// 如果这个没有比较readonly  则会产生副本
            /// </summary>
            public readonly double Distance => Math.Sqrt(X * X + Y * Y);

            /// <summary>
            /// 不会产生副本
            /// </summary>
            /// <returns></returns>
            public readonly override string ToString() => $"({X}, {Y}) is {Distance} from the origin";

            //readonly 不能干的事情
            //public readonly void Translate(int xOffset, int yOffset)
            //{
            //    X += xOffset;
            //    Y += yOffset;
            //}
        }

        #region 默认接口方法
        public interface IOrder
        {
            DateTime Purchased { get; }
            decimal Cost { get; }
        }

        /// <summary>
        /// 默认接口方法
        /// </summary>
        public interface ICustomer
        {
            IEnumerable<IOrder> PreviousOrders { get; }

            DateTime DateJoined { get; }
            DateTime? LastOrder { get; }
            string Name { get; }
            IDictionary<DateTime, string> Reminders { get; }

            /*
            // <SnippetLoyaltyDiscountVersionOne>
            // Version 1: 使用默认接口方法升级
            public decimal ComputeLoyaltyDiscount()
            {
                DateTime TwoYearsAgo = DateTime.Now.AddYears(-2);
                if ((DateJoined < TwoYearsAgo) && (PreviousOrders.Count() > 10))
                {
                    return 0.10m;
                }
                return 0;
            }
            // </SnippetLoyaltyDiscountVersionOne>
            */

            /*
            // <SnippetLoyaltyDiscountVersionTwo>
            // Version 2:提供参数化
            public static void SetLoyaltyThresholds(
                TimeSpan ago,
                int minimumOrders = 10,
                decimal percentageDiscount = 0.10m)
            {
                length = ago;
                orderCount = minimumOrders;
                discountPercent = percentageDiscount;
            }
            private static TimeSpan length = new TimeSpan(365 * 2, 0,0,0); // two years
            private static int orderCount = 10;
            private static decimal discountPercent = 0.10m;
            public decimal ComputeLoyaltyDiscount()
            {
                DateTime start = DateTime.Now - length;
                if ((DateJoined < start) && (PreviousOrders.Count() > orderCount))
                {
                    return discountPercent;
                }
                return 0;
            }
            // </SnippetLoyaltyDiscountVersionTwo>
            */

            // Version 3: 扩展默认实现
            public static void SetLoyaltyThresholds(TimeSpan ago, int minimumOrders, decimal percentageDiscount)
            {
                length = ago;
                orderCount = minimumOrders;
                discountPercent = percentageDiscount;
            }
            private static TimeSpan length = new TimeSpan(365 * 2, 0, 0, 0); // two years
            private static int orderCount = 10;
            private static decimal discountPercent = 0.10m;

            // <SnippetFinalVersion>
            public decimal ComputeLoyaltyDiscount() => DefaultLoyaltyDiscount(this);
            protected static decimal DefaultLoyaltyDiscount(ICustomer c)
            {
                DateTime start = DateTime.Now - length;

                if ((c.DateJoined < start) && (c.PreviousOrders.Count() > orderCount))
                {
                    return discountPercent;
                }
                return 0;
            }
            // </SnippetFinalVersion>
        }

        public class SampleOrder : IOrder
        {
            public SampleOrder(DateTime purchase, decimal cost) =>
                (Purchased, Cost) = (purchase, cost);

            public DateTime Purchased { get; }

            public decimal Cost { get; }
        }

        public class SampleCustomer : ICustomer
        {
            public SampleCustomer(string name, DateTime dateJoined) =>
            (Name, DateJoined) = (name, dateJoined);

            private List<IOrder> allOrders = new List<IOrder>();

            public IEnumerable<IOrder> PreviousOrders => allOrders;

            public DateTime DateJoined { get; }

            public DateTime? LastOrder { get; private set; }

            public string Name { get; }

            private Dictionary<DateTime, string> reminders = new Dictionary<DateTime, string>();
            public IDictionary<DateTime, string> Reminders => reminders;

            public void AddOrder(IOrder order)
            {
                if (order.Purchased > (LastOrder ?? DateTime.MinValue))
                    LastOrder = order.Purchased;
                allOrders.Add(order);
            }

            public decimal ComputeLoyaltyDiscount()
            {
                if (PreviousOrders.Any() == false)
                    return 0.50m;
                else
                    return ICustomer.DefaultLoyaltyDiscount(this);
            }
        }

        public void Test02()
        {
            SampleCustomer c = new SampleCustomer("customer one", new DateTime(2010, 5, 31))
            {
                Reminders =
                {
                    { new DateTime(2010, 08, 12), "childs's birthday" },
                    { new DateTime(1012, 11, 15), "anniversary" }
                }
            };

            SampleOrder o = new SampleOrder(new DateTime(2012, 6, 1), 5m);
            c.AddOrder(o);

            o = new SampleOrder(new DateTime(2103, 7, 4), 25m);
            c.AddOrder(o);

            // Check the discount:
            ICustomer theCustomer = c;
            Console.WriteLine($"Current discount: {theCustomer.ComputeLoyaltyDiscount()}");
        }

        #endregion

        #region 在更多位置中使用更多模式
        public struct RGBColor
        {
            public Int32 r, g, b;

            public RGBColor(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }


        public enum Rainbow
        {
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Indigo,
            Violet
        }

        /// <summary>
        /// switch 表达式
        /// 将其与使用经典 switch 语句的等效代码  书写更方便
        /// </summary>
        /// <param name="colorBand"></param>
        /// <returns></returns>
        public static RGBColor FromRainbow(Rainbow colorBand) =>
    colorBand switch
    {
        Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
        Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
        Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
        Rainbow.Green => new RGBColor(0x00, 0xFF, 0x00),
        Rainbow.Blue => new RGBColor(0x00, 0x00, 0xFF),
        Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
        Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
        _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
    };


        public class Address
        {
            public string State;
            public string Path;
        }

        /// <summary>
        /// 属性模式
        /// </summary>
        /// <param name="location"></param>
        /// <param name="salePrice"></param>
        /// <returns></returns>
        public static decimal ComputeSalesTax(Address location, decimal salePrice) =>
        location switch
        {
            { State: "WA" } => salePrice * 0.06M,
            { State: "MN" } => salePrice * 0.075M,
            { State: "MI" } => salePrice * 0.05M,
            // other cases removed for brevity...
            _ => 0M
        };


        /// <summary>
        /// 元组模式
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string RockPaperScissors(string first, string second)
    => (first, second) switch
    {
        ("rock", "paper") => "rock is covered by paper. Paper wins.",
        ("rock", "scissors") => "rock breaks scissors. Rock wins.",
        ("paper", "rock") => "paper covers rock. Paper wins.",
        ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
        ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
        ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
        (_, _) => "tie"
    };


        public class NewPoint
        {
            public int X { get; }
            public int Y { get; }

            public NewPoint(int x, int y) => (X, Y) = (x, y);

            public void Deconstruct(out int x, out int y) =>
                (x, y) = (X, Y);
        }

        public enum Quadrant
        {
            Unknown,
            Origin,
            One,
            Two,
            Three,
            Four,
            OnBorder
        }

        /// <summary>
        /// 位置模式
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Quadrant GetQuadrant(NewPoint point) => point switch
        {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            var (_, _) => Quadrant.OnBorder,
            _ => Quadrant.Unknown
        };

        #endregion


        /// <summary>
        /// using 声明
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static int WriteLinesToFile(IEnumerable<string> lines)
        {
            using var file = new System.IO.StreamWriter("WriteLines2.txt");
            // Notice how we declare skippedLines after the using statement.
            int skippedLines = 0;
            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    file.WriteLine(line);
                }
                else
                {
                    skippedLines++;
                }
            }
            // Notice how skippedLines is in scope here.
            return skippedLines;
            // file is disposed here
        }

        /// <summary>
        /// 静态本地函数
        /// </summary>
        /// <returns></returns>
        int M()
        {
            int y = 5;
            int x = 7;
            return Add(x, y);

            static int Add(int left, int right) => left + right;
        }


        public struct DoDispose : IDisposable
        {
            public int x;

            public void Dispose()
            {
                x = 10086;
            }
        }

        public static DoDispose zzzz = new DoDispose();

        /// <summary>
        /// 可处置的 ref 结构
        /// 用 ref 修饰符声明的 struct 可实现任何接口
        /// </summary>
        public void Test03()
        {
            ref DoDispose dd = ref zzzz;
            using (zzzz)
            {

            }
        }

        /// <summary>
        /// 可为空引用类型
        /// </summary>
        public void Test04()
        {
            string? ss = null;
            Console.WriteLine(ss!.Length);
        }



        public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        /// <summary>
        /// 异步流
        /// </summary>
        /// <returns></returns>
        public async Task Test05()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// 异步可释放
        /// Task 继承了 System.IAsyncDisposable 或 IDisposable 可以释放
        /// </summary>
        /// <returns></returns>
        public async Task Test06()
        {
            using (Task.Run(() => { Console.WriteLine(4); }))
            {

            }
        }
    }
}
