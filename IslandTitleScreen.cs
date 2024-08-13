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
    private bool WaterAtMaxHeight;
    private bool PlayedIntro;
    private float MaxHeight;
    private float Velocity;
    private bool G;
    private bool D;
    private bool O;
    void Start()
    {
        G = false;
        D = false;
        O = false;
        PlayedIntro = false;
        WaterAtMaxHeight = false;
        MaxHeight = 4.4f;
        Velocity = 0.05f;
        Text.SetActive(false);
        Anim.Play("Water_Intro");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!O && !G)
            {
                D = true;
                O = false;
                G = false;
                Dui.fontStyle = FontStyles.Bold;
            }
            else
            {
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
                D = true;
                O = true;
                G = false;
                Oui.fontStyle = FontStyles.Bold;
            }
            else
            {
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
                D = true;
                O = true;
                G = true;
                Gui.fontStyle = FontStyles.Bold;
            }
            else
            {
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
            SceneChanger.ChangeScene();
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
}
