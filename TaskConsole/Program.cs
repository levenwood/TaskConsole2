using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace TaskConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
              //Task实例可以用各种不同方式创建。最常见的方法是使用任务类型的Factory属性检索可以创建用于多个用途的任务的TaskFactory实例。例如，要创建运行操作的Task，可以使用工厂的StartNew方法：
              //var t = Task.Factory.StartNew(()=>DoAction());
                Action<object> action = (object obj) =>
                    {
                        Console.WriteLine("Task={0},obj={1},Thread={2}", Task.CurrentId, obj.ToString(), Thread.CurrentThread.ManagedThreadId);
                    };
                Task t1 = new Task(action, "alpha");
                Task t2 = Task.Factory.StartNew(action, "beta");
                t2.Wait();
                t1.Start();

                Console.WriteLine("t1 has been launched.(Main Thread={0})",Thread.CurrentThread.ManagedThreadId);
                t1.Wait();
                Task t3 = new Task(action, "gamma");

                t3.RunSynchronously();
                t3.Wait();
            */
            //从任务中返回值
            Task<int> task1 = Task<int>.Factory.StartNew(()=>1);
            int i = task1.Result;

            Task<Test> task2 = Task<Test>.Factory.StartNew(() => {
                string s = ".net";
                double d = 4.0;
                return new Test { Name = s, Number = d };
            });
            //D:\

            Task<string[]> task3 = Task<string[]>.Factory.StartNew(() => {
                string path = @"D:\";
                string[] files = System.IO.Directory.GetFiles(path);
                var result = (from file in files.AsParallel()
                             let info = new System.IO.FileInfo(file)
                             select file).ToArray();
                return result;
            });
            foreach (var str in task3.Result)
            {
                Console.WriteLine(str);
            }
           Console.ReadLine();
        }
    }
    class Test
    {
        public string Name { get; set; }
        public double Number { get; set; }
    }
}
