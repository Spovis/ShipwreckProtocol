using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Yarn : Items
{
    public static event Action OnYarnTouched;

    private float _scoreIncrement = 5f;

    public override void Collect()
    {
        AddToPlayerInventory();
        GameManager.Instance.Score += _scoreIncrement;
        AudioManager.Instance.PlayFX("Yarn");

        OnYarnTouched?.Invoke();

        Destroy(gameObject);
    }
}