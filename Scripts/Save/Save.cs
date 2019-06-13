using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int passLevel;//一共过了几关
    public int unlockLevel;//解锁了几关
    public int Count_Fragment, Count_GhostDoorMortise;//该物品的数目
    public bool 神荼, 郁垒;//是否拥有此技能
    //public List<int> fragment_nums;//关卡对应的碎片数
    public List<bool>[] order;


    public Save() { }
    public Save(bool clear,int levelTotalNum)
    {
        if (clear)
        {
            passLevel = 0;
            unlockLevel = 0;
            Count_Fragment = 0;
            Count_GhostDoorMortise = 0;
            神荼 = 郁垒 = false;
            //fragment_nums = new List<int>();
            order = new List<bool>[levelTotalNum];
            for(int i = 0; i < levelTotalNum; i++)
            {
                order[i] = new List<bool>();
                order[i].Add(true);
            }
        }
    }

    //this <= other
    //public void DeepCopy(Save other)
    //{
    //    passLevel = other.passLevel;
    //    unlockLevel = other.unlockLevel;
    //    Count_Fragment = other.Count_Fragment;
    //    Count_GhostDoorMortise = other.Count_GhostDoorMortise;
    //    神荼 = other.神荼;
    //    郁垒 = other.郁垒;
    //}
}
