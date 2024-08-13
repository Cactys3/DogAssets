using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTracker : MonoBehaviour
{
    private string SceneIndex;
    [SerializeField] SceneTracker sceneTrack;
    [SerializeField] AudioManager audioMan;
    void Start()
    {
        SetSceneIndex();
    }
    private void Update()
    {
        if (!SceneIndex.Equals(SceneManager.GetActiveScene().name))
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case DialogueManager.mudScene:

                    break;
                case DialogueManager.livingScene:

                    break;
                case DialogueManager.bathroomScene:

                    break;
                case DialogueManager.officeScene:

                    break;
                case DialogueManager.kitchenScene:

                    break;
                case DialogueManager.introScene:

                    break;
                case DialogueManager.dognipScene:

                    break;
                case DialogueManager.ending1Scene:

                    break;
                case DialogueManager.ending2Scene:

                    break;
                case DialogueManager.bossScene:

                    break;
                case DialogueManager.demoendScene:

                    break;
                case DialogueManager.lockpickingScene:

                    break;
                case DialogueManager.juicerScene:

                    break;
                case DialogueManager.fridgeovenScene:

                    break;
                case DialogueManager.microwaveScene:

                    break;
            }

            SetSceneIndex();
        }
    }
    private void SetSceneIndex()
    {
        SceneIndex = SceneManager.GetActiveScene().name.ToString();
    }
}
