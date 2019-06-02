using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    /// <summary>
    /// 加载主页
    /// </summary>
    public void LoadHome()
    {
        SceneManager.LoadScene("HomeUI");
    }

    /// <summary>
    /// 再来一次(这部分代码未实现)
    /// </summary>
    public void TryAgain()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("再来一次");
    }

    /// <summary>
    /// 加载游戏
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// 离开游戏
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
