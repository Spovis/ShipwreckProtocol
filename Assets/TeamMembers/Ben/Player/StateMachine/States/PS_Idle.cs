using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Idle : PS_Base
{
    public PS_Idle(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        if(!_machine.Collider.IsTouchingLayers(~_groundLayer)) {
            _machine.SwitchState(PlayerStates.Fall);
            if(Player.Instance != null) Player.Instance.JumpCount++;
            return;
        }

        _machine.Animator.SetTrigger("Idle");

        // Just a safety precaution to ensure the jumps get reset when idling (meaning they must be on the ground).
        Player.Instance.JumpCount = 0;
    }

    public override void UpdateState() {
        base.UpdateState();

        if (_machine.Input.CurrentMovementInput.x != 0) {
            _machine.SwitchState(PlayerStates.Walk);
            return;
        }
    }
}
