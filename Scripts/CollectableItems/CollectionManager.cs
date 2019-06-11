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
        
        text_fragment = Fragment.GetComponent<Text>();
        text_ghostDoorMortise = GhostDoorMortise.GetComponent<Text>();
    }

    private Text text_fragment;

    private Text text_ghostDoorMortise;

    void Update()
    {
        if (gameController)
        {
            if (gameController == null)
            {
                Count_Fragment = 0;
                Count_GhostDoorMortise = 0;
            }
            else
            {
                Count_Fragment = gameController.Count_Fragment;
                Count_GhostDoorMortise = gameController.Count_GhostDoorMortise;
            }
            text_fragment.text = Count_Fragment.ToString();
            text_ghostDoorMortise.text = Count_GhostDoorMortise.ToString();
        }
    }
}
