using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for slider implementations
public class SliderBase
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

    // Virtual method to handle actions when the value changes
    protected virtual void OnValueChanged(float value)
    {
        Debug.Log("Value changed to: " + value);
    }

    // Virtual methods for loading and saving slider settings
    public virtual void LoadSetting()
    {
        value = 0f;
        Debug.Log("LoadSetting called in base class");
    }

    public virtual void SaveSetting()
    {
        Debug.Log("SaveSetting called in base class");
    }
}
