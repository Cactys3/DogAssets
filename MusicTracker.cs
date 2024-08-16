using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTracker : MonoBehaviour
{
    private string SceneIndex;
    [SerializeField] SceneTracker sceneTrack;
    [SerializeField] AudioManager audioMan;
    private const string RoomMusic = "room_music";
    private const string JuicerMusic = "juicer_music";
    private const string MicrowaveMusic = "microwave_music";
    private const string FridgeOvenMusic = "fridgeoven_music";
    private const string EndMusic = "end_music";
    private const string BossMusic = "boss_music";
    private const string DognipMusic = "dognip_music";
    private const string NoMusic = "no_music";
    void Start()
    {
        SceneIndex = "defaulttt3t3";
    }
    private void Update()
    {
        if (!SceneIndex.Equals(SceneManager.GetActiveScene().name))
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case DialogueManager.mudScene:
                    SetMusic(RoomMusic);
                    break;
                case DialogueManager.livingScene:
                    SetMusic(RoomMusic);
                    break;
                case DialogueManager.bathroomScene:
                    SetMusic(RoomMusic);
                    break;
                case DialogueManager.officeScene:
                    SetMusic(RoomMusic);
                    break;
                case DialogueManager.kitchenScene:
                    SetMusic(RoomMusic);
                    break;
                case DialogueManager.introScene:
                    SetMusic(NoMusic);
                    break;
                case DialogueManager.dognipScene:
                    SetMusic(DognipMusic);
                    break;
                case DialogueManager.ending1Scene:
                    SetMusic(EndMusic);
                    break;
                case DialogueManager.ending2Scene:
                    SetMusic(EndMusic);
                    break;
                case DialogueManager.bossScene:
                    SetMusic(BossMusic);
                    break;
                case DialogueManager.demoendScene:
                    SetMusic(EndMusic);
                    break;
                case DialogueManager.lockpickingScene:
                    SetMusic(NoMusic);
                    break;
                case DialogueManager.juicerScene:
                    SetMusic(JuicerMusic);
                    break;
                case DialogueManager.fridgeovenScene:
                    SetMusic(FridgeOvenMusic);
                    break;
                case DialogueManager.microwaveScene:
                    SetMusic(MicrowaveMusic);
                    break;
                default:
                    SetMusic(NoMusic);
                    break;
            }

            SetSceneIndex();
        }
    }
    private void SetSceneIndex()
    {
        SceneIndex = SceneManager.GetActiveScene().name.ToString();
    }

    private void SetMusic(string s)
    {
        Debug.Log("Set Music: " + s);
        if (!audioMan.PlayingMusic(s))
        {
            audioMan.PlayMusic(s);
        }
    }
}
