using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNukeScript : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite[] SpriteList;
    private float Delay;
    private Transform Player;
    private float MoveSpeed;
    private void OnEnable()
    {
        Anim.enabled = false;
        MoveSpeed = 2;
        Delay = 1;
        Player = FindObjectOfType<BossPlayerMovement>().gameObject.transform;
        StartCoroutine("TickingTimeBomb");
    }
    private void Update()
    {
        Vector3 Direction = (Player.position - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg) + 90f);
        transform.position += Direction * MoveSpeed * Time.deltaTime;
    }
    private IEnumerator TickingTimeBomb()
    {
        sprite.sprite = SpriteList[0];
        yield return new WaitForSeconds(Delay);
        sprite.sprite = SpriteList[1];
        yield return new WaitForSeconds(Delay);
        sprite.sprite = SpriteList[2];
        yield return new WaitForSeconds(Delay);
        sprite.sprite = SpriteList[3];
        yield return new WaitForSeconds(Delay);
        sprite.sprite = SpriteList[4];
        yield return new WaitForSeconds(Delay);
        sprite.sprite = SpriteList[5];
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("Explode");
    }
    private IEnumerator Explode()
    {
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
            StopAllCoroutines();
            StartCoroutine("Explode");
        }
    }
}
