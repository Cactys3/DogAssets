using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private BossPlayerMovement player;
    [SerializeField] private BossFightManager manager;
    [SerializeField] private Animator anim;
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

    void Start()
    {
        MoveBoss = false;
        WalkTowardsPlayerBool = false;
        ChooseActionBool = true;
        PointTowardsPlayerBool = true;
        BaseCollider.enabled = false;
        Slash2Collider.enabled = false;
        Slash3Collider.enabled = false;
        ThrustCollider.enabled = false;
        ThrustDistance = 4.1f;
        SlashDistance = 2.7f;
        manager.PhaseNum = 1;
        anim.Play(Static1Name);
    }
    void Update()
    {
        Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        if (manager.PhaseNum == 1)
        {
            Testing();
        }
        if (PointTowardsPlayerBool)
        {
            PointTowardsPlayer();
        }
        if (WalkTowardsPlayerBool)
        {
            WalkTowardsPlayer();
        }
    }
    private void FixedUpdate()
    {
        if (AnimState == 0 && !MoveBoss && ChooseActionBool)
        {

            StartCoroutine("ChooseAction");
        }
    }
    IEnumerator ChooseAction()
    {
        ChooseActionBool = false;
        int i = Random.Range(0, 8);


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
                yield return new WaitUntil(() => (player.AnimState > 0 && (Vector2.Distance(transform.position, player.transform.position) < SlashDistance)));
                StartCoroutine("DashR");
                StartCoroutine("Slash");
                break;
            case 3:
                yield return new WaitUntil(() => (player.AnimState > 0 && (Vector2.Distance(transform.position, player.transform.position) < SlashDistance)));
                StartCoroutine("DashL");
                StartCoroutine("Slash");
                break;
            case 4:
                yield return new WaitUntil(() => (player.AnimState > 0 && (Vector2.Distance(transform.position, player.transform.position) < SlashDistance)));
                StartCoroutine("DashL");
                StartCoroutine("Thrust");
                break;
        }
        yield return new WaitForSeconds(0.5f);
        ChooseActionBool = true;
        
    }
    private void PointTowardsPlayer()
    {
        float angle;
        Vector3 mousePos = player.gameObject.transform.position;
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
    private void Testing()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            MoveBoss = !MoveBoss;
        }

        FindObjectOfType<BossPlayerMovement>().CanMove = !MoveBoss;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.BossHit(collision.tag.ToString());
    }

    IEnumerator Thrust()
    {
        AnimState = 1;
        anim.Play(Thrust1Name);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(Thrust1Name));
        anim.Play(Thrust2Name);
        ThrustCollider.enabled = true;
        body.AddForce(new Vector2(-transform.up.x, -transform.up.y) * Mathf.Abs(ThrustSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(Thrust1Name));
        anim.Play(Static1Name);
        AnimState = 0;
        ThrustCollider.enabled = false;
    }
    IEnumerator Slash()
    {
        AnimState = 2;
        anim.Play(Slash1Name);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(Slash1Name));
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
    }
    IEnumerator DashR()
    {
        AnimState = 3;
        anim.Play(DashRName);
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
        body.AddForce(new Vector2(-transform.up.y, transform.up.x) * Mathf.Abs(DashSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => AnimDone(DashLName));
        anim.Play(Static1Name);
        AnimState = 0;
    }

    private bool AnimDone(string name)
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

 
}
