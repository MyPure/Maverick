using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// 当前关卡数
    /// </summary>
    public int nowLevel = 0;

    /// <summary>
    /// 关卡总数
    /// </summary>
    public int levelTotalNum;

    private void Start()
    {
        levelTotalNum = 2;
    }

    /// <summary>
    /// 加载下一个关卡
    /// </summary>
    public void LoadNext()
    {
        nowLevel++;
        LoadLevel(nowLevel);
    }

    /// <summary>
    /// 加载特定关卡
    /// </summary>
    public void LoadLevel(int i)
    {
        //更新当前关卡
        nowLevel = i;

        //判断是否完全通关
        if(nowLevel > levelTotalNum)
            SceneManager.LoadScene("PASS_ALL");
        //加载下一个关卡
        else
            SceneManager.LoadScene("Level " + nowLevel);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
