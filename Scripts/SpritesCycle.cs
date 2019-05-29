using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpritesCycle : MonoBehaviour
{
    public Camera tcamera;
    public List<SpriteRenderer> sprites;
    public Vector2 factor;
    [Range(0,1)]public float offset;
    public void SetPosition(float position)
    {
        float totalWidth = 0;
        Vector3 l_position = new Vector3(-sprites[0].bounds.size.x / 2, 0, 0);
        for(int i = 0; i < sprites.Count; i++)
        {
            l_position.x += sprites[i].bounds.size.x / 2;
            sprites[i].transform.localPosition = l_position;
            totalWidth += sprites[i].bounds.size.x;
            l_position.x += sprites[i].bounds.size.x / 2;
        }
        float dx = position % totalWidth;
        for (int i = 0; i < sprites.Count; i++)
        {
            Vector3 d_position = sprites[i].transform.localPosition + Vector3.right * dx;
            Debug.Log(d_position);
            if (d_position.x < -totalWidth / sprites.Count)
            {
                d_position.x += totalWidth;
            }else if (d_position.x > totalWidth)
            {
                d_position.x -= totalWidth;
            }
            d_position.x -= offset * totalWidth;
            sprites[i].transform.localPosition = d_position;
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
        transform.position = tcamera.transform.position + new Vector3(0, 0, 10);
        SetPosition(tcamera.transform.position.x * factor.x);
    }
}
