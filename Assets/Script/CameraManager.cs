using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("스크롤 제한")]
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    [Header("서브 스크린")]
    public GameObject subScreen;

    [Header("강제 스크롤")]
    public bool isForceScrollX = false;
    public float forceScrollSpeedX = 0.5f;
    public bool isForceScrollY = false;
    public float forceScrollSpeedY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어를 추적해서 카메라가 따라가도록 설계
        //넣어준 스크롤의 제한 수치만큼 따라가게 하겠습니다.

        var player = GameObject.FindGameObjectWithTag("Player");

        // 플레이어가 존재할 경우
        if (player != null)
        {
            // x와 y축은 플레이어의 좌표를 설정
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            // z축은 카메라의 좌표를 설정
            float z = transform.position.z;

            // 가로 방향에 대한 처리
            if (isForceScrollX)
            {
                x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
            }
            if(x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }


            //세로 방향에 대한 처리
            if (isForceScrollY)
            {
                y = transform.position.x + (forceScrollSpeedY * Time.deltaTime);
            }
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if(y > topLimit)
            {
                y = topLimit;
            }
            
            // 설정된 위치로 적용합니다.
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

            //서브 스크린 이동
            if(subScreen != null)
            {
                // x의 경우는 현재 움직이고 있는 스크롤의
                // x를 사용
                // y나 z는 따로 필요가 없ㄱ에 서브 스크린의 좌표를 받아옵니다.
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;

                // 기존 x 값의 절반 수치만큼 이동해 배경과 어긋나게 스크롤
                // 되도록 연출합니다.
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;

            }

        }
    }
}
