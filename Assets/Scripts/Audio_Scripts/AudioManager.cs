﻿using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
            audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));

        if (PlayerPrefs.HasKey("MusicVol"))
            audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));

        if (PlayerPrefs.HasKey("SFXVol"))
            audioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
    }
}
