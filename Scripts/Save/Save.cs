using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int passLevel;//一共过了几关
    public Save() { }
    public Save(bool clear)
    {
        if (clear)
        {
            passLevel = 0;
        }
    }
}
