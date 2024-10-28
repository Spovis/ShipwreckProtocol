using UnityEngine;
using TMPro;

public class SFXVolumeSlider : SliderBase
{
    private TextMeshProUGUI volumeText; // Text component to display the volume value

    public SFXVolumeSlider(TextMeshProUGUI volumeText)
    {
        this.volumeText = volumeText;
    }

    protected override void OnValueChanged(float value)
    {
        // Apply volume change, e.g., through an AudioManager
        AudioManager.Instance.fxSource.volume = value;

        // Update the text display
        UpdateVolumeText(value);
    }

    private void UpdateVolumeText(float value)
    {
        volumeText.text = "Volume: " + $"{Mathf.RoundToInt(value * 100)}" + "%"; // Display as a percentage (0�100)
    }

    public override void SaveSetting()
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public override void LoadSetting()
    {
        value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        UpdateVolumeText(value); // Initialize text display with loaded value
    }
}
