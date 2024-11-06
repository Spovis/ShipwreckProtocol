using System.Collections;
using UnityEngine;

public class PS_Die : PS_Base {
    public PS_Die(PlayerStateMachine machine) : base(machine) { }

    public override void EnterState() {
        base.EnterState();

        DisableAll();

        _machine.Logic.PlayDeathVisuals();
    }

    private void DisableAll()
    {
        _machine.Input.CanInput = false;
        _machine.Rigidbody.velocity = Vector2.zero;
        _machine.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _machine.Collider.enabled = false;
    }

    private void EnableAll()
    {
        _machine.Input.CanInput = true;
        _machine.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _machine.Collider.enabled = true;
    }
}