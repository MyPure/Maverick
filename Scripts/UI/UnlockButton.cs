using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour
{
    public GameObject shenTu;
    public GameObject yuLei;
    public GameObject failUI;
    public AudioClip unLock;
    public GameController gameController;
    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void 解锁神荼()
    {
        int 碎片数 = 1, 鬼门卯数 = 10;
        if (gameController.Count_Fragment >= 碎片数 && gameController.Count_GhostDoorMortise >= 鬼门卯数 && !gameController.神荼)
        {
            gameController.Count_Fragment -= 碎片数;
            gameController.Count_GhostDoorMortise -= 鬼门卯数;
            gameController.神荼 = true;
            AudioSource.PlayClipAtPoint(unLock, GameObject.FindWithTag("MainCamera").transform.position);
            Instantiate(shenTu);
            gameController.SaveGame();
        }
        else
        {
            if (!gameController.神荼)
            {
                Instantiate(failUI);
            }
        }
    }
    public void 解锁郁垒()
    {
        int 碎片数 = 3, 鬼门卯数 = 30;
        if (gameController.Count_Fragment >= 碎片数 && gameController.Count_GhostDoorMortise >= 鬼门卯数 && !gameController.郁垒)
        {
            gameController.Count_Fragment -= 碎片数;
            gameController.Count_GhostDoorMortise -= 鬼门卯数;
            gameController.郁垒 = true;
            AudioSource.PlayClipAtPoint(unLock, GameObject.FindWithTag("MainCamera").transform.position);
            Instantiate(yuLei);
            gameController.SaveGame();
        }
        else
        {
            if (!gameController.郁垒)
            {
                Instantiate(failUI);
            }
        }
    }
}
