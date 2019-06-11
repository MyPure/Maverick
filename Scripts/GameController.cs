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
    public Dictionary<int, List<Transform>> mortisesInfo;//卯的信息
    public int levelTotalNum;// 关卡总数
    private static bool first = true;//首次启动

    private void Start()
    {
        mortisesInfo = new Dictionary<int, List<Transform>>();
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
    public void LoadLevel(int level)
    {
        Player.whiteTigerCount = 0;
        //更新当前关卡
        nowLevel = level;

        //判断是否完全通关
        if(nowLevel > levelTotalNum)
            SceneManager.LoadScene("PASS_ALL");
        //加载下一个关卡
        else
        {
            SceneManager.LoadScene("Level " + nowLevel);
            Debug.Log("要加载的关卡是" + nowLevel + " mortisesInfo的长度是" + mortisesInfo.Count);
            if (mortisesInfo.ContainsKey(nowLevel))
            {
                //如果不是第一次加载该场景，先销毁当前所有卯，再从存档中读出原来“存活”的卯
                List<Transform> list = new List<Transform>();
                foreach (var v in GameObject.FindGameObjectsWithTag("MORTISE"))
                    list.Add(v.transform);
                for (int i = 0; i < list.Count; i++)
                {
                    Destroy(list[i].gameObject);
                }
            }
        }
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

        #region 更新存档中的mortises

        List<Transform> mortises = new List<Transform>();
        foreach(var v in GameObject.FindGameObjectsWithTag("MORTISE"))
            mortises.Add(v.transform);
        
        if (mortisesInfo.ContainsKey(nowLevel))
            mortisesInfo[nowLevel] = mortises;
        else
            mortisesInfo.Add(nowLevel,mortises);
        save.mortisesInfo = mortisesInfo;

        #endregion

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
        mortisesInfo = save.mortisesInfo;
    }
}
