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

    //public CollectionManager collectionManager;

    public GameController gameController;

    void Start()
    {
        //确保有碰撞盒
        if (gameObject.GetComponent<BoxCollider2D>() == null)
            gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        if (!gameController)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
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
                if (gameController)
                    gameController.Count_Fragment ++;
                break;
            case ItemCode.鬼门卯:
                if (gameController)
                    gameController.Count_GhostDoorMortise++;
                break;
            default:
                break;
        }

        //显示到HUD上
        
        //播放音效
        yield return null;

        //销毁物体
        Destroy(gameObject);
    }
}
