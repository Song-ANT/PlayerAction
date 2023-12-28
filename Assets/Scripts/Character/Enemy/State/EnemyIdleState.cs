using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundHash);
        StartAnimation(stateMachine.Enemy.AnimationData.IdleHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundHash);
        StopAnimation(stateMachine.Enemy.AnimationData.IdleHash);
    }

    public override void Update()
    {
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
}