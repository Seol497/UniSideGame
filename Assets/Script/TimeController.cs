using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("시간")]
    public bool isCoundDown = true; // 카운트 다운을 진행할 지를 판단
    public float gameTime = 0;      // 게임의 최대 시간을 설정하는 변수
    public bool isTimeOver = false; // 타이메를 정지할지를 판단 
    public float displayTime = 0;   // 화면에 표시될 시간 

    float times = 0; // 현재 시간 체크

    // Start is called before the first frame update
    void Start()
    {
        // 카운트 다운을 진행하는 경우 화면에 표시할 시간을 게임 시간으로 설정합니다.
        displayTime = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        times += Time.deltaTime;

        if (isCoundDown)
        {
            displayTime = gameTime - times;
            if (displayTime < 0.0f)
            {
                displayTime = 0.0f;
                isTimeOver = true;
            }
        }
        else
        {
            displayTime = times;
            if(displayTime >= gameTime)
            {
                displayTime = gameTime;
                isTimeOver = true;
            }
        }

    }
}
