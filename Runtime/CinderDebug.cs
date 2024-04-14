using System;
using System.Diagnostics;
using UnityEngine;

using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;


namespace CinderUtils {
    public static class CinderDebug {
        #region CONSTANTS
        public const string CINDER_DEBUG = "CINDER_DEBUG";

        #endregion

        #region Logs
        [Conditional(CINDER_DEBUG)]
        public static void Log(object message) {
            Debug.Log(message);
        }
        [Conditional(CINDER_DEBUG)]
        public static void Log(object message, Object context) {
            Debug.Log(message, context);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogFormat(string format, params object[] args) { 
            Debug.LogFormat(format, args);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogFormat(Object context, string format, params object[] args) { 
            Debug.LogFormat(context, format, args);
        }
        #endregion

        #region Warnings
        [Conditional(CINDER_DEBUG)]
        public static void LogWarning(object message) {
            Debug.LogWarning(message);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogWarning(object message, Object context) {
            Debug.LogWarning(message, context);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogWarningFormat(string format, params object[] args) { 
            Debug.LogWarningFormat(format, args);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogWarningFormat(Object context, string format, params object[] args) { 
            Debug.LogWarningFormat(context, format, args);
        }
        #endregion

        #region Errors
        [Conditional(CINDER_DEBUG)]
        public static void LogError(object message) {
            Debug.LogError(message);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogError(object message, Object context) {
            Debug.LogError(message, context);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogErrorFormat(string format, params object[] args) { 
            Debug.LogErrorFormat(format, args);
        }
        [Conditional(CINDER_DEBUG)]
        public static void LogErrorFormat(Object context, string format, params object[] args) { 
            Debug.LogErrorFormat(context, format, args);
        }
        #endregion

        #region Exceptions
        [Conditional(CINDER_DEBUG)]
        public static void LogException(Exception exception) {
            Debug.LogException(exception);
        }

        [Conditional(CINDER_DEBUG)]
        public static void LogException(Exception exception, Object context) {
            Debug.LogException(exception, context);
        }
        #endregion
    }
}
