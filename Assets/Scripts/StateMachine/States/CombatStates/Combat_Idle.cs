using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Idle : BaseState
{
    private CombatStateMachine _csm;
    public Combat_Idle(CombatStateMachine combatStateMachine) : base("Combat_Idle", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        GlobalTimeManager.Instance.globalTimeScale = 1;
        _csm.characterControl.SetAnimationStoppedFalse();
        _csm.transform.rotation = new Quaternion(0, 0, 0, 0);
        FightManager.Instance.CheckMatchStatus();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _csm.CheckIsDead();
        _csm.unitData.IncreaseSpeed();

        if (_csm.unitData.currentSpeed >= _csm.unitData.speedLimit)
        {
            _csm.unitData.currentSpeed = _csm.unitData.speedLimit;
            if (FightManager.Instance.currentUnit == null)
            {
                _csm.ChangeState(((CombatStateMachine)stateMachine).selectingAttack_State);
            }
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _csm.CheckIsDead();
        if (_csm.unitData.charControl.characterTeam == CharacterTeam.Friendly)
        {
            _csm.unitData.UpdateUI();
        }
    }
}
