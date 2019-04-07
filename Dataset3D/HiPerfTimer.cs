using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Dataset3D
{
    public class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long startTime, stopTime;
        private long freq;

        // Constructor
        public HiPerfTimer()
        {
            startTime = 0; stopTime = 0;
            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported
                throw new System.ComponentModel.Win32Exception();
            }
        }

        // Start the timer
        public void Start()
        {
            // lets do the waiting threads their work
            Thread.Sleep(0);
            QueryPerformanceCounter(out startTime);
        }

        // Stop the timer
        public float Stop()
        {
            QueryPerformanceCounter(out stopTime);
            return (float)Duration;
        }

        // Returns the duration of the timer (in seconds)
        public double Duration
        { get { return (double)(stopTime - startTime) / (double)freq; } }
    }
}
