using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorScriptHorizontal : MonoBehaviour
{
    [SerializeField] bool Vertical;
    [SerializeField] float MaxDistance;
    [SerializeField] float MinDistance;
    [SerializeField] GameObject SelectedVisual;
    [SerializeField] GameObject HoverVisual;
    MicrowaveMinigame wave;
    private bool ObjectHovered;
    private bool ObjectSelected;
    private int ObjectState; //0: game started (disabled), 1: neutral, 2: selected
    // Start is called before the first frame update
    void Start()
    {
        ObjectState = 1;
        SetSelected(false);
        SetHovered(false);
        wave = FindObjectOfType<MicrowaveMinigame>();
        if (wave == null)
        {
            Debug.LogError("couldn't find microwave minigame script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ObjectState);
        //change objectstate if needed
        if (FindObjectOfType<MicrowaveMinigame>().IsGameStarted())
        {
            ChangeObjectState(0); //go into play mode
        }
        else if (ObjectState == 0)
        {
            ChangeObjectState(1); //go out of play mode
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (ObjectState == 2)
            {
                ChangeObjectState(1); //deselect it
            }
            else if (ObjectHovered && ObjectState == 1)
            {
                ChangeObjectState(2); //select it
            }
        }

        //work based on objectstate, maybe do nothing here?
        switch (ObjectState)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:
                //TODO: move it left or right if player clicks and holds and drags left or right (mouse was initially hovering on this object)
                switch (Vertical)
                {

                    case false:
                        float MousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                        if (MousePosX > MinDistance && MousePosX < MaxDistance)
                        {
                            transform.SetPositionAndRotation(new Vector3(MousePosX, transform.position.y, 0), transform.rotation);
                        }
                        break;
                    case true:
                        float MousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                        if (MousePosY > MinDistance && MousePosY < MaxDistance)
                        {
                            transform.SetPositionAndRotation(new Vector3(transform.position.x, MousePosY, 0), transform.rotation);
                        }
                        break;
                }
                break;
            default: 

                break;

        }
    }

    private void ChangeObjectState(int NewState)
    {
        if (NewState == ObjectState)
        {
            Debug.Log("Tried switching reflector object state to what it already was: " + ObjectState);
        }

        //work based on objectstate
        switch (NewState)
        {
            case 0:
                if (ObjectState == 2)
                {
                    SetSelected(false);
                }
                if (ObjectHovered)
                {
                    SetHovered(false);
                }
                ObjectState = 0;
                break;
            case 1:
                if (ObjectState == 2)
                {
                    SetSelected(false);
                }
                ObjectState = 1;
                break;
            case 2:
                SetHovered(false);
                SetSelected(true);
                ObjectState = 2;
                break;
            default:
                Debug.LogError("Reflector script called changeobjectstate with the wrong number (impossible)");
                break;

        }
    }


    private void OnMouseEnter()
    {
        if (ObjectState == 1)
        {
            SetHovered(true);
        }
    }

    private void OnMouseExit()
    {
        if (ObjectState == 1)
        {
            SetHovered(false);
        }
    }
    private void SetSelected(bool b)
    {
        ObjectSelected = b;
        switch (ObjectSelected)
        {
            case true:
                SelectedVisual.SetActive(true);
                break;
            case false:
                SelectedVisual.SetActive(false);
                break;
        }
    }
    private void SetHovered(bool b)
    {
        ObjectHovered = b;
        switch (ObjectHovered)
        {
            case true:
                HoverVisual.SetActive(true);
                break;
            case false:
                HoverVisual.SetActive(false);
                break;
        }
    }
}
