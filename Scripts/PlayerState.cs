using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType {
    Stand,
    Jump,
    Squat,
    Drop
}

public class PlayerState : MonoBehaviour
{
    public StateType stateType;
    public Player player;
    public virtual void HandleInput()
    {
        //输入判定
    }
    public virtual void StateStart()
    {
        
    }
    public virtual void StateUpdate()
    {

    }
    public virtual void SetType()
    {

    }

    public void ChangeStateTo(StateType type)
    {
        foreach (PlayerState s in player.playerStates)
        {
            if(s.stateType == type)
            {
                player.currentState = s;
                player.currentState.StateStart();
                return;
            }
        }
        Debug.Log("未能找到对应的状态，状态转换失败");
    }
    public void HorizontalMove()//水平移动，请在HandleInput里调用
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            for (int i = 0; i < player.rayX; i++)
            {
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.right, player.speed * Time.deltaTime, ~(1 << 8)));
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
                hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(-player.width / 2, -player.height / 2 + i * player.height / (player.rayX - 1)), Vector2.left, player.speed * Time.deltaTime, ~(1 << 8)));
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
}
