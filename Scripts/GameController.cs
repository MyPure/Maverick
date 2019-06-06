using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject chooseLevelUI;
    public int nowLevel = 0;//当前在第几关
    public int passLevel = 0;//一共过了几关
    public int levelTotalNum;// 关卡总数
    private static bool first = true;//首次启动

    private void Start()
    {
        levelTotalNum = 3;
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

    public void LoadHome()
    {
        nowLevel = 0;
        SceneManager.LoadScene("HomeUI");
    }

    public void BackToChooseLevel()
    {
        LoadHome();
        Instantiate(chooseLevelUI);
    }

    private Save CreatSave()
    {
        Save save = new Save();
        save.passLevel = passLevel;
        return save;
    }
    private void Awake()
    {
        if (first)
        {
            DontDestroyOnLoad(gameObject);
            first = false;
        }
    }
}
