using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCSharpVersionStudy.MyThread
{
    public class MyThread4
    {
        public class Monster
        {
            public int Blood { get; set; }
            public Monster(int blood)
            {
                this.Blood = blood;
                Console.WriteLine("我是怪物,我有{0}滴血", blood);
            }
        }

        public class Play
        {
            /// <summary>
            /// 攻击者名字
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 攻击力
            /// </summary>
            public int Power { get; set; }
            /// <summary>
            /// 法术攻击
            /// </summary>
            public void MagicExecute(object monster)
            {
                Monster m = monster as Monster;
                Monitor.Enter(monster);
                while (m.Blood > 0)
                {
                    Monitor.Wait(monster);
                    Console.WriteLine("当前英雄:{0},正在使用法术攻击打击怪物", this.Name);
                    if (m.Blood >= Power)
                    {
                        m.Blood -= Power;
                    }
                    else
                    {
                        m.Blood = 0;
                    }
                    Thread.Sleep(300);
                    Console.WriteLine("怪物的血量还剩下{0}", m.Blood);
                    //只有锁的当前所有者可以使用 Pulse 向等待对象发出信号
                    Monitor.PulseAll(monster);
                }
                Monitor.Exit(monster);
            }
            /// <summary>
            /// 物理攻击
            /// </summary>
            /// <param name="monster"></param>
            public void PhysicsExecute(object monster)
            {
                Monster m = monster as Monster;
                Monitor.Enter(monster);
                while (m.Blood > 0)
                {
                    //释放脉冲 给了法术
                    Monitor.PulseAll(monster);
                    //1S之内没有再次获取到锁自动添加到就绪队列
                    //等待法术释放信号
                    if (Monitor.Wait(monster, 1000))     //非常关键的一句代码
                    {
                        Console.WriteLine("当前英雄:{0},正在使用物理攻击打击怪物", this.Name);
                        if (m.Blood >= Power)
                        {
                            m.Blood -= Power;
                        }
                        else
                        {
                            m.Blood = 0;
                        }
                        Thread.Sleep(300);
                        Console.WriteLine("怪物的血量还剩下{0}", m.Blood);
                    }
                }
                Monitor.Exit(monster);
            }
        }

        public void Test00()
        {
            //怪物类
            Monster monster = new Monster(1000);
            //物理攻击类
            Play play1 = new Play() { Name = "无敌剑圣", Power = 100 };
            //魔法攻击类
            Play play2 = new Play() { Name = "流浪法师", Power = 120 };
            Thread thread_first = new Thread(play1.PhysicsExecute);    //物理攻击线程
            Thread thread_second = new Thread(play2.MagicExecute);     //魔法攻击线程
            thread_first.Start(monster);
            thread_second.Start(monster);
            Console.ReadKey();

        }
    }
}
