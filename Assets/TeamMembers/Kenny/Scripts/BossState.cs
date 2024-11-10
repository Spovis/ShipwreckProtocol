using UnityEngine;

// This defines the shape of the base class for all boss states. The methods
// defined here should never actually be called at runtime.
public abstract class BossState {
  // Called when the boss changes TO a particular state
  public virtual void OnEnterState(Animator animator) {
    Debug.Log("abstract class definition of OnEnterState called");
  }

  // Called every frame the boss is in a particular state
  public virtual void OnStateUpdate(
    GameObject gameObject,
    Animator animator,
    BossStateMachine stateMachine,
    Rigidbody2D fireballPrefab,
    Transform player)
  {
    Debug.Log("abstract class definition of OnStateUpdate called");
  }

  // Called when the boss changes FROM a particular state
  public virtual void OnExitState(Animator animator) {
    Debug.Log("abstract class definition of OnExitState called");
  }

  // Called when the boss collides with another object
  public virtual void OnTriggerEnter2D(Collider2D collider, Boss boss) {
    Debug.Log("abstract class definition of OnCollisionEnter2D called");
  }
}