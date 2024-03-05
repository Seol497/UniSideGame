using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    // 2024-02-29 ���� �߰�(���� ��)
    playing,gameclear,gameover, gameend
}


public class PlayerController : MonoBehaviour
{
    #region �̵� ���
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f; // �̵��ӵ�
    public float high = 0.0f;
    #endregion

    #region ���� ���
    public float jump = 9.0f;        // ������
    public LayerMask groundLayer;   // ������ ���̾�
    bool goJump = false;            // ���� �������� �Ǵ�
    bool onGround = false;           // ���� ���ִ��� �Ǵ�
    #endregion

    #region �ִϸ��̼� ���
    Animator animator;
    public List<string> animation_moves; // å�� �ڵ� �������δ� ������ ���� �� �ۼ�
    string nowAnime = "";
    string oldAnime = "";
    #endregion

    public static string gameState = Enum.GetName(typeof(GameState), 0); // ���ڸ� �ٲٸ� �� ���ڿ� �ش��ϴ� enum�� �̸��� ����˴ϴ�.

    public int score = 0; // �÷��̾ ŉ���� ����


    // Start is called before the first frame update
    void Start()
    {
        // ������ ���� ���·� �����մϴ�.
        gameState = Enum.GetName(typeof(GameState), 0);

        // rbody�� ����� �����ɴϴ�.
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        nowAnime = animation_moves[0]; // �ִϸ��̼� 0��° ���� �⺻ ������ �����մϴ�.
        oldAnime = animation_moves[0];

    }

    // Update is called once per frame
    void Update()
    {
        // ������ �÷��� ������ ���� ������Ʈ�� �۵��ϵ��� �ڵ带 �����մϴ�.
        if(gameState != Enum.GetName(typeof(GameState), 0))
        {
            return;
        }
        // Axis�� ���� ���� �޾ƿ��� ���(�Է��� ���ؼ�)
        // 1 �� ���� �޾ƿɴϴ�.
        axisH = Input.GetAxisRaw("Horizontal");

        if(axisH > 0.0f && onGround)
        {
            transform.localScale = new Vector2(1, 1);
            // �� �۾��� ��������Ʈ �������� ���� �۾��Ѵٸ�?
            // GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(axisH < 0.0f && onGround)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // ���� ����
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
        // velocity�� ������ �ٵ𿡼� �ӷ��� �ǹ��մϴ�.

        // Vector2�� ����Ƽ���� x�� y ��ǥ�� ǥ���Ҷ� ����ϴ� �������Դϴ�.

        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        // ���� ĳ���� �� �κ� �Ʒ��� �������� ������ ���� ���� �� �ٴ��� ���������� �Ǵ��ϴ� �ڵ带 ���� ��
        // ���� ���Ǵ� ���

        // ���� ��� �ִ� ��� �Ǵ� �����δ� �۾��� ������ ���
        if (onGround && axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
            // ������ٵ��� �ְ� = (axisH ���� * 3.0 ��ŭ�� x ��ǥ, ������ �ٵ� �ӷ��� y) ��ǥ �� ���� ����

        }

        // ������ ���� Ű�� ���� ���
        if (onGround && goJump)
        {
            // �� �������� �̵� �� goJump�� false�� ����
            var jumPw = new Vector2(0, jump);
            rbody.AddForce(jumPw, ForceMode2D.Impulse);
            goJump = false;
        }

        //�ִϸ��̼� ���� ó��
        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = animation_moves[0]; // ����
            }
            else
            {
                nowAnime = animation_moves[1]; // �̵�
            }
        }
        else
        {
            nowAnime = animation_moves[2]; // ����
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime; //���� �۾� �����
            animator.Play(nowAnime); //���� ������ �ִϸ��̼� �÷���
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Goal" �̶��
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
            Debug.Log($"ŉ���� ���� ={score}");
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
        // ���̻� �浹�� ���� �ʵ��� �浹 ������ �����մϴ�.
        GetComponent<CapsuleCollider2D>().enabled = false;
        // �÷��̾ ���� Ƣ������� �����մϴ�.
        rbody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
    }

    public void GameStop()
    {
        rbody.velocity = Vector2.zero; //�ӷ��� 0���� ����
    }

}
