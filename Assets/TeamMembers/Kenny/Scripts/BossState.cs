using UnityEngine;

public abstract class BossState {
  public virtual void OnEnterState(Animator animator) {
    Debug.Log("abstract class definition of OnEnterState called");
  }

  public virtual void OnStateUpdate(GameObject gameObject, Animator animator) {
    Debug.Log("abstract class definition of OnStateUpdate called");
  }

  public virtual void OnExitState() {
    Debug.Log("abstract class definition of OnExitState called");
  }
}