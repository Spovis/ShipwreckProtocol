using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For details about WHEN each method is called, see BossState.cs
public class DamageState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.SetTrigger("DamageTrigger");
  }

  public override void OnStateUpdate(
    GameObject gameObject,
    Animator animator,
    BossStateMachine stateMachine,
    Rigidbody2D fireballPrefab,
    Transform player)
  {
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") &&
      animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
    {
      stateMachine.UpdateState(0, animator);
    }
  }
}