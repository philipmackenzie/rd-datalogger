using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class PerformanceCounter
{
    [DllImport("KERNEL32")]
    private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceFrequency(out long lpFrequency);

    private long frequency;

    public PerformanceCounter()
    {
        if(!QueryPerformanceFrequency(out frequency))
        {
            frequency = 0;
        }
    }

    public long GetPerformanceCounter()
    {
        long counter;
        if(!QueryPerformanceCounter(out counter))
        {
            counter = 0;
        }
        return counter;
    }

    public double CounterToMillis(long start, long end)
    {
        if(frequency == 0)
        {
            return 0;
        }

        return (end - start) * 1000 / (double)frequency;
    }
}
