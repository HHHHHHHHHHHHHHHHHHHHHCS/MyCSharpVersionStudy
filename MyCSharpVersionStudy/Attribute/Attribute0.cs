#define MyC

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MyCSharpVersionStudy.Attribute
{
    public class Attribute0
    {
        //System.AttributeUsage声明一个Attribute的使用范围与使用原则。
        //参数 validon 规定特性可被放置的语言元素。它是枚举器 AttributeTargets 的值的组合。默认值是 AttributeTargets.All。
        //参数 allowmultiple（可选的）为该特性的 AllowMultiple 属性（property）提供一个布尔值。如果为 true，则该特性是多用的。默认值是 false（单用的）。
        //参数 inherited（可选的）为该特性的 Inherited 属性（property）提供一个布尔值。如果为 true，则该特性可被派生类继承。默认值是 false（不被继承）。
        [AttributeUsage(AttributeTargets.Class |
            AttributeTargets.Constructor |
            AttributeTargets.Field |
            AttributeTargets.Method |
            AttributeTargets.Property,
            AllowMultiple = true)]
        public class AttributeUsage0 : System.Attribute
        {

        }

        /// <summary>
        /// Conditional
        /// 这个预定义特性标记了一个条件方法，其执行依赖于指定的预处理标识符。
        /// 它会引起方法调用的条件编译，取决于指定的值，比如 Debug 或 Trace。例如，当调试代码时显示变量的值。
        /// 是否要 define 他
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("MyC")]
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }


        public void Test00()
        {
            Message("ccccc");
            Console.WriteLine("End");
        }



        /// <summary>
        /// [Obsolete(message)]
        /// [Obsolete(message,iserror)]
        /// 参数 message，是一个字符串，描述项目为什么过时以及该替代使用什么。
        /// 参数 iserror，是一个布尔值。如果该值为 true，编译器应把该项目的使用当作一个错误。默认值是 false（编译器生成一个警告）。
        /// </summary>
        [Obsolete("Don't use OldMethod, use NewMethod instead", false)]
        static void OldMethod()
        {
            Console.WriteLine("It is the old method");
        }

        public void Test01()
        {
            OldMethod();
        }




    }
}
