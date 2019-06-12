using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingGhost : MonoBehaviour
{
    //旋转速度
    public float rotateSpeed = 1.45f;

    //旋转角度
    public float angle;

    public List<Sprite> sprites;

    //
    float existTime;

    private void Start()
    {
        existTime = 0;
    }
    bool play = true;
    void Update()
    {
        existTime += Time.deltaTime;
        float currentAngle = angle * Mathf.Sin(rotateSpeed * existTime);
        if(currentAngle >= -1 && currentAngle <= 1)
        {
            if (play)
            {
                GetComponent<AudioSource>().Play();
            }
            play = false;
        }
        else
        {
            play = true;
        }
        transform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }
}
