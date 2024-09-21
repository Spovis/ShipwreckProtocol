﻿using System.Collections;
using UnityEngine;

public class PS_Fall : PS_Base {
    public PS_Fall(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Fall");
    }

    public override void UpdateState() {
        base.UpdateState();

        _machine.GenericMovePlayer();
    }

    public override void OnCollisionEnter2DState(Collision2D collision) {
        base.OnCollisionEnter2DState(collision);

        if(collision.gameObject.layer == _groundLayer.value) {
            Player.Instance.JumpCount = 0;
            _machine.SwitchState(PlayerStates.Idle);
            return;
        }
    }
}