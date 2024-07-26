using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSlashScript : MonoBehaviour
{
    [SerializeField] private Collider2D[] colliders = new Collider2D[7];
    [SerializeField] private Animator anim;
    [SerializeField] private bool R;
    [SerializeField] private bool L;
    [SerializeField] private bool M;
    [SerializeField] private bool P;
    private string ChosenFadeInAnim = "";
    private string ChosenSlashAnim = "";
    private const string FadeInAnimR = "MSFadeR";
    private const string SlashAnimR = "MSSlashR";
    private const string FadeInAnimL = "MSFadeL";
    private const string SlashAnimL = "MSSlashL";
    private const string FadeInAnimM = "MSFadeM";
    private const string SlashAnimM = "MSSlashM";
    private const string FadeInAnimP = "MSFadeP";
    private const string SlashAnimP = "MSSlashP";


    private bool DoneBool;
    void Start()
    {
        if (R)
        {
            ChosenFadeInAnim =FadeInAnimR;
            ChosenSlashAnim =SlashAnimR;
        }
        else if (L)
        {
            ChosenFadeInAnim =FadeInAnimL;
            ChosenSlashAnim =SlashAnimL;
        }
        else if (M)
        {
            ChosenFadeInAnim =FadeInAnimM;
            ChosenSlashAnim =SlashAnimM;
        }
        else
        {
            ChosenFadeInAnim =FadeInAnimP;
            ChosenSlashAnim =SlashAnimP;
        }

        DoneBool = false;
        foreach (Collider2D c in colliders)
        {
            c.enabled = false;
        }
    }

    public void Play()
    {
        StartCoroutine("StartSequence");
    }
    IEnumerator StartSequence()
    {
        DoneBool = false;

        anim.Play(ChosenFadeInAnim);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => AnimDone(1));
        yield return new WaitForSeconds(0.5f);

        anim.Play(ChosenSlashAnim);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => AnimDone(0.10f));
        foreach (Collider2D c in colliders)
        {
            c.enabled = true;
        }
        yield return new WaitUntil(() => AnimDone(0.85f));
        foreach (Collider2D c in colliders)
        {
            c.enabled = false;
        }
        yield return new WaitUntil(() => AnimDone(1));

        anim.Play("Blank");
        DoneBool = true;
    }

    private bool AnimDone(float i)
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= i;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Collider2D c in colliders)
            {
                c.enabled = false;
            }
        }
    }
}
