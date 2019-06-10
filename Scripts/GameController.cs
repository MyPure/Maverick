using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public GameObject chooseLevelUI;
    public int nowLevel = 0;//当前在第几关
    public int passLevel = 0;//一共过了几关
    public int unlocklevel = 0;//解锁了几关
    public bool 神荼, 郁垒;//是否拥有此技能
    public int Count_Fragment = 0, Count_GhostDoorMortise = 0;//收集物品数
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
        Player.whiteTigerCount = 0;
        nowLevel++;
        LoadLevel(nowLevel);
    }

    /// <summary>
    /// 加载特定关卡
    /// </summary>
    public void LoadLevel(int i)
    {
        Player.whiteTigerCount = 0;
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
        StartCoroutine(BTC());
    }

    IEnumerator BTC()
    {
        LoadHome();
        yield return null;
        Instantiate(chooseLevelUI);
    }


    //创建存档类
    private Save CreatSave()
    {
        Save save = new Save();
        save.神荼 = 神荼;
        save.郁垒 = 郁垒;
        save.Count_Fragment = Count_Fragment;
        save.Count_GhostDoorMortise = Count_GhostDoorMortise;
        save.passLevel = passLevel;
        save.unlockLevel = unlocklevel;
        return save;
    }

    //保存至文件
    public void SaveGame()
    {
        Save save = CreatSave();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save/save.dat");
        bf.Serialize(file, save);
        file.Close();
    }

    public void ClearSave()
    {
        Save save = new Save(true);//生成空存档类
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save/save.dat");
        bf.Serialize(file, save);
        file.Close();

        LoadSave(save);
    }

    private void Awake()
    {
        if (first)
        {
            DontDestroyOnLoad(gameObject);
            first = false;
        }

        //读取存档
        if (File.Exists(Application.persistentDataPath + "/save/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save/save.dat", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            LoadSave(save);
        }
        else
        {
            Save save = new Save(true);
            BinaryFormatter bf = new BinaryFormatter();
            if(!Directory.Exists(Application.persistentDataPath + "/save")){
                Directory.CreateDirectory(Application.persistentDataPath + "/save");
            }
            FileStream file = File.Create(Application.persistentDataPath + "/save/save.dat");
            bf.Serialize(file, save);
            file.Close();
            Debug.Log("未找到存档文件，已创建空存档");
        }
    }

    /// <summary>
    /// 把存档内容保存到Gamecontroller中
    /// </summary>
    /// <param name="save"></param>
    private void LoadSave(Save save)
    {
        Count_GhostDoorMortise = save.Count_GhostDoorMortise;
        Count_Fragment = save.Count_Fragment;
        passLevel = save.passLevel;
        unlocklevel = save.unlockLevel;
        神荼 = save.神荼;
        郁垒 = save.郁垒;
    }
}
