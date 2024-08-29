using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTracker : MonoBehaviour
{
    private string SceneIndex;
    [SerializeField] SceneTracker sceneTrack;
    [SerializeField] AudioManager audioMan;
    public const string RoomMusic = "room_music";
    public const string JuicerMusic = "juicer_music";
    public const string MicrowaveMusic = "microwave_music";
    public const string FridgeOvenMusic = "fridgeoven_music";
    public const string EndMusic = "end_music";
    public const string BossMusic = "boss_music";
    public const string DognipMusic = "dognip_music";
    public const string NoMusic = "no_music";
    public const string PostDognipMusic = "room_music";
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
                    //SetMusic(DognipMusic); DONT SET MUSIC - BECAUSE ITS PART OF THE ANIMATION
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
                    SetMusic(RoomMusic);  //(JuicerMusic);
                    break;
                case DialogueManager.fridgeovenScene:
                    SetMusic(FridgeOvenMusic);
                    break;
                case DialogueManager.microwaveScene:
                    SetMusic(MicrowaveMusic);
                    break;
                case DialogueManager.endchoiceScene:
                    SetMusic(EndMusic);
                    break;
                case DialogueManager.creditScene:
                    SetMusic(RoomMusic);
                    break;
                default:
                    //do nothing because this is probably a minigame
                    Debug.Log("set no music because scene: " + SceneManager.GetActiveScene().name);
                    break;
            }

            SetSceneIndex();
        }
    }
    private void SetSceneIndex()
    {
        SceneIndex = SceneManager.GetActiveScene().name.ToString();
    }

    public void SetMusic(string s)
    {
        Debug.Log("Set Music: " + s + " becuase scene: " + SceneManager.GetActiveScene().name.ToString());
        if (!audioMan.PlayingMusic(s))
        {
            audioMan.PlayMusic(s);
        }
    }
}
