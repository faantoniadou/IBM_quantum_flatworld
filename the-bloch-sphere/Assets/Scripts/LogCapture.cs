using System.Collections.Generic;
using UnityEngine;

public static class LogCapturer
{
    private static List<string> logs = new List<string>();

    static LogCapturer()
    {
        Application.logMessageReceived += HandleLog;
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        logs.Add(logString);
    }

    public static List<string> GetLogs()
    {
        return new List<string>(logs);
    }

    public static void ClearLogs()
    {
        logs.Clear();
    }
}
