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
    }
    void Update()
    {
        Fragment.GetComponent<Text>().text = Count_Fragment.ToString();
        GhostDoorMortise.GetComponent<Text>().text = Count_GhostDoorMortise.ToString();
    }
}
