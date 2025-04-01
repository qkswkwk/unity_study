using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 타이틀 씬을 관리하는 매니저
public class TitleManeger : MonoBehaviour
{
    // 처음 등장할 때 로고 애니메이션 먼저 등장
    // 애니메이션이 끝나면
    // 게임 타이틑이 등장.

    // 변수들
    public Animation logoAnim;
    public TextMeshProUGUI logoText;

    public GameObject TitlePrefab;
    public TextMeshProUGUI loadingSliderTxt;
    public Slider loadingSlider;

    // AsyncOperation : 유니티에서 비동기 작업을 관리하는 객체. 씬 로딩 진행상태 제어
    // AsyncOperation.progress. .isDone .allowSceneActivation
    private AsyncOperation _loadingOperation;

    private void Awake()
    {
        // 처음 등장 시 로고 활성화, 타이틀 프리팹 비활성화
        logoAnim.gameObject.SetActive(true);
        TitlePrefab.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadGameCo());
    }

    private IEnumerator LoadGameCo()
    {
        // 로그
        Logger.Log($"{GetType()}:: LoadingGameCo");

        // 애니메이션 실행
        logoAnim.Play();

        // 애니메이션 끝날 때 까지 기다리기
        yield return new WaitForSeconds(logoAnim.clip.length);

        logoAnim.gameObject.SetActive(false);
        TitlePrefab.SetActive(true);

        _loadingOperation = SceneLoader.Instance.AsyncLoadScene(SceneLoader.Scenetype.Lobby);
        if (_loadingOperation == null)
        {
            Logger.Log("Loading async loading error");
            yield break;
        }

        // 로비로의 씬 전환을 막음.
        _loadingOperation.allowSceneActivation = false;

        // 로딩이 잘 보이도록
        loadingSlider.value = 0.5f;
        loadingSliderTxt.text = $"{loadingSlider.value * 100}%";
        yield return new WaitForSeconds(0.5f);

        // 로딩 진행 중일 때
        while (!_loadingOperation.isDone)
        {
            loadingSlider.value = _loadingOperation.progress < 0.5f ? 0.5f : _loadingOperation.progress;
            loadingSliderTxt.text = $"{loadingSlider.value * 100}%"; // int 빼니까 정상 작동?

            // 로딩 완료되면 전환
            // 90% 에서 항상 멈춤.
            if (_loadingOperation.progress >= 0.9f)
            {
                _loadingOperation.allowSceneActivation = true;
                yield break;
            }
        }
    }
}