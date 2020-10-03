using System;
using UnityEditor;
using UnityEngine;

namespace Belwyn.Utils {

    // This classes are a implementation of Unity's Debug class, however each method here is 
    // If the TEST_BUILD is not defined the debug calls will not happen, making a non-test build cleaner, and without needing to change your code!

    // I've separated it in two classes for better comprehension. Logger is for logging, Debugger is for other debug things.

    public class Debugger {

        // Use this method with a lambda [ () => function(); ] to execute it only when testing
        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Invoke(Action action) {
            action?.Invoke();
        }


        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Break() {
            Debug.Break();
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void ClearDevelopmentConsole() {
            Debug.ClearDeveloperConsole();
        }


        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void DrawLine(Vector3 start, Vector3 end, Color color = default, float duration = 0.0f, bool depthTest = true) {
            Debug.DrawLine(start, end, color, duration, depthTest);
        }
        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color = default, float duration = 0.0f, bool depthTest = true) {
            Debug.DrawRay(start, dir, color, duration, depthTest);
        }

    }


    public class Logger {

    #region Assert
    
        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Assert(bool condition) {
            Debug.Assert(condition);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Assert(bool condition, UnityEngine.Object context) {
            Debug.Assert(condition, context);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Assert(bool condition, object message) {
            Debug.Assert(condition, message);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Assert(bool condition, object message, UnityEngine.Object context) {
            Debug.Assert(condition, message, context);
        }

    #endregion Assert




    #region AssertFormat

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void AssertFormat(bool condition, string format, params object[] args) {
            Debug.AssertFormat(condition, format, args);
        }


        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void AssertFormat(bool condition, UnityEngine.Object context, string format, params object[] args) {
            Debug.AssertFormat(condition, context, format, args);
        }

    #endregion AssertFormat




    #region Log

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Log(object message) {
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void Log(object message, UnityEngine.Object context) {
            Debug.Log(message, context);
        }

    #endregion Log




    #region LogAssertion

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogAssertion(object message) {
            Debug.LogAssertion(message);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogAssertion(object message, UnityEngine.Object context) {
            Debug.LogAssertion(message, context);
        }

    #endregion




    #region LogAssertionFormat

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public void LogAssertionFormat(string format, params object[] args) {
            Debug.LogAssertionFormat(format, args);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args) {
            Debug.LogAssertionFormat(context, format, args);
        }

    #endregion LogAssertionFormat




    #region LogError

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogError(object message) {
            Debug.LogError(message);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogError(object message, UnityEngine.Object context) {
            Debug.LogError(message, context);
        }

    #endregion




    #region LogErrorFormat

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogErrorFormat(string format, params object[] args) {
            Debug.LogErrorFormat(format, args);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args) {
            Debug.LogErrorFormat(context, format, args);
        }

    #endregion LogErrorFormat





    #region LogException

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogException(Exception exception) {
            Debug.LogException(exception);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogException(Exception exception, UnityEngine.Object context) {
            Debug.LogException(exception, context);
        }

    #endregion LogException





    #region LogFormat

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogFormat(string format, params object[] args) {
            Debug.LogFormat(format, args);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogFormat(UnityEngine.Object context, string format, params object[] args) {
            Debug.LogFormat(context, format, args);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogFormat(LogType logType, LogOption logOptions, UnityEngine.Object context, string format, params object[] args) {
            Debug.LogFormat(logType, logOptions, context, format, args);
        }

    #endregion LogFormat





    #region LogWarning

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogWarning(object message) {
            Debug.LogWarning(message);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogWarning(object message, UnityEngine.Object context) {
            Debug.LogWarning(message, context);
        }

    #endregion LogWarning





    #region LogWarningFormat

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogWarningFormat(string format, params object[] args) {
            Debug.LogWarningFormat(format, args);
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args) {
            Debug.LogWarningFormat(context, format, args);
        }

    #endregion LogWarningFormat

    }

}