using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left,
    Right
}
public class Climb : PlayerState
{
    Direction direction;//攀爬墙的位置
    public float dropSpeed = 1.0f;
    public override void HandleInput()
    {
        
    }
    public override void StateStart()
    {
        //右侧检测
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < player.rayX; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.right, player.speed * Time.deltaTime, ~(1 << 8)));
        }
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider && !h.collider.isTrigger)
            {
                direction = Direction.Right;
                return;
            }
        }

        //左侧检测
        hits.Clear();
        for (int i = 0; i < player.rayX; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.left, player.speed * Time.deltaTime, ~(1 << 8)));
        }
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider && !h.collider.isTrigger)
            {
                direction = Direction.Left;
                return;
            }
        } 
    }
    public override void StateUpdate()
    {
        //匀速下降
        transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);

        //跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeStateTo(StateType.Jump);
            GetComponent<Jump>().JumpByDirection(direction);//Climb -> Jump
            return;
        }

        //触地检测
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, dropSpeed * Time.deltaTime, ~(1 << 8)));
        }
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider && !hits[i].collider.isTrigger)
            {
                transform.position = new Vector3(transform.position.x, hits[i].point.y + player.height / 2 + 0.02f, 0);
                ChangeStateTo(StateType.Stand);//Climb -> Stand
                return;
            }
        }

        //掉落墙检测
        hits.Clear();
        bool exitWall = true;
        if (direction == Direction.Right)
        {
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.right, player.speed * Time.deltaTime, ~(1 << 8)));
            }
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider && !h.collider.isTrigger)
                {
                    exitWall = false;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.left, player.speed * Time.deltaTime, ~(1 << 8)));
            }
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider && !h.collider.isTrigger)
                {
                    exitWall = false;
                    break;
                }
            }
        }
        if (exitWall)
        {
            ChangeStateTo(StateType.Drop);//Climb -> Drop
        }
    }
    public override void SetType()
    {
        stateType = StateType.Climb;
    }
}
