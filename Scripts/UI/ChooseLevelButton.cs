using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseLevelButton : MonoBehaviour
{
    public int level;
    public GameController gameController;//在ChooseLevel中赋值
    public Text text;
    public Image image;
    public GameObject unlockFailUI;
    /// <summary>
    /// 判断该按钮能否点击
    /// </summary>
    public void Check()
    {
        if(gameController.passLevel + 1 < level)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }

        if (gameController.unlocklevel >= level)
        {
            if (text)
            {
                text.text = "已解锁!";
            }
            if (image)
            {
                image.enabled = false;
            }
        }
        else
        {
            if (text)
            {
                if (level == 2)
                {
                    text.text = "解锁消耗  10\n需解锁神荼";
                }
                else if (level == 3)
                {
                    text.text = "解锁消耗  10\n需解锁郁垒";
                }
                else
                {
                    text.text = "解锁消耗  10";
                }
            }
            if (image)
            {
                image.enabled = true;
            }
        }
    }

    public void Unlock()
    {
        if (level == 2)
        {
            if (gameController.Count_GhostDoorMortise >= 10 && gameController.神荼)
            {
                gameController.Count_GhostDoorMortise -= 10;
                gameController.unlocklevel = level;
                gameController.SaveGame();
            }
            else
            {
                Instantiate(unlockFailUI);
            }
        }
        else if (level == 3)
        {
            if (gameController.Count_GhostDoorMortise >= 10 && gameController.郁垒)
            {
                gameController.Count_GhostDoorMortise -= 10;
                gameController.unlocklevel = level;
                gameController.SaveGame();
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
                gameController.unlocklevel = level;
                gameController.SaveGame();
            }
            else
            {
                Instantiate(unlockFailUI);
            }
        }
    }

    public void CheckAndLoadSence(int level)
    {
        if (gameController.unlocklevel < level)
        {
            Unlock();
            Check();
        }
        else
        {
            gameController.LoadLevel(level);
        }
    }
}
