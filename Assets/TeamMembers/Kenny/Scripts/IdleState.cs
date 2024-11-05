using UnityEngine;

public class IdleState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.Play("Idle");
  }

  public override void OnStateUpdate(GameObject gameObject, Animator animator, BossStateMachine stateMachine) {
  }

  public override void OnExitState(Animator animator) {
  }

  public override void OnTriggerEnter2D(Collider2D collider, Boss boss) {
    Debug.Log("IdleState.OnTriggerEnter2D called");
    Debug.Log("IdleState" + collider.gameObject.tag);
    if (collider.gameObject.tag == "PlayerAttack") {
      boss.TakeDamage(20);
    }
  }
}