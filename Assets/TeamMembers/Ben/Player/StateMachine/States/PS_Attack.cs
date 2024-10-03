using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Attack : PS_Base
{
    public PS_Attack(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        _machine.Animator.SetTrigger("Attack");

        if(_machine.IsGrounded) _machine.Rigidbody.velocity *= Vector3.up;
    }

    public override void OnCollisionEnter2DState(Collision2D collision) {
        _machine.Rigidbody.velocity *= Vector3.up;
    }

    public override void UpdateState() {
        base.UpdateState();

    }
}
