using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private Transform Spawn1;
    [SerializeField] private Transform Spawn2;
    [SerializeField] private Transform Spawn3;
    [SerializeField] private Transform Spawn4;
    [SerializeField] private bool MultipleSpawns;
    private SceneTracker tracker;
    private Transform player;
    private void Awake()
    {
        try
        {
            tracker = SceneTracker.GetInstance();
        }
        catch
        {
            Debug.LogWarning("SpawnPositionScript Could not find scenetracker instance");
        }

        player = this.transform;

        //default spawn
        player.position = Spawn1.position;

        //if multiple spawnpoints
        if (MultipleSpawns)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Mud Room":
                    switch (tracker.GetLastSceneOnAwake())
                    {
                        case "Load Managers":
                            player.position = Spawn1.position;
                            break;
                        case "Kitchen Dining Room":
                            player.position = Spawn2.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                        case "Outside":
                            player.position = Spawn3.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                    }
                    break;
                case "Kitchen Dining Room":
                    switch (tracker.GetLastSceneOnAwake())
                    {
                        case "Mud Room":
                            player.position = Spawn1.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                        case "Living Room":
                            player.position = Spawn2.position; 
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                        case "dognip":
                            player.position = Spawn4.position;
                            break;
                        default:
                            player.position = Spawn3.position; //this is for minigames
                            break;
                    }
                    break;
                case "Living Room":
                    switch (tracker.GetLastSceneOnAwake())
                    {
                        case "Kitchen Dining Room":
                            player.position = Spawn1.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                        case "Office Room":
                            player.position = Spawn2.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                        case "Bathroom 1":
                            player.position = Spawn3.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                        case "Closed Door":
                            player.position = Spawn4.position;
                            FindObjectOfType<AudioManager>().PlaySFX("door_interact_exit");
                            break;
                    }
                    break;
                default:
                    Debug.Log("didn't find spawn point: " + tracker.GetLastSceneOnAwake());
                    break;
            }
        }
    }
}
