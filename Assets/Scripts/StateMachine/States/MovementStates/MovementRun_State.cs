using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRun_State : BaseState
{
    private MovementStateMachine _msm;
    private float _VerticalInput;
    private float _HorizontalInput;
    private bool _runKey;
    public MovementRun_State(MovementStateMachine movementStateMachine) : base("MovementRun_State", movementStateMachine)
    {
        _msm = (MovementStateMachine)movementStateMachine;
    }


    public override void Enter()
    {
        base.Enter();
        _VerticalInput = 0;
        _HorizontalInput = 0;
        _msm.animator.SetBool("Running", true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _VerticalInput = Input.GetAxis("Vertical");
        _HorizontalInput = Input.GetAxis("Horizontal");
        if (!_msm.canMove) { _VerticalInput = 0; _HorizontalInput = 0; }
        _msm.animator.SetFloat("hInput", _HorizontalInput);
        _msm.animator.SetFloat("vInput", _VerticalInput);
        _runKey = Input.GetKey(KeyCode.LeftShift);
        if (Mathf.Abs(_VerticalInput) < Mathf.Epsilon && Mathf.Abs(_HorizontalInput) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(_msm.idle_State);
        }
        if (!_runKey)
        {
            stateMachine.ChangeState(((MovementStateMachine)stateMachine).movement_State);
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector3 vel = _msm.characterController.velocity;
        Vector3 dir = _msm.transform.forward * _VerticalInput + _msm.transform.right * _HorizontalInput;
        dir.y = _msm.characterController.isGrounded ? 0f : vel.y - _msm.gravity;
        _msm.characterController.Move(dir.normalized * _msm.runSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
        _msm.animator.SetBool("Running", false);
    }
}
