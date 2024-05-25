using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorScript : MonoBehaviour
{

    private int ObjectState; //0: game started (disabled), 1: neutral, 2: hovered 3: selected
    // Start is called before the first frame update
    void Start()
    {
        ObjectState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //change objectstate if needed
        if (FindObjectOfType<MicrowaveMinigame>().IsGameStarted())
        {
            ChangeObjectState(0);
        }
        else if (ObjectState == 0)
        {
            ChangeObjectState(1);
        }

        //work based on objectstate, maybe do nothing here?
        switch (ObjectState)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            default:

                break;

        }
    }

    private void ChangeObjectState(int NewState)
    {
        if (NewState == ObjectState)
        {
            //what?
        }

        //work based on objectstate
        switch (NewState)
        {
            case 0:
                if (ObjectState == 3)
                {
                    //need to disable selected state
                }
                if (ObjectState == 2)
                {
                    //need to disable hovered state
                }
                ObjectState = 0;
                break;
            case 1:
                if (ObjectState == 3)
                {
                    //need to disable selected state
                }
                if (ObjectState == 2)
                {
                    //need to disable hovered state
                }
                break;
            case 2:
                if (ObjectState == 3 || ObjectState == 2)
                {
                    //if we call to hover this object when it's already hovered or selected, don't need to do anything
                    break;
                }
                break;
            case 3:
                ObjectState = 3;
                break;
            default:

                break;

        }
    }

}
