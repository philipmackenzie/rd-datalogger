##Simple logging and plotting of data for debugging/profiling timing in Unity apps.##

You can use this as a starting point for getting more diagnostics out of your app, but for basic timing I'd still stuggest using the [Unity built-in profiler](https://docs.unity3d.com/ScriptReference/Profiling.Profiler.BeginSample.html).

###Python dependencies:###
pip install matplotlib

###Run it:###
cd rd-datalogger\python
python plotExample.py ..\DataLoggerExample

You should get a graph like this:

![Graph example](https://bitbucket.org/philipmac/rd-datalogger/raw/7fcfe1a0c27c0021cc83ba16c3961cefaa3d5bf3/example.jpg)


