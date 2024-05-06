using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace ProcessMonitor
{
    internal class Program
    {
        private static ProcessMonitorTimer processMonitorTimer;
        static void Main(string[] args)
        {
            string processName = "notepad";// args[1];
            int maximumTTLSeconds = 200;// int.Parse(args[2]);
            int milliSecondsInterval = 60000;// Convert.ToInt32(args[3]);

            SetProcessMonitorTimer( milliSecondsInterval, processName, maximumTTLSeconds);

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);

            while (true)
            {
                System.Threading.Thread.Sleep(10000);
            }
            
        }

        private static void SetProcessMonitorTimer(int milliSecondsInterval, string processName, int maximumTTLSeconds)
        {
            processMonitorTimer = new ProcessMonitorTimer(milliSecondsInterval,processName,maximumTTLSeconds);
            processMonitorTimer.Elapsed += CheckProcessesState;
            processMonitorTimer.AutoReset = true;
            processMonitorTimer.Enabled = true;
        }

        private static void CheckProcessesState(Object source, ElapsedEventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName(processMonitorTimer.ProcessName);

            for (int i = 0; i < processesByName.Length; i++)
            {
                TimeSpan duration = DateTime.Now - processesByName[i].StartTime;
                if (duration > processMonitorTimer.MaximumTTLSeconds)
                {
                    Console.WriteLine("Process ID {0} name {1} killed, TTL {2}", processesByName[i].Id, processesByName[i].ProcessName,duration);
                    processesByName[i].Kill();
                }
                else
                {
                    Console.WriteLine("Process ID {0} name {1} allowed to continue, TTL {2}", processesByName[i].Id, processesByName[i].ProcessName,duration);
                }

            }


            Console.WriteLine("The process check occured at {0},{1} processes found with name {2}", e.SignalTime,processesByName.Length, processMonitorTimer.ProcessName);

            CheckKeyboardForQuitKey(Key.Q);
        }
        private static void CheckKeyboardForQuitKey(Key quitKey)
        {
            if (Keyboard.IsKeyDown(quitKey))
            {
                processMonitorTimer.Enabled = false;
                processMonitorTimer.Stop();
                processMonitorTimer.Dispose();
                Console.WriteLine("Quitting at {0}", DateTime.Now);
            }
        }

    }
}
