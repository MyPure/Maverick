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
        player = player ?? GameObject.FindGameObjectWithTag("Player");
    }

    //使用的是触发器，也可以使用碰撞器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log(gameObject.name + "碰到玩家了");
            Instantiate(DeadUI);
        }
    }
}
