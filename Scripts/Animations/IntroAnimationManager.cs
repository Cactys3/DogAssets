using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimationManager : MonoBehaviour
{
    [SerializeField] private AnimationClip PreBompClip;
    [SerializeField] private AnimationClip PostBombClip;
    [SerializeField] private AnimationClip Sleep1xClip;
    [SerializeField] private AnimationClip Sleep2xClip;
    [SerializeField] private SpriteRenderer BlackSprite;
    [SerializeField] private Sprite StartNotHovered;
    [SerializeField] private Sprite StartHovered;
    [SerializeField] private Sprite SleepNotHovered;
    [SerializeField] private Sprite SleepHovered;
    [SerializeField] private SpriteRenderer StartSprite;
    [SerializeField] private SpriteRenderer SleepSprite;
    private SpriteRenderer Sprite;
    private Animator Animator;
    private Animator FadeAnimator;
    private bool FadeOut;
    private bool FadeIn;
    private float Seconds;
    private float CurrentSeconds;
    private int state;
    private bool SleptAlready;
    private bool ShowQuestions;

    private AudioManager audioMan;

    public const string PlaneSound = "intro_plane";
    public const string NatureSound = "intro_nature";
    public const string CutToBlackSound = "intro_black";
    public const string DoorSound = "intro_door";

    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
        ShowQuestions = false;
        SleptAlready = false;
        state = 0;
        CurrentSeconds = 0;
        FadeOut = false;
        FadeIn = false;
        Animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        FadeAnimator = BlackSprite.gameObject.GetComponent<Animator>();
        StartCoroutine("PreBomb");
        SleepSprite.gameObject.SetActive(false);
        StartSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (ShowQuestions)
        {
            SleepSprite.gameObject.SetActive(true);
            StartSprite.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (StartSprite.sprite == StartHovered)
                {
                    StartSprite.sprite = StartNotHovered;
                    SleepSprite.sprite = SleepHovered;
                }
                else
                {
                    StartSprite.sprite = StartHovered;
                    SleepSprite.sprite = SleepNotHovered;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                SleepSprite.gameObject.SetActive(false);
                StartSprite.gameObject.SetActive(false);
                ShowQuestions = false;
                if (StartSprite.sprite == StartHovered)
                {
                    this.GetComponent<ChangeToScene>().ChangeScene();
                }
                else
                {
                    if (SleptAlready)
                    {
                        StartCoroutine("Sleep2x");
                    }
                    else
                    {
                        StartCoroutine("Sleep1x");
                        SleptAlready = true;
                    }
                }
            }
        }
    }

    private IEnumerator PreBomb() //Starts out with people talking in the background and the dog awake in his igloo and a peaceful animation of the window outside.
    {
        state = 1;
        Debug.Log("nature sound");
        Play(NatureSound);
        yield return new WaitForSeconds(2); //wait at black for 2 seconds
        Animator.Play("PreBomb");
        FadeAnimator.Play("Fade In");

        yield return new WaitUntil(() => FinishedFade()); //wait until fade is finished

        yield return new WaitForSeconds(2); //wait playing the animation for 2 seconds

        Debug.Log("plane sound");
        Play(PlaneSound);
        yield return new WaitForSeconds(4); //wait playing the plane sound for seconds

        StartCoroutine("Bomb");
    }
    private IEnumerator Bomb()
    {
        state = 2;

        FadeAnimator.Play("Plane");
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => FadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.977);//wait until black is shown
        Stop(NatureSound);
        Stop(PlaneSound);
        Play(CutToBlackSound);
        Debug.Log("black sound and stop other sounds");
        yield return new WaitUntil(() => FinishedFade()); //wait until bomb is finished
        yield return new WaitForSeconds(3); //wait 3 seconds on black screen
        Play(DoorSound);
        Debug.Log("door sound");
        yield return new WaitForSeconds(6); //wait 6 more seconds on black screen
        StartCoroutine("PostBomb");
    }

    private IEnumerator PostBomb()
    {
        state = 3;
        Animator.Play("PostBomb");
        FadeAnimator.Play("Fade In");
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => FinishedFade()); //wait until fade is finished

        //now we ask the questions
        ShowQuestions = true;
        StartSprite.sprite = StartNotHovered;
        SleepSprite.sprite = SleepHovered;
    }

    private IEnumerator Sleep1x()
    {
        state = 4;
        Debug.Log("Sleep1x Start");
        FadeAnimator.Play("Fade Out");
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => FinishedFade()); //wait until fade is finished
        yield return new WaitForSeconds(3f);
        Animator.Play("Sleep x1");
        FadeAnimator.Play("Fade In");
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => FinishedFade()); //wait until fade is finished
        yield return new WaitForSeconds(1f);
        ShowQuestions = true;
    }

    private IEnumerator Sleep2x()
    {
        state = 5;
        Debug.Log("Sleep1x Start");
        FadeAnimator.Play("Fade Out");
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => FinishedFade()); //wait until fade is finished
        Animator.Play("Sleep x2");
        FadeAnimator.Play("Fade In");
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => FinishedFade()); //wait until fade is finished
        yield return new WaitForSeconds(8f);
        Animator.Play("Death");
        yield return new WaitForSeconds(5f);
        Play("monster");
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
    private void Play(string s)
    {
        try
        {
            audioMan.PlaySFX(s);
        }
        catch
        {
            Debug.LogWarning("couldn't play sound because no audioma");
        }
    }
    private void Stop(string s)
    {
        try
        {
            audioMan.StopPlayingSFX(s);
        }
        catch
        {
            Debug.LogWarning("couldn't stop sound because no audioma");
        }
    }
    private bool FinishedFade() //returns true if the animation the fade is playing has played once
    {
        return FadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
    private bool FinishedThis() //returns true if the animation this is playing has played once
    {
        return Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
    /** OLD STUFF 
        private void FadeToBlack(int sec)
        {
            FadeOut = true;
            Seconds = sec;
            CurrentSeconds = 0;
        }
        private void FadeFromBlack(int sec)
        {
            FadeIn = true;
            Seconds = sec;
            CurrentSeconds = 0;
        }

            // Update is called once per frame
        void Update()
        {
            //Debug.Log(BlackSprite.color.a);
            if (FadeOut)
            {
                CurrentSeconds += (1 * Time.deltaTime);
                if (CurrentSeconds >= Seconds)
                {
                    FadeOut = false;
                    Color tmp = BlackSprite.color;
                    tmp.a = 1f;
                    BlackSprite.color = tmp;
                }
                else
                {
                    Color tmp = BlackSprite.color;
                    tmp.a = Mathf.Pow(2, CurrentSeconds / Seconds) - 1;
                    //tmp.a = CurrentSeconds / Seconds;
                    BlackSprite.color = tmp;
                }
            }

            if (FadeIn)
            {
                CurrentSeconds += (1 * Time.deltaTime);
                if (CurrentSeconds >= Seconds)
                {
                    FadeIn = false;
                    Color tmp = BlackSprite.color;
                    tmp.a = 0f;
                    BlackSprite.color = tmp;
                }
                else
                {
                    Color tmp = BlackSprite.color;
                    tmp.a = 2 - Mathf.Pow(2, CurrentSeconds / Seconds); //exponential because the early jumps from 1 to 0.9 result in a huge amount of change compared to later jumps like from 0.1 to 0
                    //tmp.a = 1 - (CurrentSeconds / Seconds);
                    BlackSprite.color = tmp;
                }
            }
        }
*/
}
