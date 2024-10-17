using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine {
  public string GetCurrentState(Boss boss, Transform player) {
    if (boss.GetHealth() == 0) {
      return "Dead";
    } else
      return "Alive";
  }
}