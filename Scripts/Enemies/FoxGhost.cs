using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxGhost : MonoBehaviour
{
    public float moveSpeed;

    private GameObject player;

    private SpriteRenderer sprite;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //和玩家的距离小于10的时候向左移动
        //if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < 10)
        //{
            //移动
            gameObject.transform.position += new Vector3(-moveSpeed*Time.deltaTime,0,0);
        //}

        //修改透明度
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, getAlpha(Time.time));


    }

    //一个周期为4的周期函数
    float getAlpha(float t)
    {
        t %= 4;
        float alpha = 0;
        if(t >= 0 && t <= 2)
            alpha = -t + 0.5f;
        else
            alpha =  t - 3.5f;
        
        return Mathf.Clamp(alpha,0,1);
    }
}
