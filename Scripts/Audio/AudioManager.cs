using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;

	public Sound[] SFXSounds;
    public Sound[] TypeSounds;
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
		//	Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
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
