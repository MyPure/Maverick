using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public List<ChooseLevelButton> buttons;
    public void Check()
    {
        foreach(ChooseLevelButton button in buttons)
        {
            if(!button.gameController)
            button.gameController = GameObject.Find("GameController").GetComponent<GameController>();
            button.Check();
        }
    }
    private void Awake()
    {
        Check();
    }
}
