using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIdle_State : BaseState
{
    private MovementStateMachine _msm;
    private float _VerticalInput;
    private float _HorizontalInput;
    private bool _runKey;
    
    public MovementIdle_State(MovementStateMachine movementStateMachine) : base("MovementIdle_State", movementStateMachine)
    {
        _msm = (MovementStateMachine)movementStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _VerticalInput = 0;
        _HorizontalInput = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _VerticalInput = Input.GetAxis("Vertical");
        _HorizontalInput = Input.GetAxis("Horizontal");
        _runKey = Input.GetKey(KeyCode.LeftShift);
        if(!_msm.canMove)
        {
            return;
        }

        if(Mathf.Abs(_VerticalInput) > Mathf.Epsilon || Mathf.Abs(_HorizontalInput) > Mathf.Epsilon)
        {
            if (_runKey)
            {
                stateMachine.ChangeState(((MovementStateMachine)stateMachine).movementRun_State);
            }
            stateMachine.ChangeState(((MovementStateMachine)stateMachine).movement_State);
        }
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        float yVelocity = 0;
        yVelocity = _msm.characterController.isGrounded ? 0f : yVelocity - _msm.gravity;
        _msm.characterController.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}
