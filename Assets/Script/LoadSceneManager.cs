using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static string SceneName;
    [SerializeField]Image progress_bar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadScene");
    }

    public static void LoadScene(string sceneName)
    {
        SceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // IEnumerator는 유니티에서 코루틴 설계시 사용하는 인터페이스입니다. 
    IEnumerator LoadScene()
    {
        yield return null; // 한 프레임 대기
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneName);
        // 일반 LoadScene과 다르게 비동기적으로 씬을 호출합니다.
        op.allowSceneActivation = false;
        // 씬이 활성화되는 것에 대한 허가를 막습니다.

        float time = 0.0f; // 시간을 체크합니다.

        // 오퍼레이션의 작업이 진행되는 동안 작업을 반복합니다.
        while (!op.isDone)
        {
            yield return null; // 한 프레임 탈출
            time += Time.deltaTime; // 프레임 당 시간 추가

            // 오퍼레이션의 작업이 진행중일 때
            if (op.progress < 0.0f)
            {
                progress_bar.fillAmount = Mathf.Lerp(progress_bar.fillAmount, op.progress, time);
                // Mathf.Lerp(a,b,c)는 a부터 b까지 c 간격으로 서서히 다가가는 기능입니다.(MoveToward와 비슷)

                //작업량 도달 시 시간 0
                if(progress_bar.fillAmount >= op.progress)
                {
                    time = 0.0f;
                }
                else // 작업이 마무리 될 때
                {
                    progress_bar.fillAmount = Mathf.Lerp(progress_bar.fillAmount, 1.0f, time);
                    // 프로그래스 바의 크기 -> 1.0이 되도록 처리합니다.
                    if(progress_bar.fillAmount == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        time = 0.0f;
                    }
                }

            }
        }
    }

}
