using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

// ���׸�
// : Ÿ���� ����ó�� ����ϴ� ��� <T> : Ÿ�� �Ű�����
// where T : ��������

public class SingletoneBehaviour<T> : MonoBehaviour where T : SingletoneBehaviour<T>
{
    // SceneLoader.Instance.LoadScene(Scene)
    
    // �ı�����
    protected bool m_IsDestroyed = false;

    // ��������
    protected static T m_Instance;

    // �ܺο��� ������ �� �ֵ��� ���ִ� public ���� 
    // static : 
    public static T Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (m_Instance == null)
        {
            m_Instance = (T)this;

            if (m_IsDestroyed)
            {
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        m_Instance = null;
    }
}