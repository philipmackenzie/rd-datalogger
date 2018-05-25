import sys
import datetime
import matplotlib.pyplot as plt
import matplotlib
import os

from matplotlib.ticker import FormatStrFormatter


def ticksToPython(ticks):
    ticksAsLong = long(ticks)
    return datetime.datetime(1, 1, 1) + datetime.timedelta(microseconds = (ticksAsLong)/10)


def millisToPythonTime(millis):
    millisAsDouble = float(millis)
    return datetime.datetime(1, 1, 1) + datetime.timedelta(milliseconds = millisAsDouble)


def readDataFile(fileName):
    lines = [line.rstrip('\n') for line in open(fileName)]

    timestamps = []
    values = []
    for line in lines:
        timestamp, value = line.split(",")
        timestamps.append(timestamp)
        #.strftime("%y%m%d_%H%M%S.%f")
        values.append(value)

    return timestamps, values


def runExample(dir):
    s = "diagnostics_";
    audioFile = os.path.join(dir, s + "audio.csv")
    updateFile = os.path.join(dir, s + "update.csv")
    lessFrequentFile = os.path.join(dir, s + "lessfrequent.csv")

    plotFiles(audioFile, updateFile, lessFrequentFile)


def plotFiles(audioFile, updateFile, lessFrequentFile):

    print(audioFile)
    print(updateFile)
    print(lessFrequentFile)

    primaryTimes, primaryValues = readDataFile(audioFile)
    secondaryTimes, secondaryValues = readDataFile(updateFile)
    lessFrequentTimes, lessFrequentValues = readDataFile(lessFrequentFile)

    plt.close("all")
    plt.figure(1)

    maxYValue = 1.5

    ax1 = plt.subplot(411)
    plt.plot(primaryTimes, primaryValues, color='blue')
    x1, x2, y1, y2 = plt.axis()
    plt.axis((x1, x2, 0, maxYValue))
    plt.xlabel('time (ms)')
    plt.ylabel('Timing')
    plt.title('OnAudioFilterRead()')
    plt.grid(True)

    ax1 = plt.subplot(412, sharex = ax1)
    plt.plot(secondaryTimes, secondaryValues, color='red')
    x1, x2, y1, y2 = plt.axis()
    plt.axis((x1, x2, 0, maxYValue))
    plt.xlabel('time (ms)')
    plt.ylabel('Timing')
    plt.title('Update()')
    plt.grid(True)

    ax1 = plt.subplot(413, sharex = ax1)
    plt.plot(lessFrequentTimes, lessFrequentValues, color='green')
    x1, x2, y1, y2 = plt.axis()
    plt.axis((x1, x2, 0, maxYValue))
    plt.xlabel('time (ms)')
    plt.ylabel('Timing')
    plt.title('CoRoutine')
    plt.grid(True)

    ax1 = plt.subplot(414, sharex = ax1)
    plt.plot(primaryTimes, primaryValues, color='blue')
    plt.plot(secondaryTimes, secondaryValues, color='red')
    plt.plot(lessFrequentTimes, lessFrequentValues, color='green')
    x1, x2, y1, y2 = plt.axis()
    plt.axis((x1, x2, 0, maxYValue))
    plt.xlabel('time (ms)')
    plt.ylabel('Timing')
    plt.title('Plot all 3')
    plt.grid(True)

    plt.show()

if __name__=="__main__":
    runExample(sys.argv[1])

