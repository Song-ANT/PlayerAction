using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    #region Field
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundSO groundData;

    #endregion

    #region Init
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundSO;
    }
    
    #endregion

    #region IState
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }
    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }
    public virtual void Update()
    {
        Move();
    }

    public virtual void PhysicsUpdate()
    {

    }
    #endregion

    #region InputAction
    private void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.performed += OnRunStarted;
        input.PlayerActions.Run.canceled += OnRunCanceled;

        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    private void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.canceled -= OnRunStarted;
        input.PlayerActions.Run.canceled -= OnRunCanceled;

        input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }




    #endregion

    #region Override
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
    }
    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
    }
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
    }
    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }
    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }
    #endregion

    #region Movement
    private void ReadMovementInput() // 입력한 값을 읽어오기
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move() // 방향 얻어 움직이기 명령부
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection) // 방향 얻어 움직이기 실행부
    {
        float movementSpeed = GetMovemenetSpeed();
        stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed)
            + stateMachine.Player.ForceReceiver.Movement)
            * Time.deltaTime
            );
    }

    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    private void Rotate(Vector3 movementDirection) // 바라보는 방향 설정
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private Vector3 GetMovementDirection() // 방향얻기
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }


    private float GetMovemenetSpeed() // 속도 얻기 (State별로 SpeedModifier 조정)
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }
    #endregion


    #region Animation
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
    #endregion

    #region Attack
    protected float GetNormalizedTime(Animator animator,string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    #endregion

}