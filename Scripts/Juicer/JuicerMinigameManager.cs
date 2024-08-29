using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JuicerMinigameManager : MonoBehaviour
{
    [Header("Gameplay Values")]
    [SerializeField] private long ClickWeight;
    [SerializeField] private long MaxClickWeight;
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
    [SerializeField] private AnimationClip BlenderClipLoop;
    [SerializeField] private AnimationClip BlenderClipStill;
    [SerializeField] private Animator BlenderAnimator;
    [Header("Exhaust Sprite Stuff")]
    [SerializeField] private AnimationClip ExhaustClipLoop;
    [SerializeField] private AnimationClip ExhaustClipStill;
    [SerializeField] private Animator ExhaustAnimator;
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


    public const string JuicerUpgradeSound = "juicer_upgrade";
    public const string JuicerFailUpgradeSound = "juicer_upgrade_fail";
    public const string JuicerClickSound = "juicer_click";
    public const string JuicerWinSound = "juicer_win";
    public const string JuicerBlenderSound = "juicer_blender";

    private bool IsClicking;
    private float ClickTimer;
    private AudioManager audioMan;
    private Image Cursor;

    private bool WonBool;

    // Start is called before the first frame update
    void Start()
    {
        MaxClickWeight = 1 * ((MaxEightX * 8) + (MaxTwoX * 2) + (MaxFiveTwelveX * 512));

        MaxClickWeight = 2097152;

        WonBool = false;
        Cursor = FindObjectOfType<MyCursor>().GetComponentInChildren<Image>();
        audioMan = FindObjectOfType<AudioManager>();
        DelayBeforeNotClicking = 3;
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

        // Check if enough time has passed since the last click
        if (ClickTimer >= DelayBeforeNotClicking)
        {
            IsClicking = false;
        }
        else
        {
            IsClicking = true;
        }

        // Logic for when clicking
        if (IsClicking)
        {
            if (!BlenderAnimator.GetCurrentAnimatorStateInfo(0).IsName(BlenderClipLoop.name))
            {
                // Make the Blender Animator play BlenderClipLoop
                BlenderAnimator.Play(BlenderClipLoop.name);
            }
            if (!ExhaustAnimator.GetCurrentAnimatorStateInfo(0).IsName(ExhaustClipLoop.name))
            {
                // Make the Exhaust Animator play ExhaustClipLoop
                ExhaustAnimator.Play(ExhaustClipLoop.name);
            }
        }
        else
        {
            if ((BlenderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) > 0.95)
            {
                // Make the Blender Animator play BlenderClipStill
                BlenderAnimator.Play(BlenderClipStill.name);
            }
            if ((ExhaustAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) > 0.95)
            {
                // Make the Exhaust Animator play ExhaustClipStill
                ExhaustAnimator.Play(ExhaustClipStill.name);
            }
        }

        // Update the ClickTimer
        ClickTimer += (1 * Time.deltaTime);
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
                if (TwoXBought < MaxTwoX)
                {
                    if (CurrentClicks >= TwoXCost) 
                    {
                        audioMan.PlayOverrideSFX(JuicerUpgradeSound);

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
                        if (!audioMan.PlayingSFX(JuicerUpgradeSound))
                        {
                            audioMan.PlayOverrideSFX(JuicerFailUpgradeSound);
                        }
                    }
                }
                break;
            case 2:
                if (EightXBought < MaxEightX) 
                {
                    if (CurrentClicks >= EightXCost)
                    {
                        audioMan.PlayOverrideSFX(JuicerUpgradeSound);

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
                        if (!audioMan.PlayingSFX(JuicerUpgradeSound))
                        {
                            audioMan.PlayOverrideSFX(JuicerFailUpgradeSound);
                        }
                    }
                }
                break;
            case 3:
                if (FiveTwelveXBought < MaxFiveTwelveX)
                {
                    if (CurrentClicks >= FiveTwelveXCost)
                    {
                        audioMan.PlayOverrideSFX(JuicerUpgradeSound);

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
                        if (!audioMan.PlayingSFX(JuicerUpgradeSound))
                        {
                            audioMan.PlayOverrideSFX(JuicerFailUpgradeSound);
                        }
                    }
                }
                break;
        }
        Number.SetNumber((int)CurrentClicks);
    }

    private IEnumerator Won()
    {

        audioMan.PlayMultipleSFX(JuicerWinSound);
        yield return new WaitForSeconds(4f);
        GetComponent<ChangeToScene>().ChangeScene();
    }

    private void JuiceClicked()
    {
        ClickTimer = 0;
        CurrentClicks += ClickWeight;
        Debug.Log((1 - ((ClickWeight / 2) / MaxClickWeight)) + " equals: 1 - " + ClickWeight + " / " + "2 / " + MaxClickWeight );
        audioMan.SetPitchSFX(JuicerClickSound, 1 - ((ClickWeight / 2) / MaxClickWeight));
        audioMan.PlayMultipleSFX(JuicerClickSound);
        if (CurrentClicks >= WinCon)
        {
            Cursor.sprite = Cursor1;
            ProgressionBar.position = new Vector2(ProgressionBar.position.x, -3f + (6.12f));
            if (!WonBool)
            {
                WonBool = true;
                StartCoroutine("Won");
            }
        }
        else
        {
            ProgressionBar.position = new Vector2(ProgressionBar.position.x, -3f + (6.12f * ((float)CurrentClicks / (float)WinCon)));
        }
    }
    private bool AnimationFinished(Animator a)
    {
        return a.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
