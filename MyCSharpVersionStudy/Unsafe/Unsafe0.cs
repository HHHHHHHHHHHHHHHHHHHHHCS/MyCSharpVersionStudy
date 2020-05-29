using System;
using System.Collections.Generic;
using System.Text;

namespace MyCSharpVersionStudy.Unsafe
{
    public class Unsafe0
    {
        //*                         执行指针间接寻址。
        //->                        通过指针访问结构的成员。
        //[]                        为指针建立索引。
        //& 	                    获取变量的地址。
        //++ 和 -- 	                递增和递减指针。
        //+ 和 - 	                执行指针算法。
        //==、!=、<、>、<= 和 >= 	比较指针。
        //stackalloc                在堆栈上分配内存。
        //fixed 语句                临时固定变量以便找到其地址。

        public void Test00()
        {
            Add(5);
        }

        public void Add(int v)
        {
            unsafe
            {
                int* p = &v;

                *p = *p + 4;
                Console.WriteLine(*p);
            }
        }

        public void Test01()
        {
            DoArray();
        }

        public unsafe void DoArray()
        {
            int[] a = new int[] { 10, 20, 30, 40, 50 };

            //fixed 临时固定变量以便找到其地址
            fixed (int* p = &a[0])
            {
                int* p2 = p;
                Console.WriteLine(*p2);

                p2 += 1;
                Console.WriteLine(*p2);
                p2 += 1;
                Console.WriteLine(*p2);
                Console.WriteLine("----------");
                Console.WriteLine(*p);
                *p += 1;
                Console.WriteLine(*p);
                *p += 1;
                Console.WriteLine(*p);
            }

            Console.WriteLine("-------------");
            Console.WriteLine(a[0]);
        }
    }
}
