using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigNukeScript : MonoBehaviour
{
    private bool TowardsPlayer;
    private bool TowardsBoss;
    private float TowardsBossSpeed;
    private float TowardsPlayerSpeed;
    [SerializeField] Transform Boss;
    [SerializeField] Transform Player;
    [SerializeField] Animator Anim;
    [SerializeField] Sprite[] Sprites;
    [SerializeField] SpriteRenderer Sprite;
    [SerializeField] GameObject Text;

    private BossFightManager manager;

    private float InitialDistance;
    private void Start()
    {
        manager = FindObjectOfType<BossFightManager>();
        manager.PlaySound(BossFightManager.BignukeFlySound);
    }
    private void OnEnable()
    {
        Anim.enabled = false;
        Text.SetActive(false);
        TowardsBossSpeed = 20f;
        TowardsPlayerSpeed = 8f;
        TowardsPlayer = true;
        TowardsBoss = true;
        Anim.StopPlayback();
        InitialDistance = Vector3.Distance(Player.position, transform.position);
    }
    private void Update()
    {

        float CurrentDistance = 0;
        if (TowardsPlayer)
        {
            CurrentDistance = Vector3.Distance(Player.position, transform.position);
            PointTowards(Player.position);
            if (CurrentDistance < 1)
            {
                Text.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TowardsPlayer = false;
                }
            }
            else
            {
                transform.position += (Player.position - transform.position).normalized * TowardsPlayerSpeed * Time.deltaTime;
                if (CurrentDistance > (InitialDistance * (6f/7f)))
                {
                    Sprite.sprite = Sprites[0];
                }
                else if (CurrentDistance > (InitialDistance * (5f / 7f)))
                {
                    Sprite.sprite = Sprites[1];
                }
                else if (CurrentDistance > (InitialDistance * (4f / 7f)))
                {
                    Sprite.sprite = Sprites[2];
                }
                else if (CurrentDistance > (InitialDistance * (3f / 7f)))
                {
                    Sprite.sprite = Sprites[3];
                }
                else if (CurrentDistance > (InitialDistance * (2f / 7f)))
                {
                    Sprite.sprite = Sprites[4];
                }
                else if (CurrentDistance > (InitialDistance * (1f / 7f)))
                {
                    Sprite.sprite = Sprites[5];
                }
            }
        }
        else if (TowardsBoss)
        {
            CurrentDistance = Vector3.Distance(Boss.position, transform.position);
            PointTowards(Boss.position);
            if (CurrentDistance < 0.2f)
            {
                TowardsBoss = false;
                Anim.enabled = true;
                manager.StopSound(BossFightManager.BignukeFlySound);
                manager.PlaySound(BossFightManager.BignukeExplodeSound);
                Anim.Play("Explosion");
            }
            else
            {
                transform.position += (Boss.position - transform.position).normalized * TowardsBossSpeed * Time.deltaTime;
            }
        }

        Debug.Log(TowardsPlayer + " " + TowardsBoss + " " + CurrentDistance);
    }
    public bool Done()
    {
        return (!TowardsBoss && !TowardsPlayer);
    }
    public bool AtPlayer()
    {
        return Vector3.Distance(Player.position, transform.position) < 1;
    }
    private void PointTowards(Vector3 Object)
    {
        float angle;
        Vector3 mousePos = Object;
        mousePos.z = 0;
        Vector3 direction = mousePos - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentRotation = transform.rotation.eulerAngles.z;
        float rotationStep = 50 * Time.deltaTime;
        float newRotation = Mathf.LerpAngle(currentRotation, angle + 90, rotationStep);
        transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }
}
