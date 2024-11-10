using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serves as the middle-man between the Boss and its states. You should never
// directly initialize any of the state classes outside of this class
public class BossStateMachine {
  // static binding
  private static BossState[] possibleStates = new BossState[] {
    new IdleState(),
    new DeathState(),
    new DamageState(),
    new AttackState(),
  };
  private BossState state = possibleStates[0];

  public BossState GetState() {
    return state;
  }

  public void UpdateState(int stateIndex, Animator animator) {
    if (possibleStates[stateIndex] != state) {
      state.OnExitState(animator);
      state = possibleStates[stateIndex];
      state.OnEnterState(animator);
    }
  }
}