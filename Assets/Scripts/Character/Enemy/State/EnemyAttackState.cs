using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;

        stateMachine.MovementSpeedModifier = 0; 
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();

            Enemy enemy = stateMachine.Enemy;
            if(!alreadyAppliedDealing && normalizedTime >= enemy.Data.Dealing_Start_TransitionTime)
            {
                enemy.Weapon.SetAttack(enemy.Data.Damage, enemy.Data.Force);
                enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }
            if (alreadyAppliedDealing && normalizedTime >= enemy.Data.Dealing_End_TransitionTime)
            {
                enemy.Weapon.gameObject.SetActive(false);

            }





        }
        else
        {
            if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }

    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Enemy.ForceReceiver.Reset();

        stateMachine.Enemy.ForceReceiver.AddForce(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);

    }
}
