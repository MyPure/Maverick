using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public List<ChooseLevelButton> buttons;

    public Text text_Count_Fragment;
    public Text text_Count_GhostDoorMortise;
    public void Check()
    {
        foreach(ChooseLevelButton button in buttons)
        {
            if(!button.gameController)
            button.gameController = GameObject.Find("GameController").GetComponent<GameController>();
            button.Check();
        }
        text_Count_Fragment.text = GameObject.Find("GameController").GetComponent<GameController>().Count_Fragment.ToString();
        text_Count_GhostDoorMortise.text = GameObject.Find("GameController").GetComponent<GameController>().Count_GhostDoorMortise.ToString();
    }

    //
    private void Awake()
    {
        Check();
    }
}
