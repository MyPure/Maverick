using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public GameObject failUI;
    public GameObject successUI;
    public AudioClip unLock;
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
        gameController.LoadLevel(gameController.nowLevel);
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

    public void ClearSave()
    {
        gameController.ClearSave();
    }

    public void SaveGame()
    {
        gameController.SaveGame();
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

    public void 解锁神荼()
    {
        int 碎片数 = 1, 鬼门卯数 = 10;
        if (gameController.Count_Fragment >= 碎片数 && gameController.Count_GhostDoorMortise >= 鬼门卯数)
        {
            gameController.Count_Fragment -= 碎片数;
            gameController.Count_GhostDoorMortise -= 鬼门卯数;
            gameController.神荼 = true;
            AudioSource.PlayClipAtPoint(unLock, GameObject.FindWithTag("MainCamera").transform.position);
            InstantiateGameObject(successUI);
        }
        else
            InstantiateGameObject(failUI);
    }
    public void 解锁郁垒()
    {
        int 碎片数 = 3, 鬼门卯数 = 30;
        if (gameController.Count_Fragment >= 碎片数 && gameController.Count_GhostDoorMortise >= 鬼门卯数)
        {
            gameController.Count_Fragment -= 碎片数;
            gameController.Count_GhostDoorMortise -= 鬼门卯数;
            gameController.郁垒 = true;
            AudioSource.PlayClipAtPoint(unLock, GameObject.FindWithTag("MainCamera").transform.position);
            InstantiateGameObject(successUI);
        }
        else
            InstantiateGameObject(failUI);
    }
}
