using System.Collections;
using UnityEngine;

public class PS_Walk : PS_Base {
    public PS_Walk(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Walk");
    }

    public override void UpdateState() {
        base.UpdateState();

        PlayerLogic.Instance.MovePlayer();

        if (_machine.Rigidbody.velocity.x == 0) {
            _machine.SwitchState(PlayerStates.Idle);
            return;
        }
    }
}