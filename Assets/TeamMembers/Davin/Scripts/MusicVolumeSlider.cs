using UnityEngine;
using TMPro;

public class MusicVolumeSlider : SliderBase
{
    private void Start()
    {
        // Initialize the music source volume and update the volume display on start
        AudioManager.Instance.musicSource.volume = value;
        UpdateVolumeText(value);
    }

    // Text component to display the volume value
    private TextMeshProUGUI volumeText;

    // Set the volume text display
    public MusicVolumeSlider(TextMeshProUGUI volumeText)
    {
        this.volumeText = volumeText;
    }

    // Handle volume changes when the slider value is updated
    protected override void OnValueChanged(float value)
    {
        // Update the audio source volume based on the slider value
        AudioManager.Instance.musicSource.volume = value/100;

        // Change the volume text display
        UpdateVolumeText(value);
    }

    // Update the displayed volume percentage
    private void UpdateVolumeText(float value)
    {
        volumeText.text = "Volume: " + $"{Mathf.RoundToInt(value)}" + "%"; // Display as a percentage (0–100)
    }

    // Save the current volume setting to PlayerPrefs
    public override void SaveSetting()
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    // Load the volume setting from PlayerPrefs
    public override void LoadSetting()
    {
        value = PlayerPrefs.GetFloat("MusicVolume", 1.0f); // Default to 1.0 if no setting exists
        UpdateVolumeText(value); // Initialize text display with loaded value
    }
}
