﻿using UnityEngine;

namespace Devdog.General
{
    public static class DevdogLogger
    {
        public enum LogType
        {
            LogVerbose = 0,
            Log = 1,
            Warning = 2,
            Error = 3
        }

        public static LogType minimaLog = LogType.LogVerbose;

        public static void LogVerbose(string message)
        {
            //            LogVerbose(message, null);
            if ((int)LogType.LogVerbose >= (int)minimaLog) Debug.Log(message);
        }

        public static void LogVerbose(string message, Object context)
        {
            if ((int)LogType.LogVerbose >= (int)minimaLog) Debug.Log(message, context);
        }

        public static void Log(string message, Object context = null)
        {
            if ((int)LogType.Log >= (int)minimaLog) Debug.Log(message, context);
        }

        public static void LogWarning(string message, Object context = null)
        {
            if ((int)LogType.Warning >= (int)minimaLog) Debug.LogWarning(message, context);
        }

        public static void LogError(string message, Object context = null)
        {
            if ((int)LogType.Error >= (int)minimaLog) Debug.LogError(message, context);
        }
    }
}