using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingGhost : MonoBehaviour
{
    //旋转速度
    public float rotateSpeed;

    //旋转轴
    public Vector3 rotateAxis;

    void Start()
    {
        rotateSpeed = 60f;
        rotateAxis = Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(gameObject.transform.localRotation.z) <= 0.5 && Mathf.Abs(gameObject.transform.localRotation.z) >= -0.5)
        {
            gameObject.transform.Rotate(rotateAxis * Time.deltaTime * rotateSpeed);
        }
        else
        {
            Debug.Log("到达边界的时间"+Time.time);
            rotateAxis = rotateAxis == Vector3.forward ? Vector3.back : Vector3.forward;
            gameObject.transform.Rotate(rotateAxis * Time.deltaTime * rotateSpeed);
        }
    }
}
