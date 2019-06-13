using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squat : PlayerState
{
    public float squatHeight = 1.0f;
    float velocity;
    BoxCollider2D boxCollider2D;
    float time;
    public AudioClip walk;
    public override void HandleInput()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            player.flip = true;
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
            player.flip = false;
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
        if (Input.GetAxis("Horizontal") != 0)
        {
            time += Time.deltaTime;
            if (time >= 0.75f)
            {
                player.audioSource.clip = walk;
                player.audioSource.Play();
                time = 0;
            }
        }
    }
    public override void StateStart()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(1,squatHeight);
        boxCollider2D.offset = new Vector2(0, -0.5f * (player.height-squatHeight));
        velocity = 0;
        time = 0;
    }
    public override void StateUpdate()
    {
        HandleInput();//移动检测

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        //掉落检测
        hits.Clear();
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
                transform.position = new Vector3(transform.position.x, hits[i].point.y + player.height / 2 + 0.05f, 0);
                break;
            }
        }

        //站立检测
        hits.Clear();
        bool canStand = true;
        for (int i = 0; i < player.rayY; i++)
        {
            hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2 + i * player.width / (player.rayY - 1), squatHeight - player.height / 2), Vector2.up, player.height - squatHeight + 0.02f, ~(1 << 8)));
        }
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider && !hits[i].collider.isTrigger)
            {
                canStand = false;
                break;
            }
        }
        if (!Input.GetKey(KeyCode.S) && canStand && onGround)
        {
            boxCollider2D.size = new Vector2(1, player.height);
            boxCollider2D.offset = new Vector2(0, 0);
            ChangeStateTo(StateType.Stand);//Squat -> Stand
            return;
        }

        //跳跃检测
        if (Input.GetKey(KeyCode.Space) && canStand)
        {
            boxCollider2D.size = new Vector2(1, player.height);
            boxCollider2D.offset = new Vector2(0, 0);
            ChangeStateTo(StateType.Jump);//Squat -> Jump
            return;
        }

        //掉落判定
        if (!onGround && canStand)
        {
            boxCollider2D.size = new Vector2(1, player.height);
            boxCollider2D.offset = new Vector2(0, 0);
            ChangeStateTo(StateType.Drop);//Squat -> Drop
            GetComponent<Drop>().velocity = velocity;
            return;
        }
        else if(!onGround && !canStand)//蹲下状态并且不能站起来的掉落
        {
            transform.Translate(Vector3.up * Time.deltaTime * velocity);
            velocity -= player.G * Time.deltaTime;
        }
    }
    public override void SetType()
    {
        stateType = StateType.Squat;
    }
}
