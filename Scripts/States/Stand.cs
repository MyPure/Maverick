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
        HorizontalMove();
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
        if(!onGround) ChangeStateTo(StateType.Drop);//Stand -> Drop
    }
    public override void SetType()
    {
        stateType = StateType.Stand;
    }
}
