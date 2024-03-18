using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_State : BaseState
{
    public Win_State(CombatStateMachine combatStateMachine) : base("Win_State", combatStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //do a little dance
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
