using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 클래스는 유니티 에디터의 게임 오브젝트에 연결할 수 있습니다.

// RequireComponent(typeof())는
// 이 스크립트를 사용할 때 해당 컴포넌트를 요구하는 속성입니다.
// 이 속성이 붙어있는 스크립트를 오브젝트의 컴포넌트로써 연결할 경우
// 오브젝트에 해당 컴포넌트가 없을 경우 자동으로 연결을 진행합니다.
// 그리고 이 속성이 존재하는 동안 에디터 내에서 요구하는 컴포넌트를 사용자는 
// 제거할 수 없습니다.

[RequireComponent(typeof(AudioSource))]
public class ScriptComponent : MonoBehaviour
{
    public GameObject prefabs;
    // 1. 스크립트에서 public으로 컴포넌트를 요구할 경우
    // 에디터 내에서 연결을 진행해야 합니다
    // 안할 경우 코드 실행 시 NullReference 오류 발생
    // 해당 오류는 레퍼런스가 없을 때 뜨는 오휴
    // 레퍼런스는 참조를 의미하며, 해당 데이터에 대한 정보를 전달하는 용도로
    // 사용합니다.

    AudioSource audioSource;
    // 2. 스크립트에서 p[ublic이 아닌 상황에서 컴포넌트를 선언한 경우
    // 게임을 시작하기 전, 해당 기능을 개체로부터 받아오는 작업을 통해
    // 연결을 진행합니다.

    GameObject nomal_object;
    // 3. 스크립트에서 public이 아닌 상황에서 컴포넌트를 선언한 경우
    // 월드 내에서 해당 개체를 찾아서 연결하는 방법


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // GetComponent<T>는 개체(오브젝트)로부터 <T> 위치에 작엉한 유형의 컴포넌트를 얻어오는
        // 기능을 가지고 있습니다.
        // 이 코드를 사용할 경우 반드시 이 스크립트를 컴포넌트로 가지고 있는 오브젝트는
        // 해당 컴포넌트를 가지고 있어야 정상적으로 작동합니다.
        // 아닐 경우 NillReference 오류 발생

        audioSource.Play();
        // 위의 코드로 인해 변수 audiosource는 AudioSource 변수가 되었고
        // 해당 컴포넌트의 기능을 사용할 수 있게 됩니다.

        nomal_object = GameObject.Find("nomal_object");
        // GameObject.Find() 는 () 안에 개체의 이름을 작성하면, 해당 이름을 게임 내부에서
        // 조사하는 기능입니다.
        // 없을 경우 오류가 발생합니다.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
