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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 加载游戏
    /// </summary>
    public void LoadGame1()
    {
        GameObject.Find("GameController").GetComponent<GameController>().LoadLevel(1);
    }

    public void LoadGame2()
    {
        GameObject.Find("GameController").GetComponent<GameController>().LoadLevel(2);
    }
    /// <summary>
    /// 离开游戏
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 实例化对象（一般是UI）
    /// </summary>
    public void InstantiateGameObject(GameObject obj)
    {
        Instantiate(obj);
    }

    /// <summary>
    /// 销毁该控件所在的Canvas
    /// 断言：至少一个他的祖先有canvas组件
    /// </summary>
    public void DestroyNowCanvas()
    {
        //从关系树往上回溯，直到找到第一个Canvas
        GameObject nowObj = gameObject.transform.parent.gameObject;
        while (nowObj.GetComponent<Canvas>() == null)
            nowObj = nowObj.transform.parent.gameObject;

        Destroy(gameObject.transform.parent.gameObject);
    }

}
