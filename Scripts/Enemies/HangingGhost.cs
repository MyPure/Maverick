using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingGhost : MonoBehaviour
{
    //旋转速度
    public float rotateSpeed = 1.45f;

    //旋转角度
    public float angle;

    //
    float existTime;

    private void Start()
    {
        existTime = 0;
    }
    void Update()
    {
        existTime += Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0, angle * Mathf.Sin(rotateSpeed * existTime));
    }
}
