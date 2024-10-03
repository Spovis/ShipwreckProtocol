using System.Collections;
using UnityEngine;

public class PS_Jump : PS_Base {
    public PS_Jump(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Jump");

        PlayerLogic.Instance.JumpCount++;

        PlayerLogic.Instance.JumpPlayer();
    }

    public override void UpdateState() {
        base.UpdateState();

        PlayerLogic.Instance.MovePlayer();

        if (_machine.Rigidbody.velocity.y < 0) {
            _machine.SwitchState(PlayerStates.Fall);
            return;
        }
    }

    public override void OnCollisionEnter2DState(Collision2D collision) {
        base.OnCollisionEnter2DState(collision);

        if (collision.gameObject.layer == _groundLayer.value) {
            PlayerLogic.Instance.JumpCount = 0;
            _machine.SwitchState(PlayerStates.Idle);
            return;
        }
    }

    public override void OnCollisionExit2DState(Collision2D collision) { }
}