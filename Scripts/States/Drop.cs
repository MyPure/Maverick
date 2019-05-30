using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : PlayerState
{
    float velocity;
    public override void StateStart()
    {
        velocity = 0;
    }
    public override void StateUpdate()
    {
        HandleInput();
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
        velocity -= player.G * Time.deltaTime;
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, Mathf.Abs(velocity) * Time.deltaTime));
            Debug.DrawLine(transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.down * Mathf.Abs(velocity) * Time.deltaTime, Color.red);
        }
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider)
            {
                transform.position = new Vector3(transform.position.x, hits[i].point.y + player.height / 2 + 0.02f, 0);
                ChangeStateTo(StateType.Stand);//Drop -> Stand
            }
        }
    }
    public override void HandleInput()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.right, player.speed * Time.deltaTime));
            }
            bool haveObstacle = false;
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider)
                {
                    haveObstacle = true;
                    break;
                }
            }
            if (!haveObstacle) MoveTo(Vector3.right * Input.GetAxis("Horizontal") * player.speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.left, player.speed * Time.deltaTime));
            }
            bool haveObstacle = false;
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider)
                {
                    haveObstacle = true;
                    break;
                }
            }
            if (!haveObstacle) MoveTo(Vector3.right * Input.GetAxis("Horizontal") * player.speed * Time.deltaTime);
        }
    }
    public override void SetType()
    {
        stateType = StateType.Drop;
    }
}
