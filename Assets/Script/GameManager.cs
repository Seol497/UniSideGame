using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 필드
    [Header("이미지")]
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;

    [Header("버튼 UI")]
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    Image titleImage;

    [Header("시간 UI")]
    public GameObject timeBar;  // 시간 표시 이미지
    public GameObject timeText; // 시간 텍스트
    TimeController timeCnt;     // 타임 컨트롤러 연결
    #endregion

    #region 유니티 라이프 사이클
    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
                    timeBar.SetActive(false); // 시간 제한이 없다면 숨기겠습니다.
        }

        Invoke("InactiveImage", 1.0f); // 1초 뒤에 해당 함수를 실행하는 기능(Invoke)
        panel.SetActive(false); // 패널 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        //1번 : 게임 클리어 2번 : 게임 오버 0번 : 게임 진행 3번 : 게임 끝
        if (PlayerController.gameState == Enum.GetName(typeof(GameState), 1))
        {
            // 게임이 클리어 시 처리할 명령
            mainImage.SetActive(true); // 이미지 활성화
            panel.SetActive(true); // 패널 활성화
            restartButton.GetComponent<Button>().interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = Enum.GetName(typeof(GameState), 3);

            // 시간 제한
            if (timeCnt != null)
                timeCnt.isTimeOver = true;
        }
        else if (PlayerController.gameState == Enum.GetName(typeof(GameState), 2))
        {
            // 게임 끝났을 때 처리할 명령
            mainImage.SetActive(true);
            panel.SetActive(true);
            restartButton.GetComponent<Button>().interactable = true;
            nextButton.GetComponent<Button>().interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = Enum.GetName(typeof(GameState), 3);

            // 시간 제한
            if (timeCnt != null)
                timeCnt.isTimeOver = true;
        }
        else if (PlayerController.gameState == Enum.GetName(typeof(GameState), 0))
        {
            // 게임이 진행 중일때 처리할 명령
            var player = GameObject.FindGameObjectWithTag("Player");

            var playerController = player.GetComponent<PlayerController>();

            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.1f)
                {
                    // 소수점 이하는 버려서 계산합니다
                    int time = (int)timeCnt.displayTime;

                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();
                    // Text(Legacy)로 작업한 경우
                    // timeText.GetComponent<Text>().text = time.ToString();

                    if (time == 0)
                    {
                        // 플레이어 컨트롤러의 게임 오버 기능을 호출합니다.
                        playerController.GameOver();
                    }
                }
            }
        }



    }
    #endregion


    #region 메서드
    void InactiveImage()
    {
        mainImage.SetActive(false); // 이미지 비활성화
    }
    #endregion
}
