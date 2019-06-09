using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger : MonoBehaviour
{
    //速度
    public float speed = 8;

    //销毁时间
    public float destroyTime = 1;

    public Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aciton());//destroyTime时间后销毁本物体
    }

    //destroyTime时间后销毁本物体
    IEnumerator aciton()
    {
        rig.velocity = new Vector2(speed, 0);
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    //销毁所有碰到的敌人
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ENEMY")
        {
            Debug.Log("碰到敌人了");
            Destroy(collision.gameObject);
        }
    }
}
