using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For details about WHEN each method is called, see BossState.cs
public class AttackState : BossState {
  public override void OnEnterState(Animator animator) {
    animator.SetTrigger("AttackTrigger");
  }

  private bool attacked = false;
  public override void OnStateUpdate(
    GameObject gameObject,
    Animator animator,
    BossStateMachine stateMachine,
    Rigidbody2D fireballPrefab,
    Transform player)
  {
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
      (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) >= 0.2f &&
      !attacked)
    {
      attacked = true;

        AudioManager.Instance.PlayFX("EnemyFireball");

        Rigidbody2D[] fireballs = {
        UnityEngine.Object.Instantiate(fireballPrefab, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody2D,
        UnityEngine.Object.Instantiate(fireballPrefab, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody2D,
        UnityEngine.Object.Instantiate(fireballPrefab, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody2D,
        UnityEngine.Object.Instantiate(fireballPrefab, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody2D
      };

      fireballs[0].GetComponent<Fireball>().SetDirection(new Vector3(-4.0f, 1.0f, 0.0f));
      fireballs[1].GetComponent<Fireball>().SetDirection(new Vector3(4.0f, 1.0f, 0.0f));
      fireballs[2].GetComponent<Fireball>().SetDirection(new Vector3(-4.0f, -1.0f, 0.0f));
      fireballs[3].GetComponent<Fireball>().SetDirection(new Vector3(4.0f, -1.0f, 0.0f));
    }

    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
      (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) < 0.1f)
    {
      attacked = false;
    }

    if (Vector2.Distance(player.position, gameObject.transform.position) > 10) {
      stateMachine.UpdateState(0, animator);
    }
  }

  public override void OnTriggerEnter2D(Collider2D collider, Boss boss) {
    if (collider.gameObject.tag == "PlayerAttack") {
      boss.TakeDamage(20);
    }
  }
}