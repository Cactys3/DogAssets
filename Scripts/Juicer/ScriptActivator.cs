using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    [SerializeField] JuicerMinigameManager manager;
    [Header("0: Juice!, 1: 2x, 2: 8x, 3: 512x")]
    [SerializeField] int Button;
    private bool Hovered;

    void Start()
    {
        Hovered = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Hovered && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("clicked " + Button);
            manager.ButtonClicked(Button);
        }
    }
    private void OnMouseEnter()
    {

        //Debug.Log("mouse " + index +" " + Button);

        Hovered = true;

    }
    private void OnMouseExit()
    { 
        //Debug.Log("demouse " + index + " " + Button);

        Hovered = false;

    }
}
