using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    public GameController gameController;
    public Text text;
    public Image image;

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
            if (text)
            {
                text.text = "消耗  10";
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
            if (gameController.Count_GhostDoorMortise >= 10)
            {
                gameController.Count_GhostDoorMortise -= 10;
                gameController.unlocklevel = gameController.nowLevel + 1;
                gameController.SaveGame();
                gameController.LoadNext();
            }
            else
            {

            }
        }
        else
        {
            gameController.LoadNext();
        }
    }
}
