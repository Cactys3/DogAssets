using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeToScene : MonoBehaviour
{
    public SceneName Scene = new SceneName();
    [SerializeField] private int Level;
    [SerializeField] private bool ChangeSceneOnEnabled;

    private void OnEnable()
    {
        if (ChangeSceneOnEnabled)
        {
            ChangeScene();
        }
    }
    [SerializeField] public void ChangeScene()
    {
        switch (Scene)
        {
            case SceneName.Title:
                SceneManager.LoadScene(DialogueManager.titleScene);
                break;
            case SceneName.Intro:
                SceneManager.LoadScene(DialogueManager.introScene);
                break;
            case SceneName.Mud:
                SceneManager.LoadScene(DialogueManager.mudScene);
                break;
            case SceneName.Lockpicking:
                SceneManager.LoadScene(DialogueManager.lockpickingScene);
                break;
            case SceneName.DogNip:
                SceneManager.LoadScene(DialogueManager.dognipScene);
                break;
            case SceneName.KitchenDining:
                SceneManager.LoadScene(DialogueManager.kitchenScene);
                break;
            case SceneName.Microwave:
                if (Level > 0 && Level <= 5)
                {
                    SceneManager.LoadScene("Microwave " + Level.ToString());
                }
                else
                {
                    Debug.LogError("Tried Going To Microwave Level: " + Level + ". That isn't a valid level (or you need to reset ChangeToScene script");
                }
                break;
            case SceneName.Juicer:
                SceneManager.LoadScene(DialogueManager.juicerScene);
                break;
            case SceneName.FridgeOven:
                if (Level > 0 && Level <= 4)
                {
                    SceneManager.LoadScene("Fridge Level " + Level.ToString());
                }
                else
                {
                    Debug.LogError("Tried Going To FridgeOven Level: " + Level + ". That isn't a valid level (or you need to reset ChangeToScene script");
                }
                break;
            case SceneName.Living:
                SceneManager.LoadScene(DialogueManager.livingScene);
                break;
            case SceneName.Office:
                SceneManager.LoadScene(DialogueManager.officeScene);
                break;
            case SceneName.Bathroom1:
                SceneManager.LoadScene(DialogueManager.bathroomScene);
                break;
            case SceneName.DemoEnd:
                SceneManager.LoadScene(DialogueManager.demoendScene);
                break;
            case SceneName.LoadInk:
                SceneManager.LoadScene("Load Managers");
                break;
            case SceneName.Ending1:
                SceneManager.LoadScene(DialogueManager.ending1Scene);
                break;
            case SceneName.Ending2:
                SceneManager.LoadScene(DialogueManager.ending2Scene);
                break;
            case SceneName.Boss:
                SceneManager.LoadScene(DialogueManager.bossScene);
                break;
            default:
                Debug.LogError("ChangeToScene on object: " + this.gameObject.name + ", is not setup properly");
                break;
        }

    }
}

public enum SceneName
{
    Title,
    Intro,
    Mud,
    Lockpicking,
    DogNip,
    KitchenDining,
    Microwave,
    Juicer,
    FridgeOven,
    Living,
    Office,
    Bathroom1,
    DemoEnd,
    LoadInk,
    Ending1,
    Ending2,
    Boss
};
