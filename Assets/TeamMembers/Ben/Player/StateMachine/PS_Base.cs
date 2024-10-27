using UnityEngine;

// The naming convention for player states is PS_<StateName>. PS stands for "Player State".
public abstract class PS_Base
{
    protected PlayerStateMachine _machine;

    protected LayerMask _groundLayer, _groundLayerMask;
    protected LayerMask _waterLayer, _waterLayerMask;
    protected LayerMask _shallowWaterLayer, _shallowWaterLayerMask;

    float playerDrag;

    public PS_Base(PlayerStateMachine machine) {
        _machine = machine;

        playerDrag = _machine.Rigidbody.drag;

        _groundLayer = LayerMask.NameToLayer("Ground");
        _groundLayerMask = LayerMask.GetMask("Ground");
        _waterLayer = LayerMask.NameToLayer("Water");
        _waterLayerMask = LayerMask.GetMask("Water");
        _shallowWaterLayer = LayerMask.NameToLayer("ShallowWater");
        _shallowWaterLayerMask = LayerMask.GetMask("ShallowWater");
    }

    public virtual void EnterState() {
        // Sets physics depending on current environment
        if (_machine.Collider.IsTouchingLayers(_waterLayerMask)) { // In water (we don't apply this for shallow water)
            _machine.Rigidbody.drag = 8; // Raises drag
        }
        else { // Not in water
            _machine.Rigidbody.drag = playerDrag;
        }
    }
    public virtual void ExitState() { }

    // If there is some behaviour that is consistent across almost every
    // state, we will put the logic here in the base class.

    public virtual void UpdateState() {
        if(_machine.Input.IsJumpPressed && _machine.Logic.JumpCount < _machine.Logic.MaxJumpCount) {
            _machine.SwitchState(PlayerStates.Jump);
            return;
        }
    }
    public virtual void FixedUpdateState() { }
    public virtual void OnCollisionEnter2DState(Collision2D collision) { }
    public virtual void OnCollisionExit2DState(Collision2D collision) {
        if (collision.gameObject.layer == _groundLayer.value) {
            _machine.SwitchState(PlayerStates.Fall);
            _machine.Logic.JumpCount++; // If the player walks off a ledge, we count it towards a jump.
            return;
        }
    }
    public virtual void OnTriggerEnter2DState(Collider2D collision) {
        if(collision.gameObject.layer == _waterLayer.value) {
            _machine.SwitchState(PlayerStates.Swim);
            return;
        }
        else if (collision.gameObject.layer == _shallowWaterLayer.value) {
            _machine.SwitchState(PlayerStates.Tread);
            return;
        }
    }
    public virtual void OnTriggerExit2DState(Collider2D collision) { }
}
