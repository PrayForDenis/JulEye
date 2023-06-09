using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Helpers
{
    internal static class CpuCounter
    {
        private static List<double> _cpuCounterList = new List<double>();
        private static PerformanceCounter _cpuCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);

        public static IReadOnlyList<double> CpuCounterList => _cpuCounterList;

        public static void AddCpuTick()
        {
            double cpu = _cpuCounter.NextValue();
            _cpuCounterList.Add(cpu);
        }
    }
}
