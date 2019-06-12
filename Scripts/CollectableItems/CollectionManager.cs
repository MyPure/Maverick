using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public int Count_Fragment, Count_GhostDoorMortise;
    public GameObject Fragment;
    public GameObject GhostDoorMortise;
    public GameController gameController;

    private void Start()
    {
        //Count_Fragment = Count_GhostDoorMortise = 0;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Count_Fragment = Count_GhostDoorMortise = 0;
        text_fragment = Fragment.GetComponent<Text>();
        text_ghostDoorMortise = GhostDoorMortise.GetComponent<Text>();
    }

    private Text text_fragment;

    private Text text_ghostDoorMortise;

    void Update()
    {
        if (!gameController)
        {
            text_fragment.text = "0";
            text_ghostDoorMortise.text = Count_GhostDoorMortise.ToString();
        }
        else
        {
            text_fragment.text = gameController.Count_Fragment.ToString();
            text_ghostDoorMortise.text = (gameController.Count_GhostDoorMortise + Count_GhostDoorMortise).ToString();
        }
    }

    public void Pass()
    {
        gameController.Count_GhostDoorMortise += Count_GhostDoorMortise;
        Count_GhostDoorMortise = 0;
    }
}
