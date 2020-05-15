using System;
using System.Collections.Generic;
using System.Text;

namespace MyCSharpVersionStudy._72
{
    public class Main72
    {
        public class Temp
        {
            public int x;
        }

        public struct MyStruct
        {
            public int x;
        }

        public void CantChange(in int t)
        {
            //t = 2;
            //in 不让修改
            Console.WriteLine(t);

            //不让传入
            //foo(ref t);
        }

        public int zz = 5;

        /// <summary>
        /// in 指定形参通过引用传递，但不通过调用方法修改
        /// </summary>
        public void Test01()
        {
            CantChange(in zz);
        }


        public readonly MyStruct temp = new MyStruct();

        public ref readonly MyStruct RefReadonly()
        {
            return ref temp;
        }

        /// <summary>
        /// ref readonly 通过引用返回其值，但不允许写入该对象
        /// </summary>
        public void Test02()
        {
            MyStruct tt = RefReadonly();
        }


        public readonly struct ReadonlyStruct
        {
            public readonly int x, y, z;

            public ReadonlyStruct(int _x, int _y, int _z)
            {
                x = _x;
                y = _y;
                z = _z;
            }
        }

        /// <summary>
        /// Readonly Struct 不能修改的struct
        /// </summary>
        public void Test03()
        {
            ReadonlyStruct st = new ReadonlyStruct();
            //不能修改
            //st.x = 5;
        }


        void WriteText(string text, bool bold = false, bool centered = false)
        {
        }

        /// <summary>
        /// 非尾随命名参数
        /// </summary>
        public void Test04()
        {
            WriteText("Hello world", centered: true);
        }

        /// <summary>
        /// 数值文字中的前导下划线
        /// C# 7.0 中实现了对数字分隔符的支持，但这不允许文字值的第一个字符是 _。 
        /// 十六进制文本和二进制文件现可以 _ 开头
        /// </summary>
        int binaryValue = 0b_0101_0101;

        /// <summary>
        /// 新的复合访问修饰符：private protected 指示可通过包含同一程序集中声明的类或派生类来访问成员。 
        /// 虽然 protected internal 允许通过同一程序集中的类或派生类进行访问，
        /// 但 private protected 限制对同一程序集中声明的派生类的访问。
        /// </summary>
        private protected void Test05()
        {

        }

        /// <summary>
        /// 条件 ref 表达式
        /// 返回的是值引用
        /// </summary>
        public void Test06()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] otherArr = new int[] { 34, 6678, 5 };
            ref var r = ref (arr != null ? ref arr[0] : ref otherArr[0]);
        }
    }
}
