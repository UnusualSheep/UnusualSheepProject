using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_State : BaseState
{
    private CombatStateMachine _csm;
    public Hit_State(CombatStateMachine combatStateMachine) : base("Hit_State", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();

        _csm.animator.SetTrigger("hit");
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
