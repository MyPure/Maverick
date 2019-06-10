using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本附在组图的父物体上
[ExecuteInEditMode]
public class SpritesCycle : MonoBehaviour
{
    public Camera tcamera;//跟随的摄像机
    public List<SpriteRenderer> sprites;//要连接的一组贴图
    public Vector2 factor;//移动因子，正值与摄像机同向移动，负值反向移动。绝对值越大，移动越快
    [Range(0,1)]public float offset;//初始偏移，一般调整至摄像机在组图正中间
    /// <summary>
    /// 给一个偏移量，使得组图能够随着相机的移动而移动
    /// </summary>
    /// <param name="position">偏移量</param>
    public void SetPosition(float position)
    {
        float totalWidth = 0;//组图总宽度
        Vector3 l_position = new Vector3(-sprites[0].bounds.size.x / 2, 0, 0);
        for(int i = 0; i < sprites.Count; i++)
        {
            l_position.x += sprites[i].bounds.size.x / 2;
            sprites[i].transform.localPosition = new Vector3(l_position.x, sprites[i].transform.localPosition.y, 0);
            totalWidth += sprites[i].bounds.size.x;
            l_position.x += sprites[i].bounds.size.x / 2;
        }
        //给组图的每一张图赋上初始值，并且计算总宽
        for (int i = 0; i < sprites.Count; i++)
        {
            Vector3 d_position = sprites[i].transform.localPosition + Vector3.right * (position % totalWidth);
            //d_position:在当前偏移量下即将移动到的位置
            if (d_position.x < -totalWidth / sprites.Count)
            {
                d_position.x += totalWidth;
            }else if (d_position.x > totalWidth)
            {
                d_position.x -= totalWidth;
            }
            //是否超出视野判定
            d_position.x -= offset * totalWidth;
            //初始偏移
            sprites[i].transform.localPosition = d_position;//新位置
        }
    }
    private void Start()
    {
        if (!tcamera)
        {
            if (Camera.main)
            {
                tcamera = Camera.main;
            }
        }
    }
    private void Update()
    {
        transform.position = tcamera.transform.position + new Vector3(0, 0, -tcamera.transform.position.z);
        SetPosition(tcamera.transform.position.x * factor.x);//以摄像机的位置为基准值给函数传递偏移量
    }
}