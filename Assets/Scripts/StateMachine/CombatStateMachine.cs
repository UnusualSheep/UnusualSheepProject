using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : StateMachine
{
    //States
    [HideInInspector]
    public FightEnter_State fightEnter_State;
    [HideInInspector]
    public Combat_Idle idle_State;
    [HideInInspector]
    public MovingToLocation_State movement_State;
    [HideInInspector]
    public SelectingAttack_State selectingAttack_State;
    [HideInInspector]
    public Combat_Attacking attacking_State;
    [HideInInspector]
    public Hit_State hit_State;
    [HideInInspector]
    public KO_State KO_State;
    [HideInInspector]
    public Win_State Win_State;

    //Animator
    [HideInInspector]
    public Animator animator;
    public UnitData unitData;
    public CharacterControl characterControl;

    private void Awake()
    {
        fightEnter_State = new FightEnter_State(this);
        idle_State = new Combat_Idle(this);
        movement_State = new MovingToLocation_State(this);
        selectingAttack_State = new SelectingAttack_State(this);
        attacking_State = new Combat_Attacking(this);
        hit_State = new Hit_State(this);
        KO_State = new KO_State(this);
        Win_State = new Win_State(this);
        
        animator = GetComponentInChildren<Animator>();
        unitData = GetComponent<UnitData>();
        characterControl = GetComponent<CharacterControl>();
    }
    protected override BaseState GetInitialState()
    {
        return fightEnter_State;
    }

    public void CheckIsDead()
    {
        if(unitData.curHp <= 0)
        {
            ChangeState(KO_State);
        }
    }

    public void StartState()
    {
        if(unitData.curHp <= 0)
        {
            unitData.curHp = 1;
        }
        ChangeState(fightEnter_State);
    }
    public void IdleState()
    {
        ChangeState(idle_State);
    }
    public void WinState()
    {
        ChangeState(Win_State);
    }
}
