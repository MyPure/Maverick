﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public GameObject chooseLevelUI;
    public bool pause = false;
    public int nowLevel = 0;//当前在第几关
    public int passLevel = 0;//一共过了几关
    public int unlocklevel = 0;//解锁了几关
    public bool 神荼, 郁垒;//是否拥有此技能
    public int Count_GhostDoorMortise = 0 ,Count_Fragment = 0 ;//收集物品数
    public int levelTotalNum;// 关卡总数
    //public List<int> fragment_nums;//各个关卡应该出现的碎片数
    private static bool first = true;//首次启动
    public List<bool>[] order;

    private void Start()
    {
        levelTotalNum = 3;
        //fragment_nums = new List<int>();
    }

    public GameObject pauseUI;
    private void Update()
    {
        if (nowLevel >= 1 && nowLevel <= 3)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pause==false)
            {
                pause = true;
                Instantiate(pauseUI);
            }
            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
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
            StartCoroutine( ReadOrder(nowLevel) );
            //StartCoroutine( LoadAndDestroyExtraFragment() );
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
        save.order = order;

        //var v = GameObject.FindGameObjectsWithTag("Fragment");

        //if (fragment_nums.Count >= nowLevel)//不是第一次玩该关卡
        //{
        //    fragment_nums[nowLevel-1] = v.Length;
        //}
        //else//是第一次
        //{
        //    fragment_nums.Add(v.Length);
        //}
        //save.fragment_nums = fragment_nums;
        //Debug.Log("创建存档成功：fragment_nums的数据有" + save.fragment_nums.Count + "个");
        //Debug.Log("碎片值为" + fragment_nums[fragment_nums.Count - 1]);

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
        Save save = new Save(true,levelTotalNum);//生成空存档类
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
            Save save = new Save(true, levelTotalNum);
            BinaryFormatter bf = new BinaryFormatter();
            if(!Directory.Exists(Application.persistentDataPath + "/save")){
                Directory.CreateDirectory(Application.persistentDataPath + "/save");
            }
            FileStream file = File.Create(Application.persistentDataPath + "/save/save.dat");
            bf.Serialize(file, save);
            file.Close();

            LoadSave(save);
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
        //fragment_nums = save.fragment_nums;        
        //Debug.Log("加载存档成功：fragment_nums的数据有" + fragment_nums.Count + "个");
        order = save.order;
    }

    private IEnumerator ReadOrder(int level)
    {
        yield return null;
        GameObject[] framentList = GameObject.FindGameObjectsWithTag("Fragment");
        if (level > 0)
        {
            if (order[level - 1][0])
            {
                for (int i = 0; i < framentList.Length; i++)
                {
                    order[level - 1].Add(true);
                }
                order[level - 1][0] = false;
            }
            else
            {
                for (int i = 0; i < framentList.Length; i++)
                {
                    framentList[i].GetComponent<SpriteRenderer>().enabled = order[level - 1][i + 1];
                    framentList[i].GetComponent<BoxCollider2D>().enabled = order[level - 1][i + 1];
                }
            }
        }
        //foreach (List<bool> b in order)
        //{
            //foreach (bool _b in b)
            //{
            //    Debug.Log(_b);
            //}
        //}
    }

    public void SaveOrder()
    {
        GameObject[] framentList = GameObject.FindGameObjectsWithTag("Fragment");
        for (int i = 0; i < framentList.Length; i++)
        {
            order[nowLevel - 1][i + 1] = framentList[i].GetComponent<SpriteRenderer>().enabled;
        }
    }
    /// <summary>
    /// 销毁多余碎片
    /// </summary>
    //private IEnumerator LoadAndDestroyExtraFragment()
    //{
    //    SceneManager.LoadScene("Level " + nowLevel);
    //    yield return new WaitForSeconds(0.1f);

    //    if(fragment_nums.Count < nowLevel)
    //    {
    //        ;//如果是第一次进入该关卡，先不用加载存档
    //    }
    //    else if (fragment_nums.Count != 0)
    //    {
	   //     GameObject[] obj_list = GameObject.FindGameObjectsWithTag("Fragment");
	   //     HashSet<int> destroy_indexs = new HashSet<int>();
	   //     int total_nums = obj_list.Length;
	   //     int destoy_nums = total_nums - fragment_nums[nowLevel - 1];
	   //     System.Random random = new System.Random();

	   //     Debug.Log("total_nums：" + total_nums);
	   //     Debug.Log("destoy_nums：" + destoy_nums);
	   //     Debug.Log("fragment_nums[nowLevel - 1]：" + fragment_nums[nowLevel - 1]);

	   //     while (destoy_nums > 0)
	   //     {
	   //         int index = random.Next() % (destoy_nums+1);
	   //         if (destroy_indexs.Contains(index))
	   //         {
	   //             ;
	   //         }
	   //         else
	   //         {
	   //             Destroy(obj_list[index]);
	   //             destoy_nums--;
	   //         }
	   //     }        	
    //    }
    //}
}
