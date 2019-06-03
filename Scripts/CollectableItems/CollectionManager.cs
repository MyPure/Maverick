using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public int Count_Fragment, Count_GhostDoorMortise;
    public GameObject Fragment;
    public GameObject GhostDoorMortise;

    private void Start()
    {
        Count_Fragment = Count_GhostDoorMortise = 0;
        text_fragment = Fragment.GetComponent<Text>();
        text_ghostDoorMortise = GhostDoorMortise.GetComponent<Text>();
    }

    private Text text_fragment;

    private Text text_ghostDoorMortise;

    void Update()
    {
        text_fragment.text = Count_Fragment.ToString();
        text_ghostDoorMortise.text = Count_GhostDoorMortise.ToString();
    }
}
