using System.Collections;
using UnityEngine;

public class PS_Jump : PS_Base {
    public PS_Jump(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Jump");

        _machine.Logic.JumpCount++;

        _machine.Logic.JumpPlayer();
    }

    public override void ExitState(PlayerStates newState) { base.ExitState(newState); }

    public override void UpdateState() {
        base.UpdateState();

        _machine.Logic.MovePlayer();

        if (_machine.Rigidbody.velocity.y < 0) {
            _machine.SwitchState(PlayerStates.Fall);
            return;
        }
    }

    public override void OnCollisionEnter2DState(Collision2D collision) {
        base.OnCollisionEnter2DState(collision);

        // Janky fix of player's jump count resetting when they touch ground above or beside them.
        if (collision.gameObject.layer == _groundLayer.value && collision.gameObject.transform.position.y < _machine.transform.position.y - 1.25f) {
            _machine.Logic.JumpCount = 0;
            _machine.SwitchState(PlayerStates.Idle);
            return;
        }
    }

    public override void OnCollisionExit2DState(Collision2D collision) { }
}