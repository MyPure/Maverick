using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : PlayerState
{
    public override void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeStateTo(StateType.Jump);//Stand -> Jump
        }
        if (Input.GetAxis("Horizontal")>0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.right, player.speed * Time.deltaTime));
            }
            bool haveObstacle = false;
            foreach(RaycastHit2D h in hits)
            {
                if (h.collider)
                {
                    haveObstacle = true;
                    break;
                }
            }
            if(!haveObstacle) MoveTo(Vector3.right * Input.GetAxis("Horizontal") * player.speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal")<0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * player.height / (player.rayX-1)), Vector2.left, player.speed * Time.deltaTime));
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
    public override void StateStart()
    {

    }
    public override void StateUpdate()
    {
        HandleInput();
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, 0.1f));
            Debug.DrawLine(transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.down * Time.deltaTime, Color.red);
        }
        bool onGround = false;
        for (int i = 0; i < hits.Count; i++)
        {
            
            if (hits[i].collider)
            {
                onGround = true;
                break;
            }
        }
        if(!onGround) ChangeStateTo(StateType.Drop);//Stand -> Drop
    }
    public override void SetType()
    {
        stateType = StateType.Stand;
    }
}
