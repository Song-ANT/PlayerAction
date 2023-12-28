using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.MovementInput != Vector2.zero)
        {
            if (stateMachine.IsRuned)
            {
                OnRun();
                return;
            }
            OnMove();
            return;
        }
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        stateMachine.IsRuned = true;
        base.OnRunStarted(context);
    }
    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsRuned = false;
        base.OnRunCanceled(context);
    }
}