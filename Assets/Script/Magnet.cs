using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템에 연결해서 플레이어를 탐색해 플레이어 근처로 움직이게 하는 코
public class Magnet : MonoBehaviour
{
    public float item_speed = 5.0f;       // 아이템이 끌려오는 속도   
    public float magnet_distance = 10.0f; // 자석 적용 범위 

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, player.position);
        // Vector3.Distance(a,b)는 a와 b 사이의 거리를 계산하는 문법

        if(distance < magnet_distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, item_speed * Time.deltaTime);
            // Vector3.MoveToward(a,b,c)는 a를 b까지 이동하는데 c만큼 속도로 이동합니다.
        }
    }
}
