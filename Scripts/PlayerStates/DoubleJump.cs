﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : PlayerState
{
    float velocity;
    public float jumpH = 1.5f;
    public override void HandleInput()
    {
        HorizontalMove();
    }
    public override void StateStart()
    {
        velocity = Mathf.Sqrt(2 * player.G * jumpH);//v = √2gh
    }
    public override void StateUpdate()
    {
        HandleInput();//检测输入
        transform.Translate(Vector3.up * Time.deltaTime * velocity);//垂直移动
        velocity -= player.G * Time.deltaTime;//模拟重力

        //触地判定
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        if (velocity <= 0)//只有下降期才判定
        {
            for (int i = 0; i < player.rayY; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, Mathf.Abs(velocity) * Time.deltaTime, ~(1 << 8)));
                Debug.DrawLine(transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.down * Mathf.Abs(velocity) * Time.deltaTime, Color.red);
            }
            for (int i = 0; i < hits.Count; i++)
            {
                if (hits[i].collider && !hits[i].collider.isTrigger)
                {
                    transform.position = new Vector3(transform.position.x, hits[i].point.y + player.height / 2 + 0.02f, 0);
                    ChangeStateTo(StateType.Stand);//DoubleJump -> Stand
                }
            }
        }
        //磕头判定
        if (velocity > 0)//只有上升期才判定
        {
            hits.Clear();
            for (int i = 0; i < player.rayY; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2 + i * player.width / (player.rayY - 1), player.height / 2), Vector2.up, Mathf.Abs(velocity) * Time.deltaTime, ~(1 << 8)));
                Debug.DrawLine(transform.position + new Vector3(-player.width / 2 + i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position + new Vector3(-player.width / 2 + i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.up * Mathf.Abs(velocity) * Time.deltaTime, Color.red);
            }
            for (int i = 0; i < hits.Count; i++)
            {
                if (hits[i].collider && !hits[i].collider.isTrigger)
                {
                    transform.position = new Vector3(transform.position.x, hits[i].point.y - player.height / 2 - 0.02f, 0);
                    velocity = 0;
                }
            }
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
    public override void SetType()
    {
        stateType = StateType.DoubleJump;
    }
}