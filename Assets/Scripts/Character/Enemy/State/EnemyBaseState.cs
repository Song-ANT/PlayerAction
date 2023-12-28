using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    #region Field
    protected EnemyStateMachine stateMachine;

    protected readonly PlayerGroundSO groundData;
    #endregion


    #region Init
    public EnemyBaseState(EnemyStateMachine ememyStateMachine)
    {
        stateMachine = ememyStateMachine;
        groundData = stateMachine.Enemy.Data.GroundedData;
    }
    #endregion


    #region IState
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void PhysicsUpdate()
    {
    }
    #endregion

    #region Animation
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }
    #endregion


    #region Move
    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.Controller.Move(((direction * movementSpeed) + stateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
    }

    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    // 
    private Vector3 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
    }
    

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            stateMachine.Enemy.transform.rotation = Quaternion.Slerp(stateMachine.Enemy.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;

        return movementSpeed;
    }
    #endregion

    #region Attack
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    #endregion

    //
    protected bool IsInChaseRange()
    {
        if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }
}