using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxGhost : MonoBehaviour
{
    public float moveSpeed;

    private GameObject player;

    private SpriteRenderer sprite;

    private Rigidbody2D rig;

    public Sprite hide, move;

    public float hideDistance = 2.0f;
    public float showDistance = 2.0f;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        sprite = GetComponent<SpriteRenderer>();

        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //和玩家的距离小于10的时候向左移动
        if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < 10)
        {
            //移动
            gameObject.transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }

        //修改透明度
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, getAlpha(Time.time));

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    //根据移动距离计算隐身 
    //(-----a------c------b-----) ：一个周期（隐身和现形不一定一样长）
    //  隐身|    现形     | 隐身
    float getAlpha(float t)
    {
        float td = (hideDistance + showDistance) / moveSpeed;//一个周期的总时间
        t %= td;
        float a = hideDistance / moveSpeed / 2, b = td - hideDistance / moveSpeed / 2;
        float c = td / 2;
        float result;
        if (t <= a || t>= b)
        {
            result = 0;
            sprite.sprite = hide;
        }
        else if(t > a && t < c)
        {
            result = (t - a) / (c - a);
            sprite.sprite = hide;
        }
        else
        {
            result = (b - t) / (b - c);
            sprite.sprite = move;
        }
        return result;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player")
        {
            Vector3 displacement = gameObject.transform.position - collision.transform.position;
            if (Mathf.Abs(displacement.y) < 0.5f)
                moveSpeed = -moveSpeed;
        }
    }
}
