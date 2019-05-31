using System.Collections;
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
    }
    public override void SetType()
    {
        stateType = StateType.DoubleJump;
    }
}
