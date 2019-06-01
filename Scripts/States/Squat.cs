﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squat : PlayerState
{
    public float squatHeight = 1.0f;
    public Sprite standSprite , squatSprite;
    public override void HandleInput()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * squatHeight / (player.rayX - 1)), Vector2.right, player.speed * Time.deltaTime, ~(1 << 8)));
            }
            bool haveObstacle = false;
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider && !h.collider.isTrigger)
                {
                    haveObstacle = true;
                    break;
                }
            }
            if (!haveObstacle) transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * player.speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * squatHeight / (player.rayX - 1)), Vector2.left, player.speed * Time.deltaTime, ~(1 << 8)));
            }
            bool haveObstacle = false;
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider && !h.collider.isTrigger)
                {
                    haveObstacle = true;
                    break;
                }
            }
            if (!haveObstacle) transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * player.speed * Time.deltaTime);
        }
    }
    public override void StateStart()
    {
        GetComponent<SpriteRenderer>().sprite = squatSprite;
    }
    public override void StateUpdate()
    {
        HandleInput();//移动检测

        //站立检测
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        bool canStand = true;
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2 + i * player.width / (player.rayY - 1), squatHeight - player.height / 2), Vector2.up, player.height - squatHeight , ~(1 << 8)));
        }
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider && !hits[i].collider.isTrigger)
            {
                canStand = false;
                break;
            }
        }
        if (!Input.GetKey(KeyCode.S) && canStand)
        {
            GetComponent<SpriteRenderer>().sprite = standSprite;
            ChangeStateTo(StateType.Stand);//Squat -> Stand
            return;
        }

        //跳跃检测
        if (Input.GetKey(KeyCode.Space) && canStand)
        {
            GetComponent<SpriteRenderer>().sprite = standSprite;
            ChangeStateTo(StateType.Jump);//Squat -> Jump
            return;
        }

        //掉落检测
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, 0.1f, ~(1 << 8)));
            Debug.DrawLine(transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.down * Time.deltaTime, Color.red);
        }
        bool onGround = false;
        for (int i = 0; i < hits.Count; i++)
        {

            if (hits[i].collider && !hits[i].collider.isTrigger)
            {
                onGround = true;
                break;
            }
        }
        if (!onGround)
        {
            GetComponent<SpriteRenderer>().sprite = standSprite;
            ChangeStateTo(StateType.Drop);//Squat -> Drop
            return;
        }

    }
    public override void SetType()
    {
        stateType = StateType.Squat;
    }
}
