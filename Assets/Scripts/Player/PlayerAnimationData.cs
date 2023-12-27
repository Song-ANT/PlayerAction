using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundName = "@Ground";
    [SerializeField] private string idleName = "Idle";
    [SerializeField] private string walkName = "Walk";
    [SerializeField] private string runName = "Run";

    [SerializeField] private string airName = "@Air";
    [SerializeField] private string jumpName = "Jump";
    [SerializeField] private string fallName = "Fall";

    [SerializeField] private string attackName = "@Attack";
    [SerializeField] private string comboAttackName = "ComboAttack";


    public int GroundHash { get; private set; }
    public int IdleHash { get; private set; }
    public int WalkHash { get; private set; }
    public int RunHash { get; private set; }

    public int AirHash { get; private set; }
    public int JumpHash { get; private set; }
    public int fallHash { get; private set; }

    public int AttackHash { get; private set; }
    public int ComboAttackHash { get; private set; }

    public void Initialize()
    {
        GroundHash = Animator.StringToHash(groundName);
        IdleHash = Animator.StringToHash(idleName);
        WalkHash = Animator.StringToHash(walkName);
        RunHash = Animator.StringToHash(runName);

        AirHash = Animator.StringToHash(airName);
        JumpHash = Animator.StringToHash(jumpName);
        fallHash = Animator.StringToHash(fallName);

        AttackHash = Animator.StringToHash(attackName);
        ComboAttackHash = Animator.StringToHash(comboAttackName);
    }
}