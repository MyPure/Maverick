using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    public GameController gameController;
    public Text text;
    public Image image;
    public GameObject unlockFailUI;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (gameController.unlocklevel >= gameController.nowLevel + 1)
        {
            if (text)
            {
                text.text = "下一关已解锁!";
            }
            if (image)
            {
                image.enabled = false;
            }
        }
        else
        {
            if (gameController.nowLevel + 1 == 2)
            {
                text.text = "解锁消耗  10\n需解锁神荼";
            }
            else if (gameController.nowLevel + 1 == 3)
            {
                text.text = "解锁消耗  10\n需解锁郁垒";
            }
            else
            {
                text.text = "解锁消耗  10\n";
            }
            if (image)
            {
                image.enabled = true;
            }
        }
    }

    public void CheckAndLoadSence()
    {
        if (gameController.unlocklevel < gameController.nowLevel + 1)
        {
            if (gameController.nowLevel + 1 == 2)
            {
                if (gameController.Count_GhostDoorMortise >= 10 && gameController.神荼)
                {
                    gameController.Count_GhostDoorMortise -= 10;
                    gameController.unlocklevel = gameController.nowLevel + 1;
                    gameController.SaveGame();
                    gameController.LoadNext();
                }
                else
                {
                    Instantiate(unlockFailUI);
                }
            }         
            else if(gameController.nowLevel + 1 == 3)
            {
                if (gameController.Count_GhostDoorMortise >= 10 && gameController.郁垒)
                {
                    gameController.Count_GhostDoorMortise -= 10;
                    gameController.unlocklevel = gameController.nowLevel + 1;
                    gameController.SaveGame();
                    gameController.LoadNext();
                }
                else
                {
                    Instantiate(unlockFailUI);
                }
            }
            else
            {
                if (gameController.Count_GhostDoorMortise >= 10)
                {
                    gameController.Count_GhostDoorMortise -= 10;
                    gameController.unlocklevel = gameController.nowLevel + 1;
                    gameController.SaveGame();
                    gameController.LoadNext();
                }
                else
                {
                    Instantiate(unlockFailUI);
                }
            }
        }
        else
        {
            gameController.LoadNext();
        }
    }
}
