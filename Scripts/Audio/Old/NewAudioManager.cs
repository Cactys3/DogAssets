using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewAudioManager : MonoBehaviour
{
    //Credit to Rehope Games on youtube for tutorials on Audio in Unity
    public static NewAudioManager Instance;

    public Sound[] MusicSounds, sfxSounds;
    public AudioSource MusicSource, sfxSource;

    private List<AudioSource> sfxList = new List<AudioSource>();
    private Dictionary<string, AudioSource> playingSFX = new Dictionary<string, AudioSource>();

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
        foreach (Sound s in sfxSounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sfxList.Add(source);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.name == name);
        if (s==null)
        {
            Debug.Log("Could not find sound: " + name);
        }
        else
        {
            MusicSource.clip = s.clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Could not find sound: " + name);
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
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

    public void SetPauseSFX(bool b)
    {
        if (b)
        {
            sfxSource.Pause();
        }
        else
        {
            sfxSource.UnPause();
        }
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float v)
    {
        if (v >= 0 && v <= 1)
        {
            MusicSource.volume = v;
        }
    }

    public void SFXVolume(float v)
    {
        if (v >= 0 && v <= 1)
        {
            sfxSource.volume = v;
        }
    }
}
