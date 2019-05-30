using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;//要跟随的物体
    float preset;//初始偏移量
    void Start()
    {
        preset = transform.position.x - target.transform.position.x;
    }
    void LateUpdate()
    {
        float destination = target.transform.position.x + preset;
        Vector3 _destination = Vector3.MoveTowards(transform.position, new Vector3(destination, transform.position.y, transform.position.z), Mathf.Min(1, Mathf.Abs(destination - transform.position.x) / 2.0f) * 4 * Time.deltaTime);
        transform.Translate(_destination - transform.position);
    }
}
