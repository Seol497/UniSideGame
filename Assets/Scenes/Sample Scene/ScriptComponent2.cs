using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptComponent2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 개체가 가지고 있는 스프라이트 렌더러의 기능을 가져와서
        // 그 기능을 바로 사용하는 코드
        // 이런 형태의 코드를 작성하기 위해선 이 스크립트를 가진 게임 오브젝트가 
        // 해당 컴포넌트를 가지고 있어야 합니다.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 1, 0);
        
    }
}
