using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterGhost : MonoBehaviour
{
    string state;
    float existTime = 0;
    Vector3 up;
    Vector3 down;
    public GameObject platform;

    //敌人组件和碰撞盒
    Enemy enemy;
    BoxCollider2D boxCollider;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.enabled = false;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        existTime = 0;
    }

    void Update()
    {
        down = platform.transform.position;
        up = down + Vector3.up * 4.0f;
        existTime += Time.deltaTime;
        if ((int)existTime % 2 == 0)
        {
            state = "up";
        }
        else
        {
            state = "down";
        }
        if (state == "up" && transform.position != up)
        {
            Vector3 d = Vector3.MoveTowards(transform.position, up, 10f * Time.deltaTime);
            transform.Translate(d - transform.position, Space.Self);
        }
        else if (state == "down" && transform.position != down)
        {
            Vector3 d = Vector3.MoveTowards(transform.position, down, 10f * Time.deltaTime);
            transform.Translate(d - transform.position, Space.Self);
        }

        //在上面的时候激活enemy组件，能够伤害到玩家
        if(transform.position == up)
        {
            enemy.enabled = true;
            boxCollider.enabled = true;
            Debug.Log("在上面了"+transform.position);
        }
        else
        {
            enemy.enabled = false;
            boxCollider.enabled = false;
        }
            

    }
}
