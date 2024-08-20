using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IslandTitleScreen : MonoBehaviour
{
    [SerializeField] private Transform Water;
    [SerializeField] Animator Anim;
    [SerializeField] ChangeToScene SceneChanger;
    [SerializeField] GameObject Text;
    [SerializeField] TextMeshProUGUI Dui;
    [SerializeField] TextMeshProUGUI Oui;
    [SerializeField] TextMeshProUGUI Gui;

    private AudioManager audioMan;
    public const string DSound = "title_D";
    public const string OSound = "title_O";
    public const string GSound = "title_G";
    public const string WaterSound = "title_water";
    public const string StartSound = "title_start";
    public const string FailSound = "title_fail";

    private bool WaterAtMaxHeight;
    private bool PlayedIntro;
    private float MaxHeight;
    private float Velocity;
    private bool G;
    private bool D;
    private bool O;
    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
        G = false;
        D = false;
        O = false;
        PlayedIntro = false;
        WaterAtMaxHeight = false;
        MaxHeight = 4.4f;
        Velocity = 0.05f;
        Text.SetActive(false);
        Anim.Play("Water_Intro");

        audioMan.PlaySFX(WaterSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && (!Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.O) && !Input.GetKeyDown(KeyCode.G)))
        {
            audioMan.PlaySFX(FailSound);
            D = false;
            O = false;
            G = false;
            Dui.fontStyle = FontStyles.Normal;
            Oui.fontStyle = FontStyles.Normal;
            Gui.fontStyle = FontStyles.Normal;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!O && !G)
            {
                audioMan.PlaySFX(DSound);
                D = true;
                O = false;
                G = false;
                Dui.fontStyle = FontStyles.Bold;
            }
            else
            {
                audioMan.PlaySFX(FailSound);
                D = false;
                O = false;
                G = false;
                Dui.fontStyle = FontStyles.Normal;
                Oui.fontStyle = FontStyles.Normal;
                Gui.fontStyle = FontStyles.Normal;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (D && !G)
            {
                audioMan.PlaySFX(OSound);
                D = true;
                O = true;
                G = false;
                Oui.fontStyle = FontStyles.Bold;
            }
            else
            {
                audioMan.PlaySFX(FailSound);
                D = false;
                O = false;
                G = false;
                Dui.fontStyle = FontStyles.Normal;
                Oui.fontStyle = FontStyles.Normal;
                Gui.fontStyle = FontStyles.Normal;
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (D && O)
            {
                audioMan.PlaySFX(GSound);
                D = true;
                O = true;
                G = true;
                Gui.fontStyle = FontStyles.Bold;
            }
            else
            {
                audioMan.PlaySFX(FailSound);
                D = false;
                O = false;
                G = false;
                Dui.fontStyle = FontStyles.Normal;
                Oui.fontStyle = FontStyles.Normal;
                Gui.fontStyle = FontStyles.Normal;
            }
        }
        if (PlayedIntro && G && O && D)
        {
            StartCoroutine("ChangeScene");
        }
        if (!PlayedIntro && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && Time.timeSinceLevelLoad > 0.5f)
        {
            PlayedIntro = true;
            Anim.Play("Water_Loop");
            Debug.Log("loop");
            Text.SetActive(true);
        }
        if (PlayedIntro && !WaterAtMaxHeight)
        {
            if (Water.position.y >= MaxHeight)
            {
                WaterAtMaxHeight = true;
                Water.position = new Vector2(0, MaxHeight);
            }
            else
            {
                Water.position += new Vector3(0, Velocity * Time.deltaTime, 0);
            }
        }
    }
    private IEnumerator ChangeScene()
    {
        audioMan.PlaySFX(StartSound);
        yield return new WaitForSeconds(audioMan.GetLengthSFX(StartSound) + 0.2f);
        audioMan.StopPlayingSFX(WaterSound);
        SceneChanger.ChangeScene();
    }
}
