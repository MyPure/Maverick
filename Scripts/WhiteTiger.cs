using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger : MonoBehaviour
{
    //速度
    public float speed = 8;

    //销毁时间
    public float destroyTime = 1;

    private float existTime = 0;
    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        existTime += Time.deltaTime;
        if (existTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    //销毁所有碰到的敌人
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ENEMY")
        {
            //Debug.Log("碰到敌人了");
            Destroy(collision.gameObject);
        }
    }
}
