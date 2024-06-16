using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class JuicerMinigameManager : MonoBehaviour
{
    [Header("Gameplay Values")]
    [SerializeField] private long ClickWeight;
    [SerializeField] private long WinCon;
    [SerializeField] private long CurrentClicks;
    [SerializeField] private long DelayBeforeNotClicking;
    [SerializeField] private long UpgradeOneCost;
    [SerializeField] private long UpgradeTwoCost;
    [SerializeField] private long UpgradeThreeCost;
    [SerializeField] private long TwoXBought;
    [SerializeField] private long EightXBought;
    [SerializeField] private long FiveTwelveXBought;
    [SerializeField] private long TwoXCost;
    [SerializeField] private long EightXCost;
    [SerializeField] private long FiveTwelveXCost;
    [SerializeField] private long MaxEightX;
    [SerializeField] private long MaxFiveTwelveX;
    [SerializeField] private long MaxTwoX;
    [Header("Blender Sprite Stuff")]
    [SerializeField] private SpriteRenderer Blender; 
    [SerializeField] private Animator Blender_ClickingTrue;
    [Header("Exhaust Sprite Stuff")]
    [SerializeField] private SpriteRenderer Exhaust;
    [SerializeField] private Sprite Exhaust_ClickingFalse;
    [SerializeField] private Animator Exhaust_ClickingTrue;
    [Header("Number Sprite Stuff")]
    [SerializeField] private SpriteRenderer Slot1;
    [SerializeField] private SpriteRenderer Slot2;
    [SerializeField] private SpriteRenderer Slot3;
    [SerializeField] private SpriteRenderer Slot4;
    [SerializeField] private SpriteRenderer Slot5;
    [SerializeField] private SpriteRenderer Slot6;
    [SerializeField] private SpriteRenderer Slot7;
    [SerializeField] private SpriteRenderer Slot8;
    [SerializeField] private SpriteRenderer Slot9;
    [SerializeField] private SpriteRenderer Slot10;
    [SerializeField] private SpriteRenderer Slot11;
    [SerializeField] private SpriteRenderer Slot12;
    [SerializeField] private Sprite Zero;
    [SerializeField] private Sprite One;
    [SerializeField] private Sprite Two;
    [SerializeField] private Sprite Three;
    [SerializeField] private Sprite Four;
    [SerializeField] private Sprite Five;
    [SerializeField] private Sprite Six;
    [SerializeField] private Sprite Seven;
    [SerializeField] private Sprite Eight;
    [SerializeField] private Sprite Nine;
    [SerializeField] private Sprite Blank;
    [SerializeField] private Sprite Max;
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
    [SerializeField] private SpriteRenderer TwoXBoughtSprite;
    [SerializeField] private SpriteRenderer EightXBoughtSprite;
    [SerializeField] private SpriteRenderer FiveTwelveXBoughtSprite;
    [SerializeField] private GameObject FiveTwelveXMax;
    [SerializeField] private GameObject TwoXMax;
    [SerializeField] private GameObject EightXMax;
    private bool IsClicking;
    private float ClickTimer;
    // Start is called before the first frame update
    void Start()
    {
        DelayBeforeNotClicking = 100;
        WinCon = 100000000;
        ClickWeight = 1;
        UpgradeOneCost = 100;
        UpgradeTwoCost = 1000;
        UpgradeThreeCost = 100000;
        MaxEightX = 1;
        MaxFiveTwelveX = 1;
        MaxTwoX = 9;
        TwoXCost = 100;
        EightXCost = 1000;
        FiveTwelveXCost = 5000;
        FiveTwelveXMax.SetActive(false);
        TwoXMax.SetActive(false);
        EightXMax.SetActive(false);
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
    public void ButtonClicked(float button)
    {
        switch (button) // "0: Juice!, 1: 2x, 2: 8x, 3: 512x"
        {
            case 0: //this is the click to progress button 
                JuiceClicked();
                break;
            case 1:
                if (CurrentClicks >= TwoXCost)
                {
                    ClickWeight = (ClickWeight * 2);
                    TwoXBought += 1;
                    if (TwoXBought >= MaxTwoX)
                    {
                        TwoXMax.SetActive(true);
                        TwoXBoughtSprite.sprite = GetNumberSprite(MaxTwoX);
                    }
                    else
                    {
                        TwoXBoughtSprite.sprite = GetNumberSprite(TwoXBought % 10);
                        CurrentClicks -= TwoXCost;
                        TwoXCost = TwoXCost * 2;
                        SetCounter();
                    }
                }
                break;
            case 2:
                if (CurrentClicks >= EightXCost)
                {
                    ClickWeight = (ClickWeight * 8);
                    EightXBought += 1;
                    if (EightXBought >= MaxEightX)
                    {
                        EightXMax.SetActive(true);
                        EightXBoughtSprite.sprite = GetNumberSprite(MaxEightX);
                    }
                    else
                    {
                        EightXBoughtSprite.sprite = GetNumberSprite(EightXBought % 10);
                        CurrentClicks -= EightXCost;
                        EightXCost = EightXCost * 2;
                        SetCounter();
                    }
                }
                break;
            case 3:
                if (CurrentClicks >= FiveTwelveXCost)
                {
                    ClickWeight = (ClickWeight * 512);
                    FiveTwelveXBought += 1;
                    if (FiveTwelveXBought >= MaxFiveTwelveX)
                    {
                        FiveTwelveXMax.SetActive(true);
                        FiveTwelveXBoughtSprite.sprite = GetNumberSprite(MaxFiveTwelveX);
                    }
                    else
                    {
                        FiveTwelveXBoughtSprite.sprite = GetNumberSprite(FiveTwelveXBought % 10);
                        CurrentClicks -= FiveTwelveXCost;
                        FiveTwelveXCost = FiveTwelveXCost * 2;
                        SetCounter();
                    }
                }
                break;
        }
    }

    private void Won()
    {

    }

    private void JuiceClicked()
    {
        ClickTimer = 0;
        CurrentClicks += ClickWeight;
        if (CurrentClicks >= WinCon)
        {
            Won();
        }
        else
        {
            ProgressionBar.position = new Vector2(ProgressionBar.position.x, -3f + (6 * ((float)CurrentClicks / (float)WinCon)));
            Debug.Log( ((float)CurrentClicks / (float)WinCon));
            Debug.Log((CurrentClicks / 100000000000) % 10 + " " + (CurrentClicks / 10000000000) % 10 + " " + (CurrentClicks / 1000000000) % 10 + " " + (CurrentClicks / 100000000) % 10 + " " + (CurrentClicks / 10000000) % 10 + " " + (CurrentClicks / 1000000) % 10 + " " + (CurrentClicks / 100000) % 10 + " " + (CurrentClicks / 10000) % 10 + " " + (CurrentClicks / 1000) % 10 + " " + (CurrentClicks / 100) % 10 + " " + (CurrentClicks / 10) % 10 + " " + CurrentClicks % 10);
            SetCounter();
        }
    }

    private void SetCounter()
    {
        Slot1.sprite = GetNumberSprite(CurrentClicks % 10);
        Slot2.sprite = GetNumberSprite((CurrentClicks / 10) % 10);
        Slot3.sprite = GetNumberSprite((CurrentClicks / 100) % 10);
        Slot4.sprite = GetNumberSprite((CurrentClicks / 1000) % 10);
        Slot5.sprite = GetNumberSprite((CurrentClicks / 10000) % 10);
        Slot6.sprite = GetNumberSprite((CurrentClicks / 100000) % 10);
        Slot7.sprite = GetNumberSprite((CurrentClicks / 1000000) % 10);
        Slot8.sprite = GetNumberSprite((CurrentClicks / 10000000) % 10);
        Slot9.sprite = GetNumberSprite((CurrentClicks / 100000000) % 10);
        Slot10.sprite = GetNumberSprite((CurrentClicks / 1000000000) % 10);
        Slot11.sprite = GetNumberSprite((CurrentClicks / 10000000000) % 10);
        Slot12.sprite = GetNumberSprite((CurrentClicks / 100000000000) % 10);
    }

    private Sprite GetNumberSprite(long num)
    {
        switch (num)
        {
            case 0:
                return Zero;
            case 1:
                return One;
            case 2:

                return Two;
            case 3:

                return Three;
            case 4:

                return Four;
            case 5:

                return Five;
            case 6:

                return Six;
            case 7:

                return Seven;
            case 8:

                return Eight;
            case 9:

                return Nine;
            default:
                return Blank;
        }
    }
}
