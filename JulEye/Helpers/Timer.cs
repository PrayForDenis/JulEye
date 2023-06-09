using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Helpers
{
    internal class Timer
    {
        private float _elapsedTime;
        private Stopwatch _stopwatch;

        public float ElapsedTime => _elapsedTime;

        public Timer()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start() 
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            CalculateResult();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        private void CalculateResult()
        {
            TimeSpan elapsedTime = _stopwatch.Elapsed;
            _elapsedTime = (float) elapsedTime.TotalSeconds;
        }
    }
}
