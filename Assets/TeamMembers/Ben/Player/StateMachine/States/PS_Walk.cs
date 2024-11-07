using System.Collections;
using UnityEngine;

public class PS_Walk : PS_Base {
    public PS_Walk(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Walk");
    }

    public override void ExitState(PlayerStates newState) { base.ExitState(newState); }

    public override void UpdateState() {
        base.UpdateState();

        _machine.Logic.MovePlayer();

        if (_machine.Rigidbody.velocity.x == 0) {
            _machine.SwitchState(PlayerStates.Idle);
            return;
        }
    }
}