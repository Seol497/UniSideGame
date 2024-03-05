using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    // 2024-02-29 상태 추가(게임 끝)
    playing,gameclear,gameover, gameend
}


public class PlayerController : MonoBehaviour
{
    #region 이동 기능
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f; // 이동속도
    public float high = 0.0f;
    #endregion

    #region 점프 기능
    public float jump = 9.0f;        // 점프력
    public LayerMask groundLayer;   // 착지할 레이어
    bool goJump = false;            // 점프 상태인지 판단
    bool onGround = false;           // 땅에 서있는지 판단
    #endregion

    #region 애니메이션 기능
    Animator animator;
    public List<string> animation_moves; // 책의 코드 기준으로는 변수를 각각 다 작성
    string nowAnime = "";
    string oldAnime = "";
    #endregion

    public static string gameState = Enum.GetName(typeof(GameState), 0); // 숫자만 바꾸면 그 숫자에 해당하는 enum의 이름이 적용됩니다.

    public int score = 0; // 플레이어가 흭득할 점수


    // Start is called before the first frame update
    void Start()
    {
        // 게임을 시작 상태로 변경합니다.
        gameState = Enum.GetName(typeof(GameState), 0);

        // rbody의 기능을 가져옵니다.
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        nowAnime = animation_moves[0]; // 애니메이션 0번째 값을 기본 값으로 설정합니다.
        oldAnime = animation_moves[0];

    }

    // Update is called once per frame
    void Update()
    {
        // 게임이 플레이 상태일 때만 업데이트가 작동하도록 코드를 설계합니다.
        if(gameState != Enum.GetName(typeof(GameState), 0))
        {
            return;
        }
        // Axis에 대한 값을 받아오는 기능(입력을 통해서)
        // 1 씩 값을 받아옵니다.
        axisH = Input.GetAxisRaw("Horizontal");

        if(axisH > 0.0f && onGround)
        {
            transform.localScale = new Vector2(1, 1);
            // 이 작업을 스프라이트 렌더러를 통해 작업한다면?
            // GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(axisH < 0.0f && onGround)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // 점프 로직
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        goJump = true;
    }

    private void FixedUpdate()
    {
        if (gameState != Enum.GetName(typeof(GameState), 0))
        {
            return;
        }
        // velocity는 리지드 바디에서 속력을 의미합니다.

        // Vector2는 유니티에서 x와 y 좌표를 표현할때 사용하는 데이터입니다.

        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        // 보통 캐릭터 발 부분 아래를 기준으로 가상의 선을 쏴서 그 바닥이 무엇인지를 판단하는 코드를 적을 때
        // 많이 사용되는 기능

        // 땅을 밟고 있는 경우 또는 움직인는 작업을 진행할 경우
        if (onGround && axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
            // 리지드바디의 솔격 = (axisH 방향 * 3.0 만큼의 x 좌표, 리지드 바디 속력의 y) 좌표 로 벡터 생성

        }

        // 땅에서 점프 키를 누른 경우
        if (onGround && goJump)
        {
            // 윗 방향으로 이동 후 goJump를 false로 변경
            var jumPw = new Vector2(0, jump);
            rbody.AddForce(jumPw, ForceMode2D.Impulse);
            goJump = false;
        }

        //애니메이션 판정 처리
        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = animation_moves[0]; // 정지
            }
            else
            {
                nowAnime = animation_moves[1]; // 이동
            }
        }
        else
        {
            nowAnime = animation_moves[2]; // 점프
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime; //이전 작업 저장용
            animator.Play(nowAnime); //현재 설정된 애니메이션 플레이
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Goal" 이라면
        if(collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if (collision.gameObject.tag == "SpeedUp")
        {
            speed *= 2.5f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Coin")
        {
            var itemData = collision.gameObject.GetComponent<ItemData>();
            score += 10;  //itemData.value;
            Debug.Log($"흭득한 점수 ={score}");
            Destroy(collision.gameObject);
        }
    }

    public void Goal()
    {
        animator.Play(animation_moves[3]);
        gameState = Enum.GetName(typeof(GameState), 1);
        GameStop();
    }

    public void GameOver()
    {
        animator.Play(animation_moves[4]);
        gameState = Enum.GetName(typeof(GameState), 2);
        GameStop();
        // 더이상 충돌을 하지 않도록 충돌 판정을 해제합니다.
        GetComponent<CapsuleCollider2D>().enabled = false;
        // 플레이어가 위로 튀어오르게 설계합니다.
        rbody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
    }

    public void GameStop()
    {
        rbody.velocity = Vector2.zero; //속력을 0으로 설정
    }

}
