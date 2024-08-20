using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    private static SceneTracker instance;
    private string LastSceneOnStart; //Is the last scene if called after "Start()", meaning the update method as ran
    private string LastSceneOnAwake; //Is the last scene before the update method has run, i.e. on "Awake()"
    void Awake()
    {
        LastSceneOnAwake = SceneManager.GetActiveScene().name;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals(LastSceneOnAwake))
        {
            LastSceneOnStart = LastSceneOnAwake;
            LastSceneOnAwake = SceneManager.GetActiveScene().name;
            Debug.Log("updated current scene to: " + LastSceneOnAwake + " and past scene to: " + LastSceneOnStart);
        }
    }
    public string GetLastSceneOnStart()
    {
        return LastSceneOnStart;
    }
    public string GetLastSceneOnAwake()
    {
        return LastSceneOnAwake;
    }
    public static SceneTracker GetInstance()
    {
        return instance;
    }
}
