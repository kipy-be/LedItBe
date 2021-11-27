using System;
using System.Threading;

namespace LedItBe.CLI
{
    public class Program
    {
        static AutoResetEvent _waiter = new AutoResetEvent(false);  

        public static void Main(string[] args)
        {
            Console.CancelKeyPress += OnCancelKeyPress;
            _waiter.WaitOne();

            Console.WriteLine("Bye !");
        }

        private static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _waiter.Set();
        }
    }
}