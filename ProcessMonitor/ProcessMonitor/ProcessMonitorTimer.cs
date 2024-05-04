using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProcessMonitor
{
    public class ProcessMonitorTimer:Timer
    {
        public string ProcessName { get; set; }
        public int MaximumTTL { get; set; }

        public ProcessMonitorTimer(int secondsInterval) : base(secondsInterval)
        {

        }
        public ProcessMonitorTimer(int secondsInterval,string processName,int maximumTTL):this(secondsInterval)
        {
            ProcessName = processName;
            MaximumTTL = maximumTTL;
        }

    }
}
