using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For details about WHEN each method is called, see BossState.cs
public class DeathState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.SetTrigger("IsDead");
  }

  public override void OnStateUpdate(
    GameObject gameObject,
    Animator animator,
    BossStateMachine stateMachine,
    Rigidbody2D fireballPrefab,
    Transform player)
  {
      // Check if the "IsDead" trigger is set and the Death animation has completed
      if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
        animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
      {
        UnityEngine.Object.Destroy(gameObject);
      }
   }
}