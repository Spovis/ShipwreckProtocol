using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.SetTrigger("DamageTrigger");
  }

  public override void OnStateUpdate(GameObject gameObject, Animator animator, BossStateMachine stateMachine) {
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") &&
      animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
    {
      stateMachine.UpdateState(0, animator);
    }
  }

  public override void OnExitState(Animator animator) { }

  public override void OnTriggerEnter2D(Collider2D collider, Boss boss) {
    Debug.Log("DamageState.OnTriggerEnter2D called");
  }
}