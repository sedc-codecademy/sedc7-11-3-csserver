using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsyncDemo
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("------Using Task.Run------");
            Console.WriteLine($"Running on thread {Thread.CurrentThread.ManagedThreadId}");
            var result = Task.Run(() => AddDelayed(1, 2));
            Console.WriteLine($"Result {result.Result} on thread {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("------Using async and Delay------");
            Console.WriteLine($"Running on thread {Thread.CurrentThread.ManagedThreadId}");
            var resultAsync = AddDelayedAsync(1, 2);
            Console.WriteLine($"Result {resultAsync.Result} on thread {Thread.CurrentThread.ManagedThreadId}");

            //Console.WriteLine("------Looping------");
            //var x = Task.Run(() => LoopSync());

            Console.WriteLine("------Looping 2------");
            var x = LoopAsync();

            Console.WriteLine(x.Result);
            

        }

        private static int LoopSync()
        {
            for (int i = 0; i < 20; i++)
            {
                var result = Task.Run(() => AddDelayed(1, 2));
                Console.WriteLine($"Run #{i} on thread {Thread.CurrentThread.ManagedThreadId}");
            }
            return -1;
        }

        private static async Task<int> LoopAsync()
        {
            for (int i = 0; i < 20; i++)
            {
                var result = await AddDelayedAsync(1, 2);
                Console.WriteLine($"Result {result} on run #{i} on thread {Thread.CurrentThread.ManagedThreadId}");
            }
            return -1;
        }


        static int AddDelayed(int first, int second)
        {
            Console.WriteLine($"Sleeping on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(random.Next(1000));
            return first + second;
        }

        static async Task<int> AddDelayedAsync(int first, int second)
        {
            //return Task.Run(() => AddDelayed(first, second));
            Console.WriteLine($"Sleeping on thread {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(random.Next(1000));
            return first + second;
        }


    }


}
