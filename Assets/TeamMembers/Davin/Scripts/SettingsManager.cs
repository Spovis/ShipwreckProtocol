using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance;

    public static SettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SettingsManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SettingsManager");
                    instance = obj.AddComponent<SettingsManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    private SliderBase musicVolumeSlider;
    private SliderBase sfxVolumeSlider;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Find the TextMeshProUGUI components in the scene
        TextMeshProUGUI musicVolumeText = GameObject.Find("MusicVolumeText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sfxVolumeText = GameObject.Find("SFXVolumeText").GetComponent<TextMeshProUGUI>();

        // Initialize the VolumeSliders with the located TextMeshProUGUI components
        musicVolumeSlider = new MusicVolumeSlider(musicVolumeText);
        sfxVolumeSlider = new SFXVolumeSlider(sfxVolumeText);

        // Load settings for both sliders
        musicVolumeSlider.LoadSetting();
        sfxVolumeSlider.LoadSetting();
    }

    // Separate methods for setting volume for music and SFX
    public void SetMusicVolume(float value)
    {
        musicVolumeSlider.Value = value;
        musicVolumeSlider.SaveSetting();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolumeSlider.Value = value;
        sfxVolumeSlider.SaveSetting();
    }

    public float GetMusicVolume() => musicVolumeSlider.Value;
    public float GetSFXVolume() => sfxVolumeSlider.Value;
}
