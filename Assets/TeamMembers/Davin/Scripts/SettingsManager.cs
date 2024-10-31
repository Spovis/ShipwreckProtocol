using UnityEngine; 
using TMPro; 

public class SettingsManager : MonoBehaviour
{
    // Static instance for the singleton pattern
    private static SettingsManager instance;

    // Static property to get the singleton instance
    public static SettingsManager Instance
    {
        get
        {
            // Create a new instance if one does not exist
            if (instance == null)
            {
                instance = FindObjectOfType<SettingsManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SettingsManager");
                    instance = obj.AddComponent<SettingsManager>();
                    DontDestroyOnLoad(obj); // Keep this object across scenes
                }
            }
            return instance; // Return the existing or new instance
        }
    }

    private SliderBase musicVolumeSlider; // Slider for music volume
    private SliderBase sfxVolumeSlider; // Slider for sound effects volume

    private void Awake()
    {
        // Ensure only one instance of SettingsManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; // Set this instance as the singleton
        DontDestroyOnLoad(gameObject); // Persist this object across scenes

        // Locate the text components in the scene for volume displays
        TextMeshProUGUI musicVolumeText = GameObject.Find("MusicVolumeText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sfxVolumeText = GameObject.Find("SFXVolumeText").GetComponent<TextMeshProUGUI>();

        // Initialize the volume sliders with the found text components
        musicVolumeSlider = new MusicVolumeSlider(musicVolumeText);
        sfxVolumeSlider = new SFXVolumeSlider(sfxVolumeText);

        // Load saved settings for both volume sliders
        musicVolumeSlider.LoadSetting();
        sfxVolumeSlider.LoadSetting();
    }

    // Method to set the music volume and save the setting
    public void SetMusicVolume(float value)
    {
        musicVolumeSlider.Value = value;
        musicVolumeSlider.SaveSetting();
    }

    // Method to set the sound effects volume and save the setting
    public void SetSFXVolume(float value)
    {
        sfxVolumeSlider.Value = value;
        sfxVolumeSlider.SaveSetting();
    }

    // Getter for the current music volume
    public float GetMusicVolume() => musicVolumeSlider.Value;

    // Getter for the current sound effects volume
    public float GetSFXVolume() => sfxVolumeSlider.Value;
}
