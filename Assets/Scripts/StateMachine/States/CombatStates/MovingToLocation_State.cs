using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToLocation_State : BaseState
{
    private CombatStateMachine _csm;
    Vector3 startLocation;
    Vector3 destination;
    public MovingToLocation_State(CombatStateMachine combatStateMachine) : base("MovingToLocation_State", combatStateMachine)
    {
        _csm = (CombatStateMachine)combatStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        startLocation = _csm.unitData.charControl.startPos;
        _csm.animator.SetTrigger("Move");
        _csm.animator.SetBool("IsAtDestination", false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //once in position either attack or move back
        if (_csm.unitData.charControl._target != null)
        {
            if (_csm.unitData.charControl.selectedAttack.abilityRange == AbilityRange.Melee)
            {
                destination = _csm.unitData.charControl._target.meleeRange.position;
            }
            else if (_csm.unitData.charControl.selectedAttack.abilityRange == AbilityRange.Ranged)
            {
                destination = _csm.unitData.charControl.rangedPosition.position;
            }
        }
        else
        {
            destination = startLocation;
        }
        if (Vector3.Distance(_csm.transform.position, destination) < 0.001f)
        {
            if (_csm.unitData.charControl._target != null)
            {
                _csm.ChangeState(((CombatStateMachine)stateMachine).attacking_State);
            }
            else
            {
                _csm.animator.SetBool("IsAtDestination", true);
                _csm.ChangeState(((CombatStateMachine)stateMachine).idle_State);
            }
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (_csm.unitData.charControl.characterTeam == CharacterTeam.Friendly)
        {
            _csm.unitData.UpdateUI();
        }
        //handle move
        _csm.transform.position = Vector3.MoveTowards(_csm.transform.position, destination, (_csm.unitData.charControl.moveSpeed * Time.deltaTime));
        _csm.transform.LookAt(destination);
    }
}
