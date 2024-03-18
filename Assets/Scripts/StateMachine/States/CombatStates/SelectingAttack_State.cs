using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingAttack_State : BaseState
{
    private CombatStateMachine _csm;
    public SelectingAttack_State(CombatStateMachine combatStateMachine) : base("SelectingAttack_State", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GlobalTimeManager.Instance.globalTimeScale = 0;
        FightManager.Instance.currentUnit = _csm.unitData;
        if (_csm.unitData.charControl.characterTeam == CharacterTeam.Friendly)
        {
            UIManager.Instance.EnableActionPanel();
        }
        else if (_csm.unitData.charControl.characterTeam == CharacterTeam.Enemy)
        {
            _csm.unitData.EnemyAttack();
        }


    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //once attack selected, select target, switch to move
        if (_csm.unitData.charControl._target != null)
        {
            _csm.ChangeState(((CombatStateMachine)stateMachine).movement_State);
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (_csm.unitData.charControl.characterTeam == CharacterTeam.Friendly)
        {
            _csm.unitData.UpdateUI();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
