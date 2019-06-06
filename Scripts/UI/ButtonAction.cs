using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    private GameController gameController;
    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// 加载主页
    /// </summary>
    public void LoadHome()
    {
        gameController.LoadHome();
    }

    public void BackToChooseLevel()
    {
        gameController.BackToChooseLevel();
    }

    /// <summary>
    /// 再来一次(这部分代码未实现)
    /// </summary>
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(int level)
    {
        gameController.LoadLevel(level);
    }
    public void LoadNext()
    {
        gameController.LoadNext();
    }

}
