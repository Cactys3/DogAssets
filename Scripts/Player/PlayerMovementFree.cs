using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFree : MonoBehaviour
{
    private bool WalkSoundPlaying;
    private bool wasHorizontal;
    private bool horizontal;
    private bool vertical;
    private float MoveSpeed;
    public LayerMask StopsMovement;
    void Start()
    {
        MoveSpeed = 3f;
        horizontal = false;
        vertical = false;
        wasHorizontal = false;
        WalkSoundPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        //can't move during dialogue
        if (FindObjectOfType<DialogueManager>().dialogueIsPlaying || FindObjectOfType<ManageUI>().DisplayingUI()) //DialogueManager.GetInstance().dialogueIsPlaying
        {
            FindObjectOfType<AudioManager>().PausePlayingSFX("dog_footsteps");
            WalkSoundPlaying = false;
            return;
        }

        if (true) 
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapBox(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.05f, 0, 0), new Vector2(1f, 1f), 0, StopsMovement))
                {
                    horizontal = true;
                    //move horizontal animation
                }
                else
                {
                    horizontal = false;
                    //bump into collider animation horizontal
                }
            }
            else
            {
                horizontal = false;
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                //One Block Wide Player:if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.9f, 0), new Vector2(0.9f, 0.9f), 0, StopsMovement))
                //Two Block Wide Player:if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.9f, 0), new Vector2(1.9f, 0.9f), 0, StopsMovement))
                if (!Physics2D.OverlapBox(transform.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.05f, 0), new Vector2(1f, 1f), 0, StopsMovement))
                {
                    vertical = true;
                    //move verical animation
                }
                else
                {
                    vertical = false;
                    //bump into collider animation vertical
                }
            }
            else
            {
                vertical = false;
            }
            //this all just makes it prioritize whichever direction was pressed last but also revert to the previous direction if that newly pressed direction is released
            if (vertical || horizontal)
            {
                if (!FindObjectOfType<AudioManager>().PlayingSFX("dog_footsteps") || WalkSoundPlaying == false)
                {
                    FindObjectOfType<AudioManager>().PlaySFX("dog_footsteps");
                    WalkSoundPlaying = true;
                }
                if (vertical && horizontal)
                {
                    if (wasHorizontal)
                    {
                        this.transform.position = this.transform.position + new Vector3(0, MoveSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime, 0);
                    }
                    else
                    {
                        this.transform.position = this.transform.position + new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0);
                    }
                }
                else if (vertical)
                {
                    wasHorizontal = false;
                    this.transform.position = this.transform.position + new Vector3(0, MoveSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime, 0);
                }
                else if (horizontal)
                {
                    wasHorizontal = true;
                    this.transform.position = this.transform.position + new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                if (WalkSoundPlaying == true)
                {
                    FindObjectOfType<AudioManager>().PausePlayingSFX("dog_footsteps");
                    WalkSoundPlaying = false;
                }
            }
        }
    }
}
