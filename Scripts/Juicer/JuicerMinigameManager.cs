using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuicerMinigameManager : MonoBehaviour
{
    [Header("Gameplay Values")]
    [SerializeField] private long ClickWeight;
    [SerializeField] private long WinCon;
    [SerializeField] private long CurrentClicks;
    [SerializeField] private long DelayBeforeNotClicking;
    [SerializeField] private long TwoXBought;
    [SerializeField] private long TwoXCost;
    [SerializeField] private long TwoXCostMultiplier;
    [SerializeField] private long MaxTwoX;
    [SerializeField] private long EightXBought;
    [SerializeField] private long EightXCost;
    [SerializeField] private long MaxEightX;
    [SerializeField] private long EightXCostMultiplier;
    [SerializeField] private long FiveTwelveXBought;
    [SerializeField] private long FiveTwelveXCost;
    [SerializeField] private long MaxFiveTwelveX;
    [SerializeField] private long FiveTwelveXCostMultiplier;
    [Header("Blender Sprite Stuff")]
    [SerializeField] private SpriteRenderer Blender; 
    [SerializeField] private Animator Blender_ClickingTrue;
    [Header("Exhaust Sprite Stuff")]
    [SerializeField] private SpriteRenderer Exhaust;
    [SerializeField] private Sprite Exhaust_ClickingFalse;
    [SerializeField] private Animator Exhaust_ClickingTrue;
    [Header("Number Sprite Stuff")]
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
    [SerializeField] private Sprite Price;
    [Header("Cursor Stuff")]
    [SerializeField] private SpriteRenderer Cursor;
    [SerializeField] private Sprite Cursor1;
    [SerializeField] private Sprite Cursor2;
    [SerializeField] private Sprite Cursor3;
    [SerializeField] private Sprite Cursor4;
    [SerializeField] private Sprite Cursor5;
    [SerializeField] private Sprite Cursor6;
    [Header("Other")]
    [SerializeField] private GenerativeNumbers Number;
    [SerializeField] private Transform ProgressionBar;
    [SerializeField] private GenerativeNumbers TwoXBoughtNumber;
    [SerializeField] private GenerativeNumbers EightXBoughtNumber;
    [SerializeField] private GenerativeNumbers FiveTwelveXBoughtNumber;
    [SerializeField] private SpriteRenderer FiveTwelveXMax;
    [SerializeField] private SpriteRenderer TwoXMax;
    [SerializeField] private SpriteRenderer EightXMax;
    [SerializeField] private GenerativeNumbers FiveTwelveXPriceNumber;
    [SerializeField] private GenerativeNumbers TwoXPriceNumber;
    [SerializeField] private GenerativeNumbers EightXPriceNumber;
    private bool IsClicking;
    private float ClickTimer;
    // Start is called before the first frame update
    void Start()
    {
        DelayBeforeNotClicking = 100;
        WinCon = 100000000;
        ClickWeight = 1;
        MaxEightX = 1;
        MaxFiveTwelveX = 1;
        MaxTwoX = 9;
        TwoXCost = 100;
        EightXCost = 1000;
        FiveTwelveXCost = 5000;
        FiveTwelveXMax.sprite = Price;
        TwoXMax.sprite = Price;
        EightXMax.sprite = Price;
        FiveTwelveXPriceNumber.SetNumber(FiveTwelveXCost);
        TwoXPriceNumber.SetNumber(TwoXCost);
        EightXPriceNumber.SetNumber(EightXCost);
        Number.SetNumber((int)CurrentClicks);
        TwoXCostMultiplier = 2;
        EightXCostMultiplier = 2;
        FiveTwelveXCostMultiplier = 2;
      //  TwoXPriceNumber.SetScale(0.3f);
     //   EightXPriceNumber.SetScale(0.3f);
      //  FiveTwelveXPriceNumber.SetScale(0.3f);
        TwoXPriceNumber.SetSpacing(0.13f);
        EightXPriceNumber.SetSpacing(0.13f);
        FiveTwelveXPriceNumber.SetSpacing(0.13f);
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
                    if (TwoXBought < MaxTwoX)
                    {
                        ClickWeight = (ClickWeight * 2);
                        TwoXBought += 1;
                        TwoXBoughtNumber.IncrementNumber(1);
                        CurrentClicks -= TwoXCost;
                        TwoXCost = TwoXCost * TwoXCostMultiplier;
                        if (TwoXBought == MaxTwoX)
                        {
                            TwoXMax.sprite = Max;
                        }
                        TwoXPriceNumber.SetNumber(TwoXCost);
                        Cursor.sprite = Cursor2;
                    }
                    else
                    {
                        //TODO: maybe do an animation thing 
                    }
                }
                break;
            case 2:
                if (CurrentClicks >= EightXCost)
                {
                    if (EightXBought < MaxEightX)
                    {
                        ClickWeight = (ClickWeight * 8);
                        EightXBought += 1;
                        EightXBoughtNumber.IncrementNumber(1);
                        CurrentClicks -= EightXCost;
                        EightXCost = EightXCost * EightXCostMultiplier;
                        if (EightXBought == MaxEightX)
                        {
                            EightXMax.sprite = Max;
                        }
                        TwoXPriceNumber.SetNumber(EightXCost);
                        Cursor.sprite = Cursor4;
                    }
                    else
                    {
                        //TODO: maybe do an animation thing 
                    }
                }
                break;
            case 3:
                if (CurrentClicks >= FiveTwelveXCost)
                {
                    if (FiveTwelveXBought < MaxFiveTwelveX)
                    {
                        ClickWeight = (ClickWeight * 512);
                        FiveTwelveXBought += 1;
                        FiveTwelveXBoughtNumber.IncrementNumber(1);
                        CurrentClicks -= FiveTwelveXCost;
                        FiveTwelveXCost = FiveTwelveXCost * FiveTwelveXCostMultiplier;

                        if (FiveTwelveXBought == MaxFiveTwelveX)
                        {
                            FiveTwelveXMax.sprite = Max;
                        }
                        FiveTwelveXPriceNumber.SetNumber(FiveTwelveXCost);
                        Cursor.sprite = Cursor6;
                    }
                    else
                    {
                        //TODO: maybe do an animation thing 
                    }
                }
                break;
        }
        Number.SetNumber((int)CurrentClicks);
    }

    private void Won()
    {
        GetComponent<ChangeToScene>().ChangeScene();
    }

    private void JuiceClicked()
    {
        ClickTimer = 0;
        CurrentClicks += ClickWeight;
        if (CurrentClicks >= WinCon)
        {
            Cursor.sprite = Cursor1;
            ProgressionBar.position = new Vector2(ProgressionBar.position.x, -3f + (6.12f));
            Won();
        }
        else
        {
            ProgressionBar.position = new Vector2(ProgressionBar.position.x, -3f + (6.12f * ((float)CurrentClicks / (float)WinCon)));
        }
    }
}
