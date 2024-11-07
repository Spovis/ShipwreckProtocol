using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class PS_Attack : PS_Base
{
    public PS_Attack(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        if (!_machine.Input.CanAttack)
        {
            PlayerStateMachine.Instance.SwitchState(PlayerStates.Idle);
            return;
        }

        _machine.Animator.SetTrigger("Attack");
        _machine.Logic.Shoot();
        HapticFeedback.MediumFeedback();

        if (_machine.IsGrounded) _machine.Rigidbody.velocity *= Vector3.up;
    }

    public override void ExitState() { base.ExitState(); }

    public override void OnCollisionEnter2DState(Collision2D collision) {
        _machine.Rigidbody.velocity *= Vector3.up;
    }

    public override void UpdateState() {
        base.UpdateState();
    }
}