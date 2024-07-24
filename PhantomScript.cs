using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhantomScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D Phantom;
    [SerializeField] private Collider2D Collider;
    [SerializeField] private Vector2 StartPos;
    [SerializeField] private Vector2 EndPos;
    [SerializeField] private float velocity;
    [SerializeField] private int SpriteNum0To9;
    [SerializeField] private Sprite[] sprites = new Sprite[10];

    private bool DoneBool;
    private bool FadeInBool;
    private bool FadeOutBool;
    private float Seconds;
    private float CurrentSeconds;
    private void Start()
    {
        DoneBool = false;
        FadeInBool = false;
        FadeOutBool = false;
        Phantom.gameObject.SetActive(false);
        Color tmp = sprite.color;
        tmp.a = 0f;
        sprite.color = tmp;
        int i = 0;
        if (SpriteNum0To9 >= 0 && SpriteNum0To9 < 10)
        {
            i = SpriteNum0To9;
        }
        sprite.sprite = sprites[i];


        StartCoroutine("test");
    }
    private IEnumerator test()
    {
        yield return new WaitForSeconds(3);
        Play();
    }
    private void FixedUpdate()
    {
        if (FadeInBool)
        {
            CurrentSeconds += 1;
            if (CurrentSeconds >= Seconds)
            {
                FadeInBool = false;
                Color tmp = sprite.color;
                tmp.a = 1f;
                sprite.color = tmp;
            }
            else
            {
                Color tmp = sprite.color;
                tmp.a = Mathf.Pow(2, CurrentSeconds / Seconds) - 1;
                sprite.color = tmp;
            }
        }

        if (FadeOutBool)
        {
            CurrentSeconds += (1 * Time.deltaTime);
            if (CurrentSeconds >= Seconds)
            {
                FadeOutBool = false;
                Color tmp = sprite.color;
                tmp.a = 0f;
                sprite.color = tmp;
            }
            else
            {
                Color tmp = sprite.color;
                tmp.a = 2 - Mathf.Pow(2, CurrentSeconds / Seconds);
                sprite.color = tmp;
            }
        }
    }
    public bool DoneFading()
    {
        return (!FadeInBool && !FadeOutBool);
    }
    private void FadeToBlack(int sec)
    {
        FadeOutBool = true;
        Seconds = sec;
        CurrentSeconds = 0;
    }
    private void FadeFromBlack(int sec)
    {
        FadeInBool = true;
        Seconds = sec;
        CurrentSeconds = 0;
    }
    public void Play()
    {
        StartCoroutine("PlayEnumerator");
    }
    private IEnumerator PlayEnumerator()
    {
        DoneBool = false;
        Phantom.gameObject.SetActive(true);
        Phantom.position = StartPos;
        Phantom.velocity = Vector2.zero;
        Phantom.angularVelocity = 0;
        Collider.enabled = true;
        Debug.Log("Started Fading In");
        FadeFromBlack(5);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => DoneFading());
        Debug.Log("Done Fading In, Sending Phantom");
        Vector2 Direction = (EndPos - StartPos).normalized;
        Phantom.position = StartPos;
        Phantom.rotation = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Phantom.velocity = Direction * velocity;
        Debug.Log("rot: " + Phantom.rotation + " pos: " + Phantom.position + " vel: " + Phantom.velocity);
        yield return new WaitUntil(() => (Phantom.position - EndPos).magnitude < 0.1f);
        Debug.Log("Phantom done, Fading to black");
        FadeToBlack(5);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => DoneFading());
        Debug.Log("Done Fading to black");
        DoneBool = true;
    }
    public bool Done()
    {
        return DoneBool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collider.enabled = false;
            //TODO: you know...
        }
    }
}
