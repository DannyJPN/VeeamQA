using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProcessMonitor
{
    internal class Program
    {
        private static ProcessMonitorTimer processMonitorTimer;
        static void Main(string[] args)
        {
            string processName = args[1];
            int maximumTTL = int.Parse(args[2]);
            int secondsInterval = Convert.ToInt32(args[3]);

            SetProcessMonitorTimer( secondsInterval, processName, maximumTTL);

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            processMonitorTimer.Stop();
            processMonitorTimer.Dispose();

            Console.WriteLine("Terminating the application...");
        }

        private static void SetProcessMonitorTimer(int secondsInterval, string processName, int maximumTTL)
        {
            processMonitorTimer = new ProcessMonitorTimer(2000,processName,maximumTTL);
            processMonitorTimer.Elapsed += OnTimedEvent;
            processMonitorTimer.AutoReset = true;
            processMonitorTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }

    }
}
