using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    [SerializeField] JuicerMinigameManager manager;
    [Header("1: 2x, 2: 8x, 3: 512x, 4: Juice!")]
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
            Debug.Log("clicked " + Button);
            manager.ButtonClicked(Button);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("cursor"))
        {
            Hovered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("cursor"))
        {
            Hovered = false;
        }
    }
}
