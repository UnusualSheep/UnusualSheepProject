using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_State : BaseState
{
    private CombatStateMachine _csm;
    public Win_State(CombatStateMachine combatStateMachine) : base("Win_State", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _csm.animator.SetTrigger("Win");
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
