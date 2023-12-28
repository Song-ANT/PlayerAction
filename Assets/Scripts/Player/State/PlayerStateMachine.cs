using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    #region Field
    public Player Player { get; }

    // States
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RuntState { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }
    public PlayerComboAttackState ComboAttackState { get; }

    // 
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public bool IsRuned { get; set; }
    public bool IsAttacking {  get; set; }
    public int ComboIndex { get; set; }

    public float JumpForce { get; set; }

    public Transform MainCameraTransform { get; set; }
    #endregion


    #region Init
    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RuntState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        ComboAttackState = new PlayerComboAttackState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundSO.BaseSpeed;
        RotationDamping = player.Data.GroundSO.BaseRotationDamping;
    }
    #endregion
}
