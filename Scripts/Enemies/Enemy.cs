using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 凡是具有“玩家接触即死”性质的敌人均应该挂载该脚本
/// </summary>
public class Enemy : MonoBehaviour
{
    //玩家对象
    public GameObject player;

    //死后的界面
    public GameObject DeadUI;

    private void Start()
    {
        //查找玩家
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        //确保有碰撞盒(部分敌人碰撞体不是方形）
        //if (gameObject.GetComponent<BoxCollider2D>() == null)
        //    gameObject.AddComponent<BoxCollider2D>();
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.GetComponent<Player>().Die();
        }
    }
}
