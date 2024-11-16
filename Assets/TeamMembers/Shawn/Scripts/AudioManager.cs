using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSound, fxSound;
    public AudioSource musicSource, fxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Music");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSound, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Can't Find Music");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }


    public void PlayFX(string name)
    {
        Sound sound = Array.Find(fxSound, x => x.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Can't Find FX Sound of " + name);
        }
        else
        {
            fxSource.PlayOneShot(sound.clip);
        }
    }
}
