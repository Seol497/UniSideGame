using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;


    // 씬 매니저 클래스는 유니티에서 씬을 관리하는
    // 클래스입니다.

    // SceneManager가 using인 상태에서 사용이 가능합니다.
    public void Load()
    {
        
        SceneManager.LoadScene(SceneName);
        
    }
}
