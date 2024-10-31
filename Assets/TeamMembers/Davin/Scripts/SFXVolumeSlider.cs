using UnityEngine;
using TMPro;

public class SFXVolumeSlider : SliderBase
{
    // Text component to display the volume value
    private TextMeshProUGUI volumeText;

    // Set the volume text display
    public SFXVolumeSlider(TextMeshProUGUI volumeText)
    {
        this.volumeText = volumeText;
    }

    // Handle volume changes when the slider value is updated
    protected override void OnValueChanged(float value)
    {
        // Update the audio source volume for sound effects
        AudioManager.Instance.fxSource.volume = value;

        // Refresh the volume text display
        UpdateVolumeText(value);
    }

    // Update the displayed volume percentage
    private void UpdateVolumeText(float value)
    {

        volumeText.text = "Volume: " + $"{Mathf.RoundToInt(value * 100)}" + "%"; // Display as a percentage (0–100)
    }

    // Save the current SFX volume setting to PlayerPrefs
    public override void SaveSetting()
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    // Load the SFX volume setting from PlayerPrefs
    public override void LoadSetting()
    {
        value = PlayerPrefs.GetFloat("SFXVolume", 1.0f); // Default to 1.0 if no setting exists
        UpdateVolumeText(value); // Initialize text display with loaded value
    }
}
