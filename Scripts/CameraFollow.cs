using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;//要跟随的物体
    Vector2 preset;//初始偏移量
    void Start()
    {
        preset = transform.position - target.transform.position;
    }
    void Update()
    {
        Vector2 d = (Vector2)target.transform.position + preset;
        Vector3 destination = new Vector3(d.x, d.y, transform.position.z);
        Vector3 _destination = Vector3.MoveTowards(transform.position, new Vector3(destination.x, destination.y, transform.position.z), Mathf.Min(1, Mathf.Abs(((Vector3)destination - transform.position).magnitude) / 2.0f) * 8 * Time.deltaTime);
        transform.Translate(_destination - transform.position);
    }
}
