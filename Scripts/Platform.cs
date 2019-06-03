using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Platform : MonoBehaviour , IComparable
{
    float existTime;//存在时间
    public static float startDropTime = 3.0f;//开始掉落时间
    public static float dropInterval = 0.5f;//掉落间隔
    public int order;//掉落次序，从0开始
    void Start()
    {
        existTime = 0;
    }
    void Update()
    {
        existTime += Time.deltaTime;
        if (existTime - startDropTime > dropInterval * order)
        {
            drop();
        }
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    [HideInInspector]
    public float velocity = 0;
    void drop()
    {
        GetComponent<Collider2D>().enabled = false;
        velocity += 10.0f * Time.deltaTime;
        transform.Translate(Vector3.down * velocity * Time.deltaTime);
    }

    public int CompareTo(object obj)
    {
        float result = transform.position.x - ((Platform)obj).transform.position.x;
        return result < 0 ? -1 : result > 0 ? 1 : 0;
    }
}
