using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;//要跟随的物体
    Vector2 preset;//初始偏移量
    float currentY;
    void Start()
    {
        preset = transform.position - target.transform.position;
        currentY = preset.y;
    }
    void Update()
    {   
        Vector2 d = (Vector2)target.transform.position + preset;
        if (target.GetComponent<Player>().currentState is Stand)
        {
            currentY = d.y;
        }
        else if (target.GetComponent<Player>().currentState is Climb)
        {
            currentY = d.y;
        }
        if (d.y - currentY >= 3)
        {
            currentY += 3;
        }
        else if (d.y - currentY <= -3)
        {
            currentY -= 3;
        }
        
        Vector3 destination = new Vector3(d.x, currentY, transform.position.z);
        Vector3 _destination = Vector3.MoveTowards(transform.position, new Vector3(destination.x, destination.y, transform.position.z), Mathf.Min(1, Mathf.Abs((destination - transform.position).magnitude) / 1.0f) * 4 * Time.deltaTime);
        transform.Translate(_destination - transform.position);
    }
}
