using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterGhost : MonoBehaviour
{
    string state;
    float existTime = 0;
    Vector3 up;
    Vector3 down;
    void Start()
    {
        up = down + Vector3.up * 3.5f;
        down = transform.position;
        existTime = 0;
    }

    void Update()
    {
        existTime += Time.deltaTime;
        if ((int)existTime % 2 == 0)
        {
            state = "up";
        }
        else
        {
            state = "down";
        }
        if (state == "up" && transform.position != up)
        {
            Vector3 d = Vector3.MoveTowards(transform.position, up, 10f * Time.deltaTime);
            transform.Translate(d - transform.position, Space.Self);
        }
        else if (state == "down" && transform.position != down)
        {
            Vector3 d = Vector3.MoveTowards(transform.position, down, 10f * Time.deltaTime);
            transform.Translate(d - transform.position, Space.Self);
        }
    }
}
