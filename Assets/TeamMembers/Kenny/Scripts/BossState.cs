using UnityEngine;

public abstract class BossState {
  public virtual void OnEnterState(Animator animator) {
    Debug.Log("abstract class definition of OnEnterState called");
  }

  public virtual void OnStateUpdate(
    GameObject gameObject,
    Animator animator,
    BossStateMachine stateMachine,
    Rigidbody2D fireballPrefab,
    Transform player)
  {
    Debug.Log("abstract class definition of OnStateUpdate called");
  }

  public virtual void OnExitState(Animator animator) {
    Debug.Log("abstract class definition of OnExitState called");
  }

  public virtual void OnTriggerEnter2D(Collider2D collider, Boss boss) {
    Debug.Log("abstract class definition of OnCollisionEnter2D called");
  }
}