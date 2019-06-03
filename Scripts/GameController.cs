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

        //加载下一个关卡
        if(SceneManager.GetSceneByName("Level " + nowLevel) != null)
            SceneManager.LoadScene("Level " + nowLevel);
        else
        {
            //在此处编写通关逻辑
            Debug.Log("通关了或找不到下一个关卡");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
