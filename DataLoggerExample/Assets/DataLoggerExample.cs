using System.Collections;
using UnityEngine;
using System.Threading;

public class DataLoggerExample : MonoBehaviour {

    private DebugDataLogger updateLogger;
    private DebugDataLogger lessFrequentLogger;
    private DebugDataLogger audioLogger;

    private PerformanceCounter counter;

    private long timeStarted;

    void Awake () {
        updateLogger = new DebugDataLogger("update", false);
        lessFrequentLogger = new DebugDataLogger("lessfrequent", false);
        audioLogger = new DebugDataLogger("audio", false);

        counter = new PerformanceCounter();
        timeStarted = counter.GetPerformanceCounter();
	}

    private void Start()
    {
        StartCoroutine(LessFrequentBursts());
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        FakeSomeWork(audioLogger, 5);
    }

    void Update ()
    {
        FakeSomeWork(updateLogger, 8);
    }

    private IEnumerator LessFrequentBursts()
    {
        for (int i = 0; i < 5; i++)
        {
            FakeSomeWork(lessFrequentLogger, 10);
            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    private void FakeSomeWork(DebugDataLogger logger, int milliseconds)
    {
        double timeNow = counter.CounterToMillis(timeStarted, counter.GetPerformanceCounter());
        logger.LogLine(timeNow + ", 0");
        logger.LogLine(timeNow + ", 1");
        Thread.Sleep(milliseconds);
        timeNow = counter.CounterToMillis(timeStarted, counter.GetPerformanceCounter());
        logger.LogLine(timeNow + ", 1");
        logger.LogLine(timeNow + ", 0");
    }

    private void OnApplicationQuit()
    {
        GetComponent<AudioSource>().Stop();

        audioLogger.Close();
        updateLogger.Close();
        lessFrequentLogger.Close();
    }
}
