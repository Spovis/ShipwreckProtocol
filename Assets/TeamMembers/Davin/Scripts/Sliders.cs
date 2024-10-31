using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract base class for slider implementations
public abstract class SliderBase
{
    // Protected field to store the slider's current value
    protected float value;

    // Property for accessing and modifying the slider value
    public float Value
    {
        get => value; // Getter for the value
        set
        {
            this.value = value; // Set the value
            OnValueChanged(value); // Notify derived classes of the change
        }
    }

    // Abstract method to handle actions when the value changes
    protected abstract void OnValueChanged(float value);

    // Abstract methods for loading and saving slider settings
    public abstract void LoadSetting();
    public abstract void SaveSetting();
}
