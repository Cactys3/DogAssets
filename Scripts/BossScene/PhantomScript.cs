using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhantomScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform Phantom;
    [SerializeField] private Collider2D Collider;
    [SerializeField] private Transform StartObj;
    [SerializeField] private Transform EndObj;
    [SerializeField] private int SpriteNum0To9;
    [SerializeField] private Sprite[] sprites = new Sprite[10];

    [SerializeField] private bool Flip;

    private BossFightManager manager;

    public float Delay;

    private Vector3 StartPos;
    private Vector3 EndPos;
    private float MoveSpeed;
    private Vector3 Direction;
    private bool MoveBool;
    private bool DoneBool;
    private bool FadeInBool;
    private bool FadeOutBool;
    private float Seconds;
    private float CurrentSeconds;
    private void Start()
    {
        manager = FindObjectOfType<BossFightManager>();
        if (Flip)
        {
            StartPos = EndObj.position;
            EndPos = StartObj.position;
        }
        else
        {
            EndPos = EndObj.position;
            StartPos = StartObj.position;
        }

        Phantom.gameObject.transform.position = StartPos;
        Direction = (EndPos - StartPos).normalized;
        //Phantom.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg));
        Phantom.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg) + 90f);
        Debug.Log(Phantom.position + "" + StartPos);
        MoveSpeed = 20;
        Phantom.gameObject.SetActive(false);
        MoveBool = false;
        DoneBool = false;
        FadeInBool = false;
        FadeOutBool = false;

        Delay = 1;

        Color tmp = sprite.color;
        tmp.a = 0f;
        sprite.color = tmp;
        int i = 0;
        if (SpriteNum0To9 >= 0 && SpriteNum0To9 < 10f)
        {
            i = SpriteNum0To9;
        }
        sprite.sprite = sprites[i];
    }
    void Update()
    {
        if (FadeInBool)
        {
            CurrentSeconds += Time.deltaTime;
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
                tmp.a = Mathf.Pow(2f, CurrentSeconds / Seconds) - 1f;
                sprite.color = tmp;
            }
        }

        if (FadeOutBool)
        {
            CurrentSeconds += Time.deltaTime;
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
                tmp.a = 2f - Mathf.Pow(2f, CurrentSeconds / Seconds);
                sprite.color = tmp;
            }
        }

        if (MoveBool && !Direction.IsUnityNull())
        {
            //Debug.Log(Phantom.transform.position + " " + (Direction * (MoveSpeed / 100f)) +" "+ Direction + " " + MoveSpeed/100f);
            Phantom.transform.position += Direction * (MoveSpeed) * Time.deltaTime;
        }
    }
    public bool DoneFading()
    {
        return (!FadeInBool && !FadeOutBool);
    }
    private void FadeToBlack(float sec)
    {
        FadeOutBool = true;
        Seconds = sec;
        CurrentSeconds = 0;
    }
    private void FadeFromBlack(float sec)
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
        MoveBool = false;

        yield return new WaitForSeconds(((float)SpriteNum0To9) * Delay);

        Phantom.position = StartPos;
        Direction = (EndPos - StartPos).normalized;
        //Phantom.rotation = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
        //Phantom.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg));
        Phantom.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg) + 90f);
        Collider.enabled = true;
        Phantom.gameObject.SetActive(true);

        Debug.Log("Started Fading In");

        FadeFromBlack(1);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => DoneFading());

        Debug.Log("Done Fading In, Sending Phantom");

        //Phantom.velocity = Direction * velocity;

        manager.PlayMultiSound(BossFightManager.PhantomSound);

        MoveBool = true;

        Debug.Log("rot: " + Phantom.rotation + " pos: " + Phantom.position + " vel: " + Direction*(MoveSpeed/100f));

        yield return new WaitUntil(() => (Phantom.position - EndPos).magnitude < 0.5f);

        Debug.Log("Phantom done, Fading to black");
        Phantom.gameObject.SetActive(false);
        FadeToBlack(0.5f);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => DoneFading());

        Debug.Log("Done Fading to black");
        MoveBool = false;
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
