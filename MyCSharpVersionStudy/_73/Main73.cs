using System;
using System.Collections.Generic;
using System.Text;

namespace MyCSharpVersionStudy._73
{
    public class Main73
    {
        //启用更高效的安全代码

        unsafe public struct MyStruct
        {
            public fixed int myFixedField[10];
            public int[] myOldFixedField;
        }


        /// <summary>
        /// 索引 fixed 字段不需要进行固
        /// </summary>
        unsafe public void Test01()
        {
            MyStruct myStruct = new MyStruct();
            //新的固定
            //int p = myStruct.myFixedField[5];

            //老的固定
            fixed (int* ptr = myStruct.myOldFixedField)
            {
                int p = ptr[5];
            }
        }


        /// <summary>
        /// 可能会重新分配 ref 局部变量
        /// </summary>
        public void Test02()
        {
            MyStruct m1 = new MyStruct();
            MyStruct m2 = new MyStruct();


            ref MyStruct refLocal = ref m1; // initialization
            refLocal = ref m2; // reassigned, refLocal refers to different storage.
        }


        /// <summary>
        /// stackalloc 数组支持初始值设定项
        /// </summary>
        unsafe public void Test03()
        {
            //老的
            var arr = new int[3] { 1, 2, 3 };
            var arr2 = new int[3] { 1, 2, 3 };

            //unsafe 的
            int* pArr = stackalloc int[3] { 1, 2, 3 };
            int* pArr2 = stackalloc int[3] { 1, 2, 3 };
            Span<int> spanArr = stackalloc int[3] { 1, 2, 3 };
        }

        /// <summary>
        /// 更多类型支持 fixed 语句
        /// 比如GetPinnableReference
        /// </summary>
        unsafe public void Test04()
        {
            int[] PascalsTriangle = {
                  1,
                1,  1,
              1,  2,  1,
            1,  3,  3,  1,
          1,  4,  6,  4,  1,
        1,  5,  10, 10, 5,  1
    };

            Span<int> RowFive = new Span<int>(PascalsTriangle, 10, 5);
            ref var xx = ref RowFive.GetPinnableReference();
        }

        /// <summary>
        /// 增强的泛型约束
        /// 支持枚举 事件
        /// 和 不可空的
        /// </summary>
        public void Test05<T0, T1, T2>(T0 t0, T1 t1, T2 t2)
            where T0 : Enum where T1 : Delegate where T2 : unmanaged
        {

        }

        /// <summary>
        /// 元组支持 == 和 !=
        /// </summary>
        public void Test06()
        {
            (int, int) xx = (1, 2);
            (int, int) yy = (1, 2);
            (int, int) zz = (1, 3);

            if (xx == yy)
            {

            }

            if (xx != zz)
            {

            }
        }

        /// <summary>
        /// 将特性添加到自动实现的属性的支持字段
        /// </summary>
        //[field: SomeThingAboutFieldAttribute]
        public int SomeProperty { get; set; }

        public MyStruct myS = new MyStruct();

        public void Test07(MyStruct arg)
        {
            Test07(myS);
        }

        /// <summary>
        /// in 方法重载解析决胜属性
        /// </summary>
        /// <param name="arg"></param>
        public void Test07(in MyStruct arg)
        {
            Test07(in myS);
        }


        public class B
        {
            public B(int i, out int j)
            {
                j = i;
            }
        }

        /// <summary>
        /// 扩展初始值设定项中的表达式变量
        /// </summary>
        public class D : B
        {
            public D(int i) : base(i, out var j)
            {
                Console.WriteLine($"The value of 'j' is {j}");
            }
        }

        /*
         * 
        改进了重载候选项
            在每个版本中，对重载解析规则进行了更新，以解决多义方法调用具有“明显”选择的情况。 
            此版本添加了三个新规则，以帮助编译器选取明显的选择：

            1.当方法组同时包含实例和静态成员时，如果方法在不含实例接收器或上下文的情况下被调用，
                则编译器将丢弃实例成员。 如果方法在含有实例接收器的情况下被调用，则编译器将丢弃静态成员。 
                在没有接收器时，编译器将仅添加静态上下文中的静态成员，否则，将同时添加静态成员和实例成员。 
                当接收器是不明确的实例或类型时，编译器将同时添加两者。 
                静态上下文（其中隐式 this 实例接收器无法使用）包含未定义 this 的成员的正文（例如，静态成员），
                以及不能使用 this 的位置（例如，字段初始值设定项和构造函数初始值设定项）。
            2.当一个方法组包含类型参数不满足其约束的某些泛型方法时，这些成员将从候选集中移除。
            3.对于方法组转换，返回类型与委托的返回类型不匹配的候选方法将从集中移除。

            你将注意到此更改，因为当你确定哪个方法更好时，你将发现多义方法重载具有更少的编译器错误。
            新的编译器选项

        新的编译器选项支持 C# 程序的新版本和 DevOps 方案。
            公共或开放源代码签名

        -publicsign 编译器选项指示编译器使用公钥对程序集进行签名。 
            程序集被标记为已签名，但签名取自公钥。 
            此选项使你能够使用公钥在开放源代码项目中构建签名的程序集。

            有关详细信息，请参阅 -publicsign 编译器选项一文。

        pathmap
        -pathmap 编译器选项指示编译器将生成环境中的源路径替换为映射的源路径。 
        -pathmap 选项控制由编译器编写入 PDB 文件或为 CallerFilePathAttribute 编写的源路径。
         * 
         */
    }
}
