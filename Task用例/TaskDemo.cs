using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyClassLibrary.Task用例
{
    class TaskDemo
    {
        static void Main(string[] args)
        {
            // 建立一个调度器数组，用于控制 Task 线程

            CancellationTokenSource[] cts = new CancellationTokenSource[]
            {
                new CancellationTokenSource(),

                new CancellationTokenSource(),

                new CancellationTokenSource()
            };

            // 创建一个 Task 工厂对象

            TaskFactory taskFactory = new TaskFactory();

            // 创建 Task 线程数组，数组大小为线程个数

            Task<Int32>[] tasks = new Task<Int32>[]
            {
                taskFactory.StartNew(() => Add(cts[0].Token)),

                taskFactory.StartNew(() => Add(cts[1].Token)),

                taskFactory.StartNew(() => Add(cts[2].Token))
            };

            // CancellationToken.None 指示 TasksEnded 不能被取消

            taskFactory.ContinueWhenAll(tasks, TasksEnded, CancellationToken.None);

            // 等待按下任意一个键取消下一个线程的任务

            Console.ReadKey();

            cts[0].Cancel();

            Console.ReadKey();

            cts[1].Cancel();

            Console.ReadKey();

            cts[2].Cancel();

            Console.ReadKey();

            for (int i = 0; i < tasks.Length; i++)
            {
                // 打印线程任务得出的数值

                Console.WriteLine(tasks[i].Result);
            }

            Console.ReadKey();

        }

        /// <summary>
        /// 添加新任务
        /// </summary>
        /// <param name="ct"> 线程控制器对象 </param>
        /// <returns>线程任务返回的数据</returns>
        static Int32 Add(CancellationToken ct)
        {
            Console.WriteLine("任务开始……");

            int result = 0;

            while (!ct.IsCancellationRequested)
            {
                result++;

                Thread.Sleep(1000);
            }
            return result;
        }

        /// <summary>
        /// 线程都结束后执行的方法
        /// </summary>
        /// <param name="tasks">线程数组</param>
        static void TasksEnded(Task<Int32>[] tasks)
        {
            Console.WriteLine("所有任务已完成！");
        }
    }
}
