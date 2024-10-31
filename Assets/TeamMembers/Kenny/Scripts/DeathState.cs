using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.SetTrigger("IsDead");
  }

  public override void OnStateUpdate(GameObject gameObject, Animator animator) {
      // Check if the "IsDead" trigger is set and the Death animation has completed
      if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
        animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
      {
        UnityEngine.Object.Destroy(gameObject);
      }
   }

  public override void OnExitState() { }
}