using System;
using System.Collections.Generic;
using System.Text;

namespace MyCSharpVersionStudy.Reflection
{
    public class Reflection2
    {
        public static int StaticSum(int a, int b)
        {
            return a + b;
        }

        public int InstanceSum(int a, int b)
        {
            return a + b;
        }

        //创建一个委托
        delegate int delegateOperate(int a, int b);

        public void Test00()
        {
            //静态方法的委托
            Delegate staticD = Delegate.CreateDelegate(typeof(delegateOperate), typeof(Reflection2), "StaticSum");
            //实例方法的委托
            Delegate instanceD = Delegate.CreateDelegate(typeof(delegateOperate), new Reflection2(), "InstanceSum");

            Console.WriteLine($"staticD：{staticD.DynamicInvoke(1, 2)}");
            Console.WriteLine($"instanceD：{instanceD.DynamicInvoke(10, 20)}");
            Console.ReadKey();
        }

        public void Test01()
        {

            Type closed = typeof(List<int>);
            Type unBound = typeof(List<>);

            //转换
            var newClosed = unBound.MakeGenericType(typeof(int));
            var newUnBound = closed.GetGenericTypeDefinition();

            Console.WriteLine($"List<int> 类型{closed}");
            Console.WriteLine($"List<> 类型{unBound}");
            Console.WriteLine($"List<> MakeGenericType执行后 类型{newClosed}");
            Console.WriteLine($"List<int> GetGenericTypeDefinition执行后 类型{newUnBound}");
        }
    }
}
