using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using Unity.VisualScripting;
using Unity.Mathematics;


public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;
    [SerializeField] GameObject AudioPrefab;


    [Header("Sound Effects")]
    public Sound[] MicrowaveMinigame;
    public Sound[] JuicerMinigame;
    public Sound[] FridgeMinigame;
    public Sound[] Boss;
    public Sound[] TabUI;
    public Sound[] OtherUI;
    public Sound[] MiscSounds;
    public Sound[] InteractSounds;
    [Header("Multiple Clips")]
    public AudioClip[] DoorClips;
    public AudioClip[] DogfoodClips;
    public AudioClip[] FridgeClips;
    public AudioClip[] MicrowaveClips;
    public AudioClip[] DrawerClips;
    public AudioClip[] CabinetsClips;
    public AudioClip[] ClosedDoorClips;
    public AudioClip[] JuicerClips;
    public AudioClip[] ValveClips;
    [Header("Type Sounds")]
    public Sound[] TypeSounds;
    [Header("Music Sounds")]
    public Sound[] MusicSounds;

    public AudioSource MusicSource;
    public AudioSource TypeSource;

    private Sound[] SFXSounds;

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
            MiscSounds,
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

		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}
        else if (s.source.isPlaying)
        {
            Debug.LogWarning("Sound: " + sound + " is playing: " + s.source.time);
            return;
        }
        else
        {
            Debug.Log("Sound: " + sound);
        }

        RandomizeClips(); //for sounds that have multiple audioclips

        if (s.volumeVariance + s.pitchVariance != 0)
        {
            float newvolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            float newpitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            if (newvolume > 1)
            {
                newvolume = 1;
            }
            if (newpitch > 3)
            {
                newpitch = 1;
            }
            if (newvolume < 0)
            {
                newvolume = 0;
            }
            if (newpitch < 0.1f)
            {
                newpitch = 0.1f;
            }
            s.source.volume = newvolume;
            s.source.pitch = newpitch;
        }

        Debug.Log(s.source.name);
        s.source.Play();
    }

    public bool PlayingSFX(string sound)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return false;
        }
        else if (s.source.isPlaying)
        {
            return true;
        }
        return false;
    }

    public void PlayMultipleSFX(string sound)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);

        if (s == null || s.source.isPlaying) //TODO: i htink "s.source.isPlaying" is bad or does nothing
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }
        else
        {
            Debug.Log("Sound: " + sound);
        }

        RandomizeClips(); //for sounds that have multiple audioclips

        GameObject soundObject = Instantiate(AudioPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        audioSource.clip = s.clip;
        if (s.volumeVariance + s.pitchVariance != 0)
        {
            float newvolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            float newpitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            if (newvolume > 1)
            {
                newvolume = 1;
            }
            if (newpitch > 3)
            {
                newpitch = 1;
            }
            if (newvolume < 0)
            {
                newvolume = 0;
            }
            if (newpitch < 0.1f)
            {
                newpitch = 0.1f;
            }
            audioSource.volume = newvolume;
            audioSource.pitch = newpitch;
        }
        audioSource.Play();

        Destroy(soundObject, audioSource.clip.length + 1f); // Destroy slightly after the clip ends
    }
    public void SetPitchSFX(string sound, float pitch)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);

        if (s == null)
        {
            Debug.LogError("Sound: " + sound + " not found!");
            return;
        }
        if (pitch <= 3 && pitch >= 0)
        {
            s.source.pitch = pitch;
        }
        else if (pitch > 3)
        {
            pitch = 3;
        }
        else
        {
            pitch = 0;
        }
        s.pitch = pitch;
    }

    public void StopPlayingSFX(string sound)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }
    public void PausePlayingSFX(string sound)
    {
        Sound s = Array.Find(SFXSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Pause();
    }
    // TYPING
    public void PlayTypeSound(string name)
    {
        Sound s = Array.Find(TypeSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Could not find sound: " + name);
        }
        else if (TypeSource.isPlaying)
        {
            Debug.Log("source is playing for: " + name);
        }
        else
        {
            TypeSource.clip = s.clip;
            s.source = TypeSource;

            if (s.volumeVariance + s.pitchVariance != 0)
            {
                float newvolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
                float newpitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

                if (newvolume > 1)
                {
                    newvolume = 1;
                }
                if (newpitch > 3)
                {
                    newpitch = 1;
                }
                if (newvolume < 0)
                {
                    newvolume = 0;
                }
                if (newpitch < 0.1f)
                {
                    newpitch = 0.1f;
                }
                Debug.Log(newvolume + " " + newpitch);
                Debug.Log(s.source.pitch);
                s.source.volume = newvolume;
                s.source.pitch = newpitch;
            }

            TypeSource.Play();
        }
    }
    private void RandomizeClips()
    {
        Sound door = Array.Find(SFXSounds, item => item.name == "door_interact");
        Sound microwave = Array.Find(SFXSounds, item => item.name == "microwave_interact");
        Sound drawer = Array.Find(SFXSounds, item => item.name == "drawer_interact");
        Sound cabinet = Array.Find(SFXSounds, item => item.name == "cabinet_interact");
        Sound closeddoor = Array.Find(SFXSounds, item => item.name == "closed_door_interact");
        Sound fridge = Array.Find(SFXSounds, item => item.name == "fridge_interact");
        Sound valve = Array.Find(SFXSounds, item => item.name == "valve_interact");
        Sound dogfood = Array.Find(SFXSounds, item => item.name == "dogfood_interact");

        door.source.clip = DoorClips[UnityEngine.Random.Range(0, DoorClips.Length)];
        microwave.source.clip = MicrowaveClips[UnityEngine.Random.Range(0, MicrowaveClips.Length)];
        drawer.source.clip = DrawerClips[UnityEngine.Random.Range(0, DrawerClips.Length)];
        cabinet.source.clip = CabinetsClips[UnityEngine.Random.Range(0, CabinetsClips.Length)];
        closeddoor.source.clip = ClosedDoorClips[UnityEngine.Random.Range(0, ClosedDoorClips.Length)];
        fridge.source.clip = FridgeClips[UnityEngine.Random.Range(0, FridgeClips.Length)];
        valve.source.clip = ValveClips[UnityEngine.Random.Range(0, ValveClips.Length)];
        dogfood.source.clip = DogfoodClips[UnityEngine.Random.Range(0, DogfoodClips.Length)];
    }
    public void PlayMultipleType(string sound)
    {
        Sound s = Array.Find(TypeSounds, item => item.name == sound);

        if (s == null)
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
        if (s.volumeVariance + s.pitchVariance != 0)
        {
            float newvolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            float newpitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            if (newvolume > 1)
            {
                newvolume = 1;
            }
            if (newpitch > 3)
            {
                newpitch = 1;
            }
            if (newvolume < 0)
            {
                newvolume = 0;
            }
            if (newpitch < 0.1f)
            {
                newpitch = 0.1f;
            }
            audioSource.volume = newvolume;
            audioSource.pitch = newpitch;
        }
        audioSource.Play();

        Destroy(soundObject, audioSource.clip.length + 1f); // Destroy slightly after the clip ends
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
            s.source = MusicSource;

            if (s.volumeVariance + s.pitchVariance != 0)
            {
                float newvolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
                float newpitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

                if (newvolume > 1)
                {
                    newvolume = 1;
                }
                if (newpitch > 3)
                {
                    newpitch = 1;
                }
                if (newvolume < 0)
                {
                    newvolume = 0;
                }
                if (newpitch < 0.1f)
                {
                    newpitch = 0.1f;
                }
                s.source.volume = newvolume;
                s.source.pitch = newpitch;
            }

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
