﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : PlayerState
{
    [HideInInspector]
    public float velocity;
    [HideInInspector]
    public DoubleJump doubleJump;
    public override void StateStart()
    {
        doubleJump = GetComponent<DoubleJump>();
        velocity = 0;
    }
    public override void StateUpdate()
    {
        HandleInput();//检测输入
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
        velocity -= player.G * Time.deltaTime;
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, Mathf.Abs(velocity) * Time.deltaTime, ~(1 << 8)));
            Debug.DrawLine(transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.down * Mathf.Abs(velocity) * Time.deltaTime, Color.red);
        }
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider && !hits[i].collider.isTrigger)
            {
                transform.position = new Vector3(transform.position.x, hits[i].point.y + player.height / 2 + 0.05f, 0);
                ChangeStateTo(StateType.Stand);//Drop -> Stand
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.gameController.神荼)
        {
            ChangeStateTo(StateType.DoubleJump);//Drop -> DoubleJump
            return;
        }

        //攀爬判定
        hits.Clear();
        for (int i = 0; i < player.rayX; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.right, 0.02f, ~(1 << 8)));
        }
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider && !h.collider.isTrigger)
            {
                ChangeStateTo(StateType.Climb);//Jump -> Climb
                transform.position = new Vector3(h.point.x - player.width / 2 - 0.02f, transform.position.y, 0);
                return;
            }
        }
        hits.Clear();
        for (int i = 0; i < player.rayX; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.left, 0.02f, ~(1 << 8)));
        }
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider && !h.collider.isTrigger)
            {
                ChangeStateTo(StateType.Climb);//Jump -> Climb
                transform.position = new Vector3(h.point.x + player.width / 2 + 0.02f, transform.position.y, 0);
                return;
            }
        }
    }
    public override void HandleInput()
    {
        HorizontalMove();
    }
    public override void SetType()
    {
        stateType = StateType.Drop;
    }
}
