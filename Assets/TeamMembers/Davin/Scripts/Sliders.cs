using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SliderBase
{
    protected float value;

    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanged(value);
        }
    }

    protected abstract void OnValueChanged(float value);

    public abstract void LoadSetting();
    public abstract void SaveSetting();
}

