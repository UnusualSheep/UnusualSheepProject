using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnter_State : BaseState
{
    private CombatStateMachine _csm;
    public FightEnter_State(CombatStateMachine combatStateMachine) : base("FightEnter_State", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _csm.characterControl.SetAnimationStoppedFalse();
        if(_csm.unitData.curHp <=0)
        {
            _csm.unitData.curHp = 1;
        }
        _csm.animator.SetTrigger("FightStarted");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_csm.characterControl.animationStopped)
        {
            stateMachine.ChangeState(((CombatStateMachine)stateMachine).idle_State);
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
