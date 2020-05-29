using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyCSharpVersionStudy.Reflection
{
    //https://www.cnblogs.com/dcz2015/p/11058193.html
    public class Reflection0
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
            var bc = new BaseClass();
            var dc = new DerivedClass();
            BaseClass[] bca = new BaseClass[] { bc, dc };
            foreach (var v in bca)
            {
                Type t = v.GetType();
                Console.WriteLine("Object Type:{0}", t.Name);
                Console.WriteLine("Full Name:{0}", t.FullName);

                FieldInfo[] fis = t.GetFields();
                foreach (var item in fis)
                {
                    Console.WriteLine("     Field:{0}", item.Name);
                }
                Console.WriteLine("End");
            }

            Console.WriteLine("----------------");
        }

        public void Test01()
        {
            var bc = typeof(BaseClass);
            var dc = typeof(DerivedClass);
            Type[] types = new[] { bc, dc };
            foreach (var t in types)
            {
                Console.WriteLine("Object Type:{0}", t.Name);
                Console.WriteLine("Full Name:{0}", t.FullName);

                FieldInfo[] fis = t.GetFields();
                foreach (var item in fis)
                {
                    Console.WriteLine("     Field:{0}", item.Name);
                }
                Console.WriteLine("End");
            }

            Console.WriteLine("----------------");
        }

        public void Test02()
        {
            Type t = typeof(DerivedClass);
            var theAssembly = Assembly.GetExecutingAssembly();
            foreach (var item in theAssembly.GetTypes())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("========================");

            var baseType = Assembly.GetExecutingAssembly().GetType("MyCSharpVersionStudy.Reflection.Reflection0+BaseClass");
            Console.WriteLine(baseType.Name);
            var derivedType = Assembly.GetExecutingAssembly().GetType("MyCSharpVersionStudy.Reflection.Reflection0+DerivedClass");
            Console.WriteLine(derivedType.Name);
        }


        public void Test03()
        {
            var intArray = typeof(int).MakeArrayType();
            var int3Array = typeof(int).MakeArrayType(3);

            Console.WriteLine($"是否是int 数组 intArray == typeof(int[]) ：{intArray == typeof(int[]) }");
            Console.WriteLine($"是否是int 3维数组 intArray3 == typeof(int[]) ：{int3Array == typeof(int[]) }");
            Console.WriteLine($"是否是int 3维数组 intArray3 == typeof(int[,,])：{int3Array == typeof(int[,,]) }");

            //数组元素的类型
            Type elementType = intArray.GetElementType();
            Type elementType2 = int3Array.GetElementType();

            Console.WriteLine($"{intArray}类型元素类型：{elementType }");
            Console.WriteLine($"{int3Array}类型元素类型：{elementType2 }");


            //获取数组的维数
            var rank = int3Array.GetArrayRank();
            Console.WriteLine($"{int3Array}类型维数：{rank }");
        }


        public void Test04()
        {
            var classType = typeof(Reflection0);

            foreach (var t in classType.GetNestedTypes())
            {
                Console.WriteLine($"NestedType = {t}");
                //是否是public  且不为嵌套
                Console.WriteLine($"{t} 修饰符 {t.IsPublic}");
                //是否是嵌套 并且是 public
                Console.WriteLine($"{t}访问, {t.IsNestedPublic}");
            }
        }

        private static void PrintTypeName(Type t)
        {
            Console.WriteLine($"NameSpace: {t.Namespace}");
            Console.WriteLine($"Name :{t.Name}");
            Console.WriteLine($"FullName: {t.FullName}");
        }

        public void Test05()
        {

            #region 获取名称
            var type = typeof(Reflection0);
            Console.WriteLine($"\n------------一般类型-------------");
            PrintTypeName(type);

            //嵌套类型
            Console.WriteLine($"\n------------嵌套类型-------------");
            foreach (var t in type.GetNestedTypes())
            {
                PrintTypeName(t);
            }

            var type2 = typeof(Dictionary<,>);            //非封闭式泛型
            var type3 = typeof(Dictionary<string, int>);  //封闭式泛型

            Console.WriteLine($"\n------------非封闭式泛型-------------");
            PrintTypeName(type2);
            Console.WriteLine($"\n------------封闭式泛型-------------");
            PrintTypeName(type3);
            Console.ReadKey();
            #endregion
        }
    }
}
