using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    private SliderBase musicVolumeSlider; // Slider for music volume
    private SliderBase sfxVolumeSlider; // Slider for sound effects volume
    private Slider musicSlider;
    private Slider sfxSlider;

    

    private void Awake()
    {
        

        // Locate the text components in the scene for volume displays
        TextMeshProUGUI musicVolumeText = GameObject.Find("MusicVolumeText").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sfxVolumeText = GameObject.Find("SFXVolumeText").gameObject.GetComponent<TextMeshProUGUI>();

        musicSlider = musicVolumeText.transform.parent.GetComponent<Slider>();
        sfxSlider = sfxVolumeText.transform.parent.GetComponent<Slider>();

        //DYNAMIC BINDING
        // Initialize the volume sliders with the found text components
        musicVolumeSlider = new MusicVolumeSlider(musicVolumeText);
        sfxVolumeSlider = new SFXVolumeSlider(sfxVolumeText);




        // Load saved settings for both volume sliders
        musicVolumeSlider.LoadSetting();
        sfxVolumeSlider.LoadSetting();
    }
    private void OnEnable()
    {
        musicSlider.value = musicVolumeSlider.Value;
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
