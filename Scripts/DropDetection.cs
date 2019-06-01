using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDetection : MonoBehaviour
{
    //死亡后出现的界面
    public GameObject DeadUI;


    //实例化后的标记
    private bool flag = true;

    // Update is called once per frame
    void Update()
    {
        if(flag && gameObject.transform.position.y < -5)
        {
            Instantiate(DeadUI);

            //移除组件
            RemoveComponent();

            flag = false;
        }
    }


    /// <summary>
    /// 移除player身上所有的组件
    /// </summary>
    public void RemoveComponent()
    {
        List<Component> comList = new List<Component>();
        foreach (var component in gameObject.GetComponents<Component>())
        {
            if (component is Transform || component is DropDetection)
                ;
            else
                comList.Add(component);
        }
        foreach (Component item in comList)
        {
            Destroy(item);
        }
    }
}
