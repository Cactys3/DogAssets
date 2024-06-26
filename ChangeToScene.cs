using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeToScene : MonoBehaviour
{
    public MyEnum Scene = new MyEnum();
    [SerializeField] private int Level;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            switch(Scene)
            {
                case MyEnum.Intro:
                    SceneManager.LoadScene("Intro Animation");
                    break;
                case MyEnum.Mud:
                    SceneManager.LoadScene("Mud Room");
                    break;
                case MyEnum.Lockpicking:
                    SceneManager.LoadScene("Lockpicking Intro");
                    break;
                case MyEnum.DogNip:
                    SceneManager.LoadScene("DogNip Animation");
                    break;
                case MyEnum.KitchenDining:
                    SceneManager.LoadScene("Kitchen Dining Room");
                    break;
                case MyEnum.Microwave:
                    if (Level > 0 && Level <= 5)
                    {
                        SceneManager.LoadScene("Microwave " + Level.ToString());
                    }
                    else
                    {
                        Debug.LogError("Tried Going To Microwave Level: " + Level + ". That isn't a valid level (or you need to reset ChangeToScene script");
                    }
                    break;
                case MyEnum.Juicer:
                    SceneManager.LoadScene("Juicer Minigame");
                    break;
                case MyEnum.FridgeOven:
                    if (Level > 0 && Level <= 2)
                    {
                        SceneManager.LoadScene("Fridge Level " + Level.ToString());
                    }
                    else
                    {
                        Debug.LogError("Tried Going To FridgeOven Level: " + Level + ". That isn't a valid level (or you need to reset ChangeToScene script");
                    }
                    break;
                case MyEnum.Living:
                    SceneManager.LoadScene("Living Room");
                    break;
                case MyEnum.Office:
                    SceneManager.LoadScene("Office Room");
                    break;
                case MyEnum.Bathroom1:
                    SceneManager.LoadScene("Bathroom 1");
                    break;
                default:
                    Debug.LogError("ChangeToScene on object: " + this.gameObject.name + ", is not setup properly");
                    break;
            }
        }
    }
}

public enum MyEnum
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
    Bathroom1
};
