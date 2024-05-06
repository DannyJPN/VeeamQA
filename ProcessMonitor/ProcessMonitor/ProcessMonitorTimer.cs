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
        public TimeSpan MaximumTTLSeconds { get; set; }

        public ProcessMonitorTimer(int milliSecondsInterval) : base(milliSecondsInterval)
        {

        }
        public ProcessMonitorTimer(int milliSecondsInterval,string processName,int maximumTTLSeconds):this(milliSecondsInterval)
        {
            ProcessName = processName;
            MaximumTTLSeconds = new TimeSpan(0,0,maximumTTLSeconds);
        }

    }
}
