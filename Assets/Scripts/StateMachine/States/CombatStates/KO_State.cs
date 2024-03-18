using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO_State : BaseState
{
    private CombatStateMachine _csm;
    public KO_State(CombatStateMachine combatStateMachine) : base("KO_State", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _csm.animator.SetTrigger("isDead");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_csm.unitData.curHp > 0)
        {
            _csm.ChangeState(((CombatStateMachine)stateMachine).idle_State);
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        _csm.animator.SetTrigger("revived");
    }
}
