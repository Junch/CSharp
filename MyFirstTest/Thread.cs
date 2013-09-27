using System;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using System.Diagnostics;

namespace MyFirstTest
{
    class ThreadStudy
    {
        [Test]
        public void task1()
        {
            Task t = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                    Console.Write('*');
            });

            t.Wait();
        }

        [Test]
        public void task2()
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

        void playMacro()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(3000);
                Console.Beep();
                Console.WriteLine(mFilename);
            }
        }

        string mFilename = "cj.txt";

        [Test]
        public void task3()
        {
            Task task = new Task(playMacro);
            task.Start();
            task.Wait();
        }


        ///////////////////////////////////////

        [Test]
        public void sleep1()
        {
            Thread.Sleep(2000);
            Console.Beep();
            Console.WriteLine("print after 2s");
        }

        [Test]
        public void sleep2()
        {
            ManualResetEvent dummy = new ManualResetEvent(false);
            dummy.WaitOne(2000);
            Console.Beep();
            Console.WriteLine("print after 2s");
        }

        ///////////////////////////////////////

        [Test]
        public void sharedData1()
        {
            int n = 0;
            var up = Task.Run(() =>
            {
                 for (int i = 0; i < 1000000; i++)
                    n++;
            });
            for (int i = 0; i < 1000000; i++)
                n--;
            
            up.Wait();
            Console.WriteLine(n); // Now n is random
            Assert.AreEqual(0, n);
        }

        [Test]
        public void sharedData2()
        {
            int n = 0;
            object _lock = new object();
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    lock (_lock)
                        n++;
            });
            for (int i = 0; i < 1000000; i++)
                lock (_lock)
                    n--;
            up.Wait();
            Console.WriteLine(n);
            Assert.AreEqual(0, n);
        }

        [Test]
        public void sharedData3()
        {
            int n = 0;
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    Interlocked.Increment(ref n);
            });
            for (int i = 0; i < 1000000; i++)
                Interlocked.Decrement(ref n);
            up.Wait();
            Console.WriteLine(n);
            Assert.AreEqual(0, n);
        }

        [Test]
        public void cancellation()
        {
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                token.ThrowIfCancellationRequested();
            }, token);

            Thread.Sleep(3000);
            try 
            {
                cancellationTokenSource.Cancel();
                task.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions[0].Message);
            }
        }

        [Test]
        public void stopWatch1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(3000);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        [Test]
        public void stopWatch2()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(1000);
            Console.WriteLine("RunTime " + stopWatch.ElapsedMilliseconds);

            stopWatch.Reset();
            stopWatch.Start();
            Thread.Sleep(1000);
            Console.WriteLine("RunTime " + stopWatch.ElapsedMilliseconds);

            stopWatch.Reset();
            stopWatch.Start();
            Thread.Sleep(1000);
            Console.WriteLine("RunTime " + stopWatch.ElapsedMilliseconds);

            stopWatch.Stop();
        }

        static EventWaitHandle _waitHandle = new AutoResetEvent(false);

        [Test]
        public void autoResetEvent()
        {
            new Thread(waiter).Start();
            Thread.Sleep(1000);
            _waitHandle.Set();
        }

        void waiter()
        {
            Console.WriteLine("Waiting...");
            _waitHandle.WaitOne();
            Console.WriteLine("Notified"); 
        }


    }
}
