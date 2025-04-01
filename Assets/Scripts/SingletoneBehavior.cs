using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

// 제네릭
// : 타입을 변수처럼 사용하는 기능 <T> : 타입 매개변수
// where T : 제약조건

public class SingletoneBehaviour<T> : MonoBehaviour where T : SingletoneBehaviour<T>
{
    // SceneLoader.Instance.LoadScene(Scene)
    
    // 파괴여부
    protected bool m_IsDestroyed = false;

    // 정적변수
    protected static T m_Instance;

    // 외부에서 접근할 수 있도록 해주는 public 변수 
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