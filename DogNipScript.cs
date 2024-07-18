using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNipScript : MonoBehaviour
{
    [SerializeField] private DialogueManager manager;
    void Start()
    {
        if ((bool) manager.GetVariableStateSystem("ate_dognip"))
        {
            //TODO: play an 'on drugs' music and maybe add a color effect to the screen.
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
