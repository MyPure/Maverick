using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : PlayerState
{
    public AudioClip walk;
    public override void HandleInput()
    {
        HorizontalMove();
        if(Input.GetAxis("Horizontal") == 0)
        {
            player.animator.Play("idle");
        }
        else
        {
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                player.audioSource.clip = walk;
                player.audioSource.Play();
                time = 0;
            }
            player.animator.Play("walk");
        }
    }
    public override void StateStart()
    {
        time = 0.5f;
    }
    private float time;
    public override void StateUpdate()
    {
        HandleInput();//检测输入
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position - new Vector2(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2), Vector2.down, 0.06f, ~(1 << 8)));
            Debug.DrawLine(transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0), transform.position - new Vector3(player.width / 2 - i * player.width / (player.rayY - 1), player.height / 2, 0) + Vector3.down * Time.deltaTime, Color.red);
        }
        bool onGround = false;
        for (int i = 0; i < hits.Count; i++)
        {
            
            if (hits[i].collider && !hits[i].collider.isTrigger)
            {
                onGround = true;
                transform.position = new Vector3(transform.position.x, hits[i].point.y + player.height / 2 + 0.05f, 0);
                break;
            }
        }
        if (!onGround)
        {
            ChangeStateTo(StateType.Drop);//Stand -> Drop
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ChangeStateTo(StateType.Squat);//Stand -> Squat
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeStateTo(StateType.Jump);//Stand -> Jump
        }
    }
    public override void SetType()
    {
        stateType = StateType.Stand;
    }
}
