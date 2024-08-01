using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;
    [SerializeField] GameObject AudioPrefab;

    [Header("Sound Effects")]
    private Sound[] SFXSounds;
    public Sound[] MicrowaveMinigame;
    public Sound[] JuicerMinigame;
    public Sound[] FridgeMinigame;
    public Sound[] Boss;
    public Sound[] TabUI;
    public Sound[] OtherUI;
    public Sound[] SceneSounds;
    public Sound[] InteractSounds;
    [Header("Type Sounds")]
    public Sound[] TypeSounds;
    [Header("Music Sounds")]
    public Sound[] MusicSounds;

    public AudioSource MusicSource;
    public AudioSource TypeSource;

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
        //setup the SFXSounds to be all the sfx sound lists combined
        List<Sound[]> listOfLists = new List<Sound[]>
        {
            MicrowaveMinigame,
            JuicerMinigame,
            FridgeMinigame,
            Boss,
            TabUI,
            OtherUI,
            SceneSounds,
            InteractSounds
        };
        int totalLength = 0;
        foreach (Sound[] s in listOfLists)
        {
            totalLength += s.Length;
        }
        SFXSounds = new Sound[totalLength];
        int OutsideIndex = 0;
        foreach (Sound[] s in listOfLists)
        {
            foreach (Sound S in s)
            {
                SFXSounds[OutsideIndex] = S;
                OutsideIndex++;
            }
        }
        foreach (Sound s in SFXSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    public void PlaySFX(string sound)
	{
		Sound s = Array.Find(SFXSounds, item => item.name == sound);

		if (s == null || s.source.isPlaying)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}
        else
        {
            Debug.Log("Sound: " + sound);
        }

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
    }

    public void PlayMultipleSFX(string sound)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);

        if (s == null || s.source.isPlaying)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }
        else
        {
            Debug.Log("Sound: " + sound);
        }
        GameObject soundObject = Instantiate(AudioPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        audioSource.clip = s.clip;
        audioSource.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        audioSource.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        audioSource.Play();

        Destroy(soundObject, audioSource.clip.length + 1f); // Destroy slightly after the clip ends
    }

    public void StopPlayingSFX(string sound)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

    // TYPING
    public void PlayTypeSound(string name)
    {
        Sound s = Array.Find(TypeSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Could not find sound: " + name);
        }
        else
        {
            TypeSource.clip = s.clip;
            TypeSource.Play();
        }
    }
    public void StopTypeSound()
    {
        TypeSource.Stop();
    }
    // MUSIC
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Could not find sound: " + name);
        }
        else
        {
            MusicSource.clip = s.clip;
            MusicSource.Play();
        }
    }
    public void StopMusic()
    {
        MusicSource.Stop();
    }
    public void SetPauseMusic(bool b)
    {
        if (b)
        {
            MusicSource.Pause();
        }
        else
        {
            MusicSource.UnPause();
        }
    }
    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
    }
    public void MusicVolume(float v)
    {
        if (v >= 0 && v <= 1)
        {
            MusicSource.volume = v;
        }
    }
}
