using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

public class DebugDataLogger
{
    private StreamWriter streamWriter;

    private object handle = new object();

    bool open = false;

    private string GetFileName(string prefix, bool useDate)
    {
        string outputFileName = "diagnostics_" + prefix;
        if (useDate)
        {
            outputFileName += "_" + DateTime.Now.ToString("yyddmm_hhmmss");
        }
        outputFileName += ".csv";

        return outputFileName;
    }

    public DebugDataLogger(string prefix, bool useDate=true)
    {
        string fullPath = GetFileName(prefix, useDate);

        if (File.Exists(fullPath))
        {
            if (useDate)
            {
                Debug.Log(fullPath + " already exists.");
                return;
            }
            else
            {
                File.Delete(fullPath);
            }
        }

        Debug.Log("Writing diagnostics to: " + fullPath);
        Console.WriteLine("Writing data to " + Path.GetFullPath(fullPath));

        streamWriter = File.CreateText(fullPath);

        open = true;
    }

    public void LogLine(string s)
    {
        lock (handle)
        {
            streamWriter.WriteLine(s);
            streamWriter.Flush();
        }
    }

    public void Close()
    {
        if (open)
        {
            streamWriter.Close();
            open = false;
        }
    }

    ~DebugDataLogger()
    {
        Close();
    }
}