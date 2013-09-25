using System;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;

namespace MyFirstTest
{
    class ThreadStudy
    {
        [Test, Ignore]
        public void task()
        {
            const int repetitions = 1000;
            Task task = new Task(() =>
            {
                for (int count = 0; count < repetitions; count++)
                {
                    Console.Write('-');
                }
            });
            task.Start();
            for (int count = 0; count < repetitions; count++)
            {
                Console.Write('.');
            }
            // Wait until the Task completes
            task.Wait();
        }

        [Test]
        public void sleep1()
        {
            Thread.Sleep(2000);
            Console.Beep();
        }

        [Test]
        public void sleep2()
        {
            ManualResetEvent dummy = new ManualResetEvent(false);
            dummy.WaitOne(2000);
            Console.Beep();
        }
    }
}
