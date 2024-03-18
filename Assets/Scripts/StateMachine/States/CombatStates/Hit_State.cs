using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_State : BaseState
{
    public Hit_State(CombatStateMachine combatStateMachine) : base("Hit_State", combatStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        //play hit animation
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //wait for hit animation to be done
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
