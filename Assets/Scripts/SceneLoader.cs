using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneLoader;

//  ���� �ε��ϴ� Ŭ����
public class SceneLoader : SingletoneBehaviour<SceneLoader>
{
    // enum
     public enum Scenetype
    {
        Title,
        Lobby,
        InGame
    }

    // ȣ�� ��� : SceneLoader.Instance.LoadScene(Scenetype, Title)
    // �ε��Լ�
    public void LoadScene(Scenetype scenetype)  
    {
        // �α�
        Logger.Log($"{scenetype} scene loading...");

        // �ð� �ʱ�ȭ
        Time.timeScale = 1f;
        SceneManager.LoadScene(scenetype.ToString());
    }

    // ���ε��Լ�
    public void ReloadScene()
    {
        // �α�
        Logger.Log($"{SceneManager.GetActiveScene().name} scene loading...");

        Time.timeScale = 1f;    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �񵿱� �� �ε��Լ�
    public AsyncOperation AsyncLoadScene(Scenetype scenetype)
    {
        Logger.Log($"{scenetype} scene async loading...");

        Time.timeScale = 1f;
        return SceneManager.LoadSceneAsync(scenetype.ToString());
    }
}