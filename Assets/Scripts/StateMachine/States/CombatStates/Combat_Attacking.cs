using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Attacking : BaseState
{
    private CombatStateMachine _csm;
    private bool isMelee;
    public Combat_Attacking(CombatStateMachine combatStateMachine) : base("Combat_Attacking", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        isMelee = (_csm.unitData.charControl.selectedAttack.abilityRange == AbilityRange.Melee)
                   ? true : false;
        _csm.characterControl.SetAnimationStoppedFalse();
        //perform attack
        _csm.transform.LookAt(_csm.unitData.charControl._target.transform);
        //switch camera for melee attacks
        if (isMelee){ _csm.unitData.charControl._target.SwitchCameras(); }

        _csm.characterControl.PerformAttack(_csm.characterControl.selectedAttack);

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //wait for attack to be performed the move back to locatioin
        if(FightManager.Instance.unitAttacked)
        {
            //_csm.unitData.SendDamage();
            _csm.ChangeState(((CombatStateMachine)stateMachine).movement_State);
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        if (isMelee) { _csm.unitData.charControl._target.SwitchCameras(); }
        if(FightManager.Instance.currentUnit == _csm.unitData)
        {
            FightManager.Instance.currentUnit = null;
        }
        FightManager.Instance.unitAttacked = false;
        _csm.unitData.currentSpeed = 0;
        _csm.unitData.charControl._target = null;
        UIManager.Instance.DisableAbilityWindows();
    }
}
