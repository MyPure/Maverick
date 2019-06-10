using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerState currentState;//当前状态
    public List<PlayerState> playerStates;//所有状态
    public float height = 2.0f;//角色高
    public float width = 1.0f;//角色宽
    public float speed = 4.0f;//角色移动速度
    public float G = 20.0f;//角色受到的重力
    public int rayY = 3;//竖直方向发射的射线数量
    public int rayX = 5;//水平方向发射的射线数量
    public CollectionManager collectionManager;
    public GameObject DeadUI;
    public GameObject whiteTiger;

    public static int whiteTigerCount;//使用白虎的次数

    /// <summary>
    /// 状态在第一次运行或切换时调用
    /// </summary>
    private void Start()
    {
        foreach(PlayerState state in playerStates)
        {
            //进行状态默认设置
            state.player = this;
            state.SetType();
        }
        //收集物品管理
        if (!collectionManager)
        {
            collectionManager = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
        }
        //设置默认状态
        if (!currentState)
        {
            currentState = GetComponent<Stand>();
        }
        currentState.StateStart();
    }
    bool death;//是否死亡
    /// <summary>
    /// 状态在每帧更新时调用
    /// </summary>
    private void Update()
    {
        currentState.StateUpdate();

        if(!death && transform.position.y < -6)
        {
            Die();
        }
        checkWhiteTiger();
    }

    void checkWhiteTiger()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (whiteTigerCount++ < 3 && GameObject.Find("GameController").GetComponent<GameController>().郁垒)
            {
                Transform player = GameObject.Find("Player").transform;
                Instantiate(whiteTiger, player.position - new Vector3(0, 0.5f, 0), player.rotation);
            }
            else
            {
#if UNITY_EDITOR
                Transform player = GameObject.Find("Player").transform;
                Instantiate(whiteTiger, player.position - new Vector3(0, 0.5f, 0), player.rotation);
#endif
            }
        }

    }

    public void Die()
    {
        death = true;
        Instantiate(DeadUI);
        List<Component> comList = new List<Component>();
        foreach (var component in gameObject.GetComponents<Component>())
        {
            if (!(component is Transform))
                comList.Add(component);
        }
        foreach (Component item in comList)
        {
            Destroy(item);
        }
    }
}
