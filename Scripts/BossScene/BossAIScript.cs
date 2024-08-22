using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class BossAIScript : MonoBehaviour
{
    [Header("Just For Testing")]
    [SerializeField] LayerMask StopsMovement; 
    [SerializeField] bool MoveBoss;
    [SerializeField] float RotationSpeed;
    int AnimState = 0;

    [Header("References")]
    [SerializeField] private GameObject BigNukeText;
    [SerializeField] public Rigidbody2D body;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BossPlayerMovement player;
    [SerializeField] private BossFightManager manager;
    public Animator anim;
    [SerializeField] private Collider2D BossHitbox;
    [SerializeField] private Collider2D BaseCollider;
    [SerializeField] private Collider2D Slash2Collider;
    [SerializeField] private Collider2D Slash3Collider;
    [SerializeField] private Collider2D ThrustCollider;
    public const string Thrust1Name = "Thrust1";
    public const string Thrust2Name = "Thrust2";
    public const string Slash1Name = "Slash1";
    public const string Slash2Name = "Slash2";
    public const string Slash3Name = "Slash3";
    public const string DashLName = "DashL";
    public const string DashRName = "DashR";
    public const string Static1Name = "Static1";
    public const string Static2Name = "Static2";
    public const string PointName = "Point";

    [Header("Attack Values")]
    [SerializeField] private float ThrustSpeed;
    [SerializeField] private float DashSpeed;
    [SerializeField] private float SlashSpeed;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float ThrustDistance;
    [SerializeField] private float SlashDistance;

    private bool WalkTowardsPlayerBool;
    private bool PointTowardsPlayerBool;
    private bool ChooseActionBool;

    public bool DoneWithBigNukeBool;

    public Vector3 PhaseTwoPosition;

    private bool FadeOutBool;


    void Start()
    {
        Testing();
        MoveBoss = false;
        WalkTowardsPlayerBool = false;
        ChooseActionBool = true;
        PointTowardsPlayerBool = true;
        BaseCollider.enabled = false;
        Slash2Collider.enabled = false;
        Slash3Collider.enabled = false;
        ThrustCollider.enabled = false;
        DoneWithBigNukeBool = false;
        ThrustDistance = 4.1f;
        SlashDistance = 2.7f;
        manager.PhaseNum = 1;
        anim.Play(Static1Name);

        PhaseTwoPosition = new Vector3(0.4f, 2.9f, 0);
        FadeOutBool = false;
    }
    void Update()
    {
        if (FadeOutBool)
        {
            if (sprite.color.a - (0.5f * Time.deltaTime) < 0)
            {
                SetAlpha(0);
                FadeOutBool = false;
            }
            else
            {
                SetAlpha(sprite.color.a - (0.5f * Time.deltaTime));
            }
        }

        //Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        if (manager.PhaseNum == 1)
        {
           // Testing();
            if (AnimState == 0 && !MoveBoss && ChooseActionBool) //phase one
            {

                StartCoroutine("ChooseAction");
            }
        }
        if (PointTowardsPlayerBool)
        {
            PointTowards(player.gameObject.transform.position);
        }
        if (WalkTowardsPlayerBool)
        {
            WalkTowardsPlayer();
        }
    }
    IEnumerator ChooseAction()
    {
        ChooseActionBool = false;
        int i = Random.Range(0, 8);

        yield return new WaitForSeconds(0.5f); //base delay between moves

        float WaitTime = 0;
        float StartTime = 0;

        switch(i)
        {
            case 0:
                MoveSpeed = 1;
                WalkTowardsPlayerBool = true;
                yield return new WaitUntil(() => (Vector2.Distance(transform.position, player.transform.position) < ThrustDistance));
                StartCoroutine("Thrust");
                WalkTowardsPlayerBool = false;
                break;
            case 1:
                MoveSpeed = 3;
                WalkTowardsPlayerBool = true;
                yield return new WaitUntil(() => (Vector2.Distance(transform.position, player.transform.position) < SlashDistance));
                StartCoroutine("Slash");
                WalkTowardsPlayerBool = false;
                break;
            case 2:

                WaitTime = 5;
                StartTime = manager.GetTime();

                yield return new WaitUntil(() => ((manager.GetTime() - StartTime) > WaitTime) || (player.AnimState > 0 && (Vector2.Distance(transform.position, player.transform.position) < SlashDistance))); //wait until player is in range OR it's been WaitTime seconds
                if (((manager.GetTime() - StartTime) < WaitTime)) // if it hasn't been WaitTime seconds, attack. Else, get a new action
                {
                    StartCoroutine("DashR");
                    StartCoroutine("Slash");
                }
                break;
            case 3:

                WaitTime = 5;
                StartTime = manager.GetTime();

                yield return new WaitUntil(() => ((manager.GetTime() - StartTime) > WaitTime) || (player.AnimState > 0 && (Vector2.Distance(transform.position, player.transform.position) < SlashDistance)));
                StartCoroutine("DashL");
                StartCoroutine("Slash");
                break;
            case 4:

                WaitTime = 5;
                StartTime = manager.GetTime();

                yield return new WaitUntil(() => ((manager.GetTime() - StartTime) > WaitTime) || (player.AnimState > 0 && (Vector2.Distance(transform.position, player.transform.position) < SlashDistance)));
                StartCoroutine("DashL");
                StartCoroutine("Thrust");
                break;
        }
        ChooseActionBool = true;
        
    }


    private void PointTowards(Vector3 Object)
    {
        float angle;
        Vector3 mousePos = Object;
        mousePos.z = 0;
        Vector3 direction = mousePos - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentRotation = transform.rotation.eulerAngles.z;
        float rotationStep = RotationSpeed * Time.deltaTime;
        float newRotation = Mathf.LerpAngle(currentRotation, angle + 90, rotationStep);
        transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }
    private void WalkTowardsPlayer()
    {
        if (body.velocity.magnitude < MoveSpeed + 1)
        {
            body.velocity = MoveSpeed * -transform.up.normalized;
        }
    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.BossHit(collision.tag.ToString());
    }

    IEnumerator Thrust()
    {
        AnimState = 1;
        anim.Play(Thrust1Name);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => AnimDone(Thrust1Name));
        yield return new WaitForSeconds(0.7f);

        PointTowardsPlayerBool = false;

        manager.PlayMultiSound(BossFightManager.BossThrustSound);
        anim.Play(Thrust2Name);
        ThrustCollider.enabled = true;
        body.AddForce(new Vector2(-transform.up.x, -transform.up.y) * Mathf.Abs(ThrustSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(Thrust1Name));
        anim.Play(Static1Name);
        AnimState = 0;
        ThrustCollider.enabled = false;

        PointTowardsPlayerBool = true;

    }
    IEnumerator Slash()
    {

        AnimState = 2;
        anim.Play(Slash1Name);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(Slash1Name));
        PointTowardsPlayerBool = false;
        manager.PlayMultiSound(BossFightManager.BossSlashSound);
        anim.Play(Slash2Name);
        Slash2Collider.enabled = true;
        body.AddForce(new Vector2(-transform.up.x, -transform.up.y) * Mathf.Abs(SlashSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(Slash2Name));
        Slash2Collider.enabled = false;
        Slash3Collider.enabled = true;
        anim.Play(Slash3Name);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => AnimDone(Slash3Name));
        anim.Play(Static1Name);
        Slash3Collider.enabled = false;
        AnimState = 0;
        PointTowardsPlayerBool = true;
    }
    IEnumerator DashR()
    {
        AnimState = 3;
        anim.Play(DashRName);
        manager.PlayMultiSound(BossFightManager.BossDashSound);
        body.AddForce(new Vector2(transform.up.y, -transform.up.x) * Mathf.Abs(DashSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(DashRName));
        anim.Play(Static1Name);
        AnimState = 0;
    }
    IEnumerator DashL()
    {
        AnimState = 3;
        anim.Play(DashLName);
        manager.PlayMultiSound(BossFightManager.BossDashSound);
        body.AddForce(new Vector2(-transform.up.y, transform.up.x) * Mathf.Abs(DashSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(DashLName));
        anim.Play(Static1Name);
        AnimState = 0;
    }
    IEnumerator Point()
    {
        AnimState = 3;
        anim.Play(PointName);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(PointName));
        anim.Play(Static2Name);
        AnimState = 0;
    }
    IEnumerator BigNuke()
    {

        anim.Play("BigNuke1");
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => AnimDone("BigNuke1"));

        anim.Play("BigNuke2");
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => AnimDone("BigNuke2"));

        DoneWithBigNukeBool = true;
    }
    public void PlayBigNuke()
    {
        StartCoroutine("BigNuke");
    }
    public bool AnimDone(string name)
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
    private bool PlayingAnim(string name)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        int nameHash = stateInfo.shortNameHash;
        var controller = anim.runtimeAnimatorController;
        foreach (var animationClip in controller.animationClips)
        {
            if (Animator.StringToHash(animationClip.name) == nameHash)
            {
                if (name.Equals(animationClip.name))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DisableEverything(int phase)
    {
        StopAllCoroutines();
        BaseCollider.enabled = false;
        MoveBoss = false;
        WalkTowardsPlayerBool = false;
        PointTowardsPlayerBool = false;
        ChooseActionBool = false;
        body.velocity = Vector3.zero;
        if (phase == 1)
        {
            anim.Play(Static1Name);
        }
        else
        {
            anim.Play(Static2Name);
        }
        BaseCollider.enabled = false;
        Slash2Collider.enabled = false;
        Slash3Collider.enabled = false;
        ThrustCollider.enabled = false;
        BossHitbox.enabled = false;
        transform.rotation = Quaternion.identity;
    }
    public void FadeOut()
    {
        FadeOutBool = true;
    }
    public void PhaseTwoAnimation()
    {
        transform.position = PhaseTwoPosition;
        anim.Play("Blank");
        SetAlpha(1);
        anim.Play("Transition");
    }
    public void EnablePhaseTwo()
    {
        anim.Play("Static2");
        PointTowardsPlayerBool = true; //tenative
    }

    public bool BossInPosition()
    {

        if (sprite.color.a == 0)
        {
            body.rotation = 0;
            body.velocity = Vector3.zero;
            transform.position = PhaseTwoPosition;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayPoint()
    {
        StartCoroutine("Point");
    }

    public void SetAlpha(float a)
    {
        Color color = sprite.color;
        color.a = a;
        sprite.color = color;
    }
    private void Testing()
    {
        //UnityEngine.Debug.Log(Vector2.Distance(transform.position, PhaseTwoPosition) + " <- Position, angle -> " + ((Mathf.Atan2((Phase2Point - transform.position).y, (Phase2Point - transform.position).x) * Mathf.Rad2Deg) + 90 - transform.rotation.eulerAngles.z) % 360);

        if (Input.GetKeyDown(KeyCode.Period))
        {
            MoveBoss = !MoveBoss;
            FindObjectOfType<BossPlayerMovement>().CanMove = !MoveBoss;
        }
        if (!MoveBoss)
        {
            return;
        }




        if (body.velocity.magnitude < MoveSpeed + 1)
        {
            //Horizontal Movement
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapBox(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.05f, 0, 0), new Vector2(1f, 1f), 0, StopsMovement))
                {
                    //this.transform.position = this.transform.position + new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0);
                    body.velocity = new Vector3(MoveSpeed * Input.GetAxisRaw("Horizontal"), body.velocity.y);
                }
            }
            //Vertical Movement
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapBox(transform.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.05f, 0), new Vector2(1f, 1f), 0, StopsMovement))
                {
                    //this.transform.position = this.transform.position + new Vector3(0, MoveSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime, 0);
                    body.velocity = (new Vector3(body.velocity.x, MoveSpeed * Input.GetAxisRaw("Vertical"), 0));

                }

            }
            if (AnimState == 0) //Animation Moves, One at a time
            {

                if (Input.GetKeyDown(KeyCode.Space) && !player.DashingOnCD)
                {
                    bool Dashing = false;
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        StartCoroutine("DashR");
                        Dashing = true;
                    }
                    if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        StartCoroutine("DashL");
                        Dashing = true;
                    }
                    if (!Dashing)
                    {
                        StartCoroutine("DashL");
                        Dashing = true;
                    }
                    FindObjectOfType<BossHUDManager>().StartDashCD();
                }

                //Left Click Attack
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartCoroutine("Slash");
                }
                //Right Click Attack
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    StartCoroutine("Thrust");
                }
            }
        }
    }
}
