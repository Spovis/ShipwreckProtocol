using UnityEngine;

// For details about WHEN each method is called, see BossState.cs
public class IdleState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.Play("Idle");
  }

  public override void OnStateUpdate(
    GameObject gameObject,
    Animator animator,
    BossStateMachine stateMachine,
    Rigidbody2D fireballPrefab,
    Transform player)
  {
    // if the distance between the player and the boss is less than 10
    if (Vector2.Distance(player.position, gameObject.transform.position) < 20) {
      stateMachine.UpdateState(3, animator);
    }
  }

  public override void OnTriggerEnter2D(Collider2D collider, Boss boss) {
    if (collider.gameObject.tag == "PlayerAttack") {
      boss.TakeDamage(20);
    }
  }
}