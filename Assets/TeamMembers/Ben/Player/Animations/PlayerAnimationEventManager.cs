using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventManager : MonoBehaviour
{
    public void EndAttack() {
        PlayerStateMachine.Instance.SwitchState(PlayerStates.Idle);
    }

    public void StepSound()
    {
        AudioManager.Instance.PlayFX("Step");
    }

    public void WaterStepSound()
    {
        AudioManager.Instance.PlayFX("WaterStep");
    }
}
