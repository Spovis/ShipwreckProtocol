﻿using System.Collections;
using UnityEngine;

public class PS_Tread : PS_Base {
    public PS_Tread(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Tread");
    }

    public override void ExitState(PlayerStates newState) { base.ExitState(newState); }

    public override void UpdateState() {
        _machine.Logic.MovePlayer(0.6f);

        if(_machine.Input.IsJumpPressed) {
            _machine.Logic.JumpPlayer(0.6f);
        }

        // We use "IsMoving" to activate/deactivate the current state's idle animation
        _machine.Animator.SetBool("IsMoving", _machine.Rigidbody.velocity.x != 0);
    }

    public override void OnTriggerExit2DState(Collider2D collision) {
        _machine.SwitchState(PlayerStates.Idle);
    }

    public override void OnCollisionExit2DState(Collision2D collision) { }
}