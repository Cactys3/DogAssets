using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class JuicerMinigameManager : MonoBehaviour
{
    [Header("Gameplay Values")]
    [SerializeField] private int ClickWeight;
    [SerializeField] private int WinCon;
    [SerializeField] private int CurrentClicks;
    [SerializeField] private int DelayBeforeNotClicking;
    [Header("Blender Sprite Stuff")]
    [SerializeField] private SpriteRenderer Blender; 
    [SerializeField] private Animator Blender_ClickingTrue;
    [Header("Exhaust Sprite Stuff")]
    [SerializeField] private SpriteRenderer Exhaust;
    [SerializeField] private Sprite Exhaust_ClickingFalse;
    [SerializeField] private Animator Exhaust_ClickingTrue;
    [Header("Cursor Stuff")]
    [SerializeField] private SpriteRenderer Cursor;
    [SerializeField] private Sprite Cursor1;
    [SerializeField] private Sprite Cursor2;
    [SerializeField] private Sprite Cursor3;
    [SerializeField] private Sprite Cursor4;
    [SerializeField] private Sprite Cursor5;
    [SerializeField] private Sprite Cursor6;
    [Header("Other")]
    [SerializeField] private Transform ProgressionBar;
    private bool IsClicking;
    private float ClickTimer;
    // Start is called before the first frame update
    void Start()
    {
        DelayBeforeNotClicking = 100;
        WinCon = 10000;
        ClickWeight = 10;
    }

    void Update()
    {
        test();


        ClickTimer += (1 * Time.deltaTime);
        if (ClickTimer >= DelayBeforeNotClicking) //if it's been a certian time since the last click
        {
            IsClicking = false;
        }
        else
        {
            IsClicking = true;
        }


    }
    private void test()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Cursor.sprite = Cursor1;
        }
        if (Input.GetKey(KeyCode.O))
        {
            Cursor.sprite = Cursor2;
        }
        if (Input.GetKey(KeyCode.I))
        {
            Cursor.sprite = Cursor3;
        }
        if (Input.GetKey(KeyCode.U))
        {
            Cursor.sprite = Cursor4;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Cursor.sprite = Cursor5;
        }
        if (Input.GetKey(KeyCode.T))
        {
            Cursor.sprite = Cursor6;
        }
    }
    public void ButtonClicked(int button)
    {
        switch (button)
        {
            case 0: //this is the click to progress button
                ClickTimer = 0;

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }
}
