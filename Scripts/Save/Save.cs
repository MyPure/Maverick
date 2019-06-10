using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int passLevel;//一共过了几关
    public int Count_Fragment, Count_GhostDoorMortise;

    public Save() { }
    public Save(bool clear)
    {
        if (clear)
        {
            passLevel = 0;
        }
    }

    //this <= other
    public void DeepCopy(Save other)
    {
        passLevel = other.passLevel;
        Count_Fragment = other.Count_Fragment;
        Count_GhostDoorMortise = other.Count_GhostDoorMortise;
    }
}
