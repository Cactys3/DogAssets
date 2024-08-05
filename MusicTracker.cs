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
                case ChangeToScene.MudScene:

                    break;
                case ChangeToScene.LivingRoomScene:

                    break;
                case ChangeToScene.BathroomScene:

                    break;
                case ChangeToScene.OfficeScene:

                    break;
                case ChangeToScene.KitchenScene:

                    break;
                case ChangeToScene.IntroScene:

                    break;
                case ChangeToScene.DogNipScene:

                    break;
                case ChangeToScene.Ending1Scene:

                    break;
                case ChangeToScene.Ending2Scene:

                    break;
                case ChangeToScene.BossScene:

                    break;
                case ChangeToScene.DemoEndScene:

                    break;
                case ChangeToScene.LockpickingScene:

                    break;
                case ChangeToScene.JuicerLevelOne:

                    break;
                case ChangeToScene.FridgeLevelOne:

                    break;
                case ChangeToScene.MicrowaveLevelOne:

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
