using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

// �α����ִ� Ŭ����
public static class Logger
{
    // Conditional : Ư�� ���ǿ����� ������ �� �ֵ��� ����
    // Conditional("Unity Editor")  ����Ƽ �����Ϳ����� ����
    // Conditional("Unity_IOS"), Conditional("Unity_Android")   �������̳� �ȵ���̵忡���� ����

    // �α�
    [Conditional("DEV_VER")]
    public static void Log(string message)
    {
        Debug.LogFormat("[{0}] {1}", System.DateTime.Now.ToString(format: "HH:mm:ss"), message);
    }

    // ����
    [Conditional("DEV_VER")]
    public static void LogWarning(string message)
    {
        Debug.LogWarningFormat("[{0}] {1}", System.DateTime.Now.ToString(format: "HH:mm:ss"), message);
    }

    // ����
    public static void LogError(string message)
    {
        Debug.LogErrorFormat("[{0}] {1}", System.DateTime.Now.ToString(format: "HH:mm:ss"), message);
    }
}
