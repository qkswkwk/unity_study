using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

// 로그해주는 클래스
public static class Logger
{
    // Conditional : 특정 조건에서만 가능할 수 있도록 제어
    // Conditional("Unity Editor")  유니티 에디터에서만 노출
    // Conditional("Unity_IOS"), Conditional("Unity_Android")   아이폰이나 안드로이드에서만 노출

    // 로그
    [Conditional("DEV_VER")]
    public static void Log(string message)
    {
        Debug.LogFormat("[{0}] {1}", System.DateTime.Now.ToString(format: "HH:mm:ss"), message);
    }

    // 워닝
    [Conditional("DEV_VER")]
    public static void LogWarning(string message)
    {
        Debug.LogWarningFormat("[{0}] {1}", System.DateTime.Now.ToString(format: "HH:mm:ss"), message);
    }

    // 에러
    public static void LogError(string message)
    {
        Debug.LogErrorFormat("[{0}] {1}", System.DateTime.Now.ToString(format: "HH:mm:ss"), message);
    }
}
