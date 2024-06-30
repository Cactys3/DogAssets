using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeToScene : MonoBehaviour
{
    public SceneName Scene = new SceneName();
    [SerializeField] private int Level;


    public void ChangeScene()
    {
        switch (Scene)
        {
            case SceneName.Intro:
                SceneManager.LoadScene("Intro Animation");
                break;
            case SceneName.Mud:
                SceneManager.LoadScene("Mud Room");
                break;
            case SceneName.Lockpicking:
                SceneManager.LoadScene("Lockpicking Intro");
                break;
            case SceneName.DogNip:
                SceneManager.LoadScene("DogNip Animation");
                break;
            case SceneName.KitchenDining:
                SceneManager.LoadScene("Kitchen Dining Room");
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
                SceneManager.LoadScene("Juicer Minigame");
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
                SceneManager.LoadScene("Living Room");
                break;
            case SceneName.Office:
                SceneManager.LoadScene("Office Room");
                break;
            case SceneName.Bathroom1:
                SceneManager.LoadScene("Bathroom 1");
                break;
            case SceneName.DemoEnd:
                SceneManager.LoadScene("Demo End");
                break;
            default:
                Debug.LogError("ChangeToScene on object: " + this.gameObject.name + ", is not setup properly");
                break;
        }

    }
}

public enum SceneName
{
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
    DemoEnd
};
