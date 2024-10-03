using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventManager : MonoBehaviour
{
    public void EndAttack() {
        PlayerStateMachine.Instance.SwitchState(PlayerStates.Idle);
    }
}
