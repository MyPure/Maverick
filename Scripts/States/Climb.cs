using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : PlayerState
{
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void StateStart()
    {
        base.StateStart();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
    }
    public override void SetType()
    {
        stateType = StateType.Climb;
    }
}
