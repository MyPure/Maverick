using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public enum ItemCode
    {
        元神碎片,
        鬼门卯
    }

    //可收集物体的类型
    public ItemCode itemType;


    // Start is called before the first frame update
    void Start()
    {
        //确保有碰撞盒
        if (gameObject.GetComponent<BoxCollider2D>() == null)
            gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        //修改数据
        switch (itemType)
        {
            case ItemCode.元神碎片:
                GameData.Count_Fragment+=10;
                Debug.Log("当前元神碎片的数量是" + GameData.Count_Fragment);
                break;
            case ItemCode.鬼门卯:
                GameData.Count_GhostDoorMortise++;
                Debug.Log("当前鬼门卯的数量是" + GameData.Count_GhostDoorMortise);
                break;
            default:
                Debug.Log("收集到了奇怪的东西");
                break;
        }
        //显示到HUD上



        //播放音效
        yield return null;
    }
}
