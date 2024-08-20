using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNukeScript : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite[] SpriteList;
    [SerializeField] private Collider2D hitbox;

    private BossFightManager manager;

    private int CurrentSprite;
    private float Delay;
    private Transform Player;
    private float MoveSpeed;
    private void Start()
    {
        manager = FindObjectOfType<BossFightManager>();
        manager.PlaySound(BossFightManager.MininukeFlySound);
    }
    private void OnEnable()
    {
        Anim.enabled = false;
        MoveSpeed = 2;
        Delay = 1;
        Player = FindObjectOfType<BossPlayerMovement>().gameObject.transform;
        StartCoroutine("TickingTimeBomb");
        CurrentSprite = 0;
    }
    private void Update()
    {
        Vector3 Direction = (Player.position - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg) + 90f);
        transform.position += Direction * MoveSpeed * Time.deltaTime;
    }
    private void AddTick() //maybe when the player hits a bomb with a sword?
    {
        StopAllCoroutines();
        CurrentSprite++;
        StartCoroutine("TickingTimeBomb");
    }
    private IEnumerator TickingTimeBomb()
    {
        while(CurrentSprite < 5)
        {
            sprite.sprite = SpriteList[CurrentSprite];
            yield return new WaitForSeconds(Delay);
            CurrentSprite++;
        }
        sprite.sprite = SpriteList[5];
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("Explode");
    }
    private IEnumerator Explode()
    {
        manager.StopSound(BossFightManager.MininukeFlySound);
        manager.PlayMultiSound(BossFightManager.MininukeExplodeSound);
        //maybe activate a bigger trigger collider here for the explosion radius
        Anim.enabled = true;
        Anim.Play("Explode");
        yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hitbox.enabled = false;
            StopAllCoroutines();
            StartCoroutine("Explode");
        }
    }
}
