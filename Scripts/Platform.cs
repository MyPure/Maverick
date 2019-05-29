using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    float existTime;//存在时间
    public static float startDropTime = 3.0f;//开始掉落时间
    public float dropInterval = 0.5f;//掉落间隔
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
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    float velocity = 0;
    void drop()
    {
        GetComponent<Collider2D>().enabled = false;
        velocity += 10.0f * Time.deltaTime;
        transform.Translate(Vector3.down * velocity * Time.deltaTime);
    }
}
