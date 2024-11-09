using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Idle : PS_Base
{
    public PS_Idle(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        
        if (_machine.Collider.IsTouchingLayers(_waterLayerMask)) {
            _machine.SwitchState(PlayerStates.Swim);
            return;
        }
        else if (_machine.Collider.IsTouchingLayers(_shallowWaterLayerMask)) {
            _machine.SwitchState(PlayerStates.Tread);
            return;
        }
        else if (!_machine.IsGrounded) {
            _machine.SwitchState(PlayerStates.Fall);
            _machine.Logic.JumpCount = 1;
            return;
        }

        _machine.Animator.SetTrigger("Idle");

        // Just a safety precaution to ensure the jumps get reset when idling (meaning they must be on the ground).
        _machine.Logic.JumpCount = 0;
    }

    public override void ExitState(PlayerStates newState) { base.ExitState(newState); }

    public override void UpdateState() {
        base.UpdateState();

        if (_machine.Input.CurrentMovementInput.x != 0) {
            _machine.SwitchState(PlayerStates.Walk);
            return;
        }
    }
}
