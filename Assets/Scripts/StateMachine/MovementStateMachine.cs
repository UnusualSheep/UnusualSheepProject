using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : StateMachine
{
    //states
    [HideInInspector]
    public MovementIdle_State idle_State;
    [HideInInspector]
    public Movement_State movement_State; 
    [HideInInspector]
    public MovementRun_State movementRun_State;
    [HideInInspector]
    public Animator animator;

    public CharacterController characterController;
    public bool canMove = true;
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float gravity = 9.81f;

    private void Awake()
    {
        idle_State = new MovementIdle_State(this);
        movement_State = new Movement_State(this);
        movementRun_State = new MovementRun_State(this);
        animator = GetComponentInChildren<Animator>();
    }
    protected override BaseState GetInitialState()
    {
        return idle_State;
    }
}
