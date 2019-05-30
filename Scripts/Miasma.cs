using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瘴气
/// </summary>
public class Miasma : MonoBehaviour
{
    //玩家
    public GameObject player;

    //可见的距离
    public float visibleDistance = 10;

    //刚开始鬼火不显示
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = player ?? GameObject.FindGameObjectWithTag("Player");

        gameObject.transform.position += new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.01f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        //和玩家的距离小于4的时候可以显示
        if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < visibleDistance)
            spriteRenderer.enabled = true;
        else
            spriteRenderer.enabled = false;

    }
}
