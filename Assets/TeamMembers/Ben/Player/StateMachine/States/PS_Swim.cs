using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PS_Swim : PS_Base {
    public PS_Swim(PlayerStateMachine machine) : base(machine) { }

    float currentDrag;

    public override void EnterState() {
        base.EnterState();

        currentDrag = _machine.Rigidbody.drag;
        _machine.Rigidbody.drag = 5;

        _machine.Animator.SetTrigger("Swim");
    }

    public override void ExitState() {
        base.ExitState();

        _machine.Rigidbody.drag = currentDrag;
    }

    public override void UpdateState() {
        _machine.GenericMovePlayer(0.7f);

        if(_machine.Input.IsJumpPressed) {
            _machine.GenericJumpPlayer(1.2f);
        }
    }

    public override void OnTriggerExit2DState(Collider2D collision) {
        _machine.SwitchState(PlayerStates.Idle);
    }

    public override void OnCollisionExit2DState(Collision2D collision) { }
}