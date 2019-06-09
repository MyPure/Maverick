using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseLevelButton : MonoBehaviour
{
    public int level;
    public GameController gameController;//在ChooseLevel中赋值

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
    }
}
