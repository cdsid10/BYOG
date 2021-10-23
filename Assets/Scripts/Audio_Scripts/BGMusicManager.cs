using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
	public string soundName;

	public AudioClip audioClip;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public bool playOnAwake = false;
	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource bGMusicAudioSource;
}

public class BGMusicManager : MonoBehaviour
{
	public static BGMusicManager instance;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
            Destroy(gameObject);
        }
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

        foreach (Sound sound in sounds)
        {
            sound.bGMusicAudioSource = gameObject.AddComponent<AudioSource>();

            sound.bGMusicAudioSource.clip = sound.audioClip;
            sound.bGMusicAudioSource.playOnAwake = sound.playOnAwake;
            sound.bGMusicAudioSource.loop = sound.loop;

            sound.bGMusicAudioSource.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    private void Start()
    {
        PlaySound("Main_Menu_BG_Music");
    }

	public void PlaySound(string sound)
	{
		Sound soundToPlay = Array.Find(sounds, item => item.soundName == sound);
		if (soundToPlay == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

        soundToPlay.bGMusicAudioSource.volume = soundToPlay.volume * (1f + UnityEngine.Random.Range(-soundToPlay.volumeVariance / 2f, soundToPlay.volumeVariance / 2f));
        soundToPlay.bGMusicAudioSource.pitch = soundToPlay.pitch * (1f + UnityEngine.Random.Range(-soundToPlay.pitchVariance / 2f, soundToPlay.pitchVariance / 2f));

        soundToPlay.bGMusicAudioSource.Play();
    }

	public void StopSound(string sound)
	{
		Sound soundToStop = Array.Find(sounds, item => item.soundName == sound);
		if (soundToStop == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

        soundToStop.bGMusicAudioSource.volume = soundToStop.volume * (1f + UnityEngine.Random.Range(-soundToStop.volumeVariance / 2f, soundToStop.volumeVariance / 2f));
        soundToStop.bGMusicAudioSource.pitch = soundToStop.pitch * (1f + UnityEngine.Random.Range(-soundToStop.pitchVariance / 2f, soundToStop.pitchVariance / 2f));

        soundToStop.bGMusicAudioSource.Stop();
    }
}
