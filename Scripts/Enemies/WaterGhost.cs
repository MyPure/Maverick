using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterGhost : MonoBehaviour
{
    string state;
    Vector3 up;
    Vector3 down;
    Vector3 preset;
    public GameObject platform;

    public float upTime = 1.0f;//上升时间
    public float downTime = 1.0f;//下降时间
    //敌人组件和碰撞盒
    Enemy enemy;
    BoxCollider2D boxCollider;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.enabled = false;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        preset = transform.localPosition;
    }

    void Update()
    {
        down = platform.transform.position + preset;//水鬼处在砖里的位置
        up = down + Vector3.up * 3.4f;//水鬼起来的位置

        float td = upTime + downTime;//一个周期的总时间
        if (Time.time % td < upTime)
        {
            state = "up";
        }
        else
        {
            state = "down";
        }

        //水鬼移动
        if (state == "up" && transform.position != up)
        {
            Vector3 d = Vector3.MoveTowards(transform.position, up, 10f * Time.deltaTime);//10为速度，可修改
            transform.Translate(d - transform.position, Space.Self);
        }
        else if (state == "down" && transform.position != down)
        {
            Vector3 d = Vector3.MoveTowards(transform.position, down, 10f * Time.deltaTime);
            transform.Translate(d - transform.position, Space.Self);
        }

        //在上面的时候激活enemy组件，能够伤害到玩家
        if (platform.GetComponent<Platform>().velocity == 0)
        {
            if (transform.position == up)
            {
                enemy.enabled = true;
                boxCollider.enabled = true;
            }
            else
            {
                enemy.enabled = false;
                boxCollider.enabled = false;
            }
        }
        else//当砖块开始掉落的时候水鬼不起作用
        {
            enemy.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
