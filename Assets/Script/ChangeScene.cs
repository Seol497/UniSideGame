using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;


    // �� �Ŵ��� Ŭ������ ����Ƽ���� ���� �����ϴ�
    // Ŭ�����Դϴ�.

    // SceneManager�� using�� ���¿��� ����� �����մϴ�.
    public void Load()
    {
        
        SceneManager.LoadScene(SceneName);
        
    }
}
