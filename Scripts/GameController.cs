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

        //加载下一个关卡
        if(SceneManager.GetSceneByName("Level " + nowLevel) != null)
            SceneManager.LoadScene("Level " + nowLevel);
        else
        {
            //在此处编写通关逻辑
            Debug.Log("通关了");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
