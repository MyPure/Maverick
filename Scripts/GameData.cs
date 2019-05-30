using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常驻内存的游戏数据，注意并不是monobehaviour
/// </summary>
public class GameData
{
    public enum GameResult
    {
        Win,
        Lose,
        TimeOut,
        NotSure
    }

    //游戏结果
    public static GameResult gameResult = GameResult.NotSure;

    //元神碎片个数
    public static int Count_Fragment = 0;

    //鬼门卯
    public static int Count_GhostDoorMortise = 0;
}
