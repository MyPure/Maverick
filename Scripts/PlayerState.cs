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
    public void MoveTo(Vector3 deltaPos)
    {
        transform.Translate(deltaPos);
    }
}
