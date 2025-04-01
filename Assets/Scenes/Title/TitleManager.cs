using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ÿ��Ʋ ���� �����ϴ� �Ŵ���
public class TitleManeger : MonoBehaviour
{
    // ó�� ������ �� �ΰ� �ִϸ��̼� ���� ����
    // �ִϸ��̼��� ������
    // ���� Ÿ�̺z�� ����.

    // ������
    public Animation logoAnim;
    public TextMeshProUGUI logoText;

    public GameObject TitlePrefab;
    public TextMeshProUGUI loadingSliderTxt;
    public Slider loadingSlider;

    // AsyncOperation : ����Ƽ���� �񵿱� �۾��� �����ϴ� ��ü. �� �ε� ������� ����
    // AsyncOperation.progress. .isDone .allowSceneActivation
    private AsyncOperation _loadingOperation;

    private void Awake()
    {
        // ó�� ���� �� �ΰ� Ȱ��ȭ, Ÿ��Ʋ ������ ��Ȱ��ȭ
        logoAnim.gameObject.SetActive(true);
        TitlePrefab.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadGameCo());
    }

    private IEnumerator LoadGameCo()
    {
        // �α�
        Logger.Log($"{GetType()}:: LoadingGameCo");

        // �ִϸ��̼� ����
        logoAnim.Play();

        // �ִϸ��̼� ���� �� ���� ��ٸ���
        yield return new WaitForSeconds(logoAnim.clip.length);

        logoAnim.gameObject.SetActive(false);
        TitlePrefab.SetActive(true);

        _loadingOperation = SceneLoader.Instance.AsyncLoadScene(SceneLoader.Scenetype.Lobby);
        if (_loadingOperation == null)
        {
            Logger.Log("Loading async loading error");
            yield break;
        }

        // �κ���� �� ��ȯ�� ����.
        _loadingOperation.allowSceneActivation = false;

        // �ε��� �� ���̵���
        loadingSlider.value = 0.5f;
        loadingSliderTxt.text = $"{loadingSlider.value * 100}%";
        yield return new WaitForSeconds(0.5f);

        // �ε� ���� ���� ��
        while (!_loadingOperation.isDone)
        {
            loadingSlider.value = _loadingOperation.progress < 0.5f ? 0.5f : _loadingOperation.progress;
            loadingSliderTxt.text = $"{loadingSlider.value * 100}%"; // int ���ϱ� ���� �۵�?

            // �ε� �Ϸ�Ǹ� ��ȯ
            // 90% ���� �׻� ����.
            if (_loadingOperation.progress >= 0.9f)
            {
                _loadingOperation.allowSceneActivation = true;
                yield break;
            }
        }
    }
}