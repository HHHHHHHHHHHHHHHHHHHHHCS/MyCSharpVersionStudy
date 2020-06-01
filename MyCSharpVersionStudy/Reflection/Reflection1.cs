using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MyCSharpVersionStudy.Reflection
{
    public class Reflection1
    {
        public class BaseClass
        {
            public int baseField = 0;
        }

        public class DerivedClass : BaseClass
        {
            public int derivedField = 0;
        }

        public void Test00()
        {
            var base1 = typeof(System.String).BaseType;
            var base2 = typeof(System.IO.FileStream).BaseType;
            var base3 = typeof(DerivedClass).BaseType;

            Console.WriteLine($"base1 :{base1.Name}");
            Console.WriteLine($"base2 :{base2.Name}");
            Console.WriteLine($"base3 :{base3.Name}");

            foreach (var iType in typeof(Guid).GetInterfaces())
            {
                Console.WriteLine($"iType :{iType.Name}");
            }

        }

        public void Test01()
        {
            var base3 = typeof(DerivedClass).BaseType;


            //Type中的方法 IsInstanceOfType 其实和is是等价的。
            var baseClassObject = new BaseClass();
            var check1 = baseClassObject is BaseClass;
            var check2 = base3.IsInstanceOfType(baseClassObject);
            Console.WriteLine($"使用is判断类型是否相同 :{check1}");  //结果True
            Console.WriteLine($"使用IsInstanceOfType类型是否相同 :{check2 }"); //结果True　
        }

        public void Test02()
        {
            //还有一个是 IsAssignableFrom ，它的作用是确定指定类型的实例是否可以分配给当前类型的实例。
            var base4 = typeof(BaseClass); //baseClass的实例
            var baseClassObject = new BaseClass();
            var derivedClassObject = new DerivedClass();
            var classObject = new object();
            var checkResult1 = base4.IsAssignableFrom(baseClassObject.GetType()); //判断BaseClass类型是否可以分配给BassClass类型
            var checkResult2 = base4.IsAssignableFrom(derivedClassObject.GetType());  //判断DerivedClass类型是否可以分配给BassClass类型
            var checkResult3 = base4.IsAssignableFrom(classObject.GetType()); //判断Class类型是否可以分配给BassClass类型
            Console.WriteLine($"使用IsAssignableFrom类型是否和接受的类型一致 :{checkResult1}");   //True
            Console.WriteLine($"使用IsAssignableFrom类型是否和接受的类型一致 :{checkResult2}");   //True
            Console.WriteLine($"使用IsAssignableFrom类型是否和接受的类型一致 :{checkResult3}");  //False

        }

        public void Test03()
        {
            //当不存在这个构造函数的时候，就会抛出错误。
            var dateTime1 = (DateTime)Activator.CreateInstance(typeof(DateTime), 2019, 6, 19);
            var dateTime2 = (DateTime)Activator.CreateInstance(typeof(DateTime), 2019, 6, 19, 10, 10, 10);
            Console.WriteLine($"DateTime1: {dateTime1}"); //DateTime1: 2019/6/19 0:00:00
            Console.WriteLine($"DateTime2: {dateTime2}"); //DateTime2: 2019/6/19 10:10:10
        }


        public class InvokeClass
        {
            private string _testString;
            private long _testInt;

            public InvokeClass(string abc)
            {
                _testString = abc;
            }
            public InvokeClass(StringBuilder abc)
            {
                _testString = abc.ToString();
            }

            public InvokeClass(string abc, long def)
            {
                _testString = abc;
                _testInt = def;
            }
        }

        public void Test04()
        {
            //二义性
            //var newObj = new InvokeClass(null);
            //不过可以这样
            //var newObj = new InvokeClass((string)null);
            //找不到对应的构造函数
            //var obj2 = (InvokeClass)Activator.CreateInstance(typeof(InvokeClass), null);


            //找到一个参数为string的构造函数
            ConstructorInfo constructorInfo = typeof(InvokeClass).GetConstructor(new[] { typeof(string) });
            //使用该构造函数传入一个null参数     
            var obj4 = (InvokeClass)constructorInfo.Invoke(new object[] { null });

            Console.WriteLine("=======================");

            //获取所有的构造函数
            var constructorInfoArray = typeof(InvokeClass).GetConstructors();
            //过滤一次，获取所有两个参数的构造函数
            var constructorInfoArray2 = Array.FindAll(constructorInfoArray, x => x.GetParameters().Length == 2);
            //最后找的第二个参数是long类型的构造函数
            var constructorInfo2 = Array.Find(constructorInfoArray2, x => x.GetParameters()[1].ParameterType == typeof(long));
            //如果存在，就创建对象
            if (constructorInfo2 != null)
            {
                var obj5 = (InvokeClass)constructorInfo2.Invoke(new object[] { "abc", 123 });
            }
        }

        //动态构造对象的缺点就是慢，
        //简单对比一下，采用反射和new创建100万个对象，耗时对比还是比较明显的。
        public void Test05()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100000; i++)
            {
                var obj3 = (InvokeClass)Activator.CreateInstance(typeof(InvokeClass), "abc", 123);
            }
            sw.Stop();
            Console.WriteLine($"时间：{sw.ElapsedMilliseconds}ms");

            var sw2 = new Stopwatch();
            sw2.Start();
            for (int i = 0; i < 100000; i++)
            {
                var obj = new InvokeClass("abc", 123);

            }
            sw2.Stop();
            Console.WriteLine($"时间：{sw2.ElapsedMilliseconds}ms");
        }
    }
}
