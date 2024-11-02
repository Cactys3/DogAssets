using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FridgeOvenPlayerMovement : MonoBehaviour
{
    [SerializeField] private float Movespeed;
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float DiveSpeedRot;
    [SerializeField] private float DiveSpeedForce;
    [SerializeField] private float Uprightspeed;
    [SerializeField] private float FrictionValue;
    [SerializeField] private bool Upright;
    [SerializeField] private bool IsGrounded;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Sprite StaticSprite;
    [SerializeField] private Sprite MovingSprite;
    public const string LandingSound = "f_landing";
    public const string WinSound = "f_win";
    public const string GoalSound = "f_goal";
    public const string JumpSound = "f_jump";
    public const string FootstepsSound = "f_footsteps";
    public const string DiveSound = "f_dive";
    public const string GetUp1Sound = "f_getup_1";
    public const string GetUp2Sound = "f_getup_2";
    private Rigidbody2D body;
    private AudioManager AudioMan;
    private Vector3 Spawn;
    private bool IsMoving;
    private bool WasMoving;
    private SpriteRenderer sprite;
    private bool CanMove;
    private bool PlayLandingSound;
    private bool AllowFootsteps;
    void Start()
    {
        AllowFootsteps = true;
        PlayLandingSound = false;
        CanMove = true;
        FrictionValue = 0.9f;
        Upright = true;
        Spawn = this.transform.position;
        body = GetComponent<Rigidbody2D>();
        Movespeed = 4;
        JumpSpeed = 7;
        DiveSpeedRot = 30;
        DiveSpeedForce = 100;
        Uprightspeed = 0.57f;
        body.freezeRotation = true;
        sprite = GetComponent<SpriteRenderer>();
        WasMoving = false;
        AudioMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            body.angularDrag = 0.05f;//idk about this, maybe do this with upright or grounded?
        }
        else
        {
            body.angularDrag = 0.5f;
        }

        if (WasMoving != IsMoving && AllowFootsteps)
        {
            //Debug.Log("Was Moving: " + WasMoving);
            if (!WasMoving)
            {
                Play(FootstepsSound); //is set to loop
            }
            else
            {
                Stop(FootstepsSound);
            }
            WasMoving = IsMoving;
        }
        if (Playing(JumpSound))
        {
            Stop(FootstepsSound);
        }
        IsMoving = false;
        ////Debug.Log(body.constraints);

        IsGrounded = CheckIsGrounded();
        Upright = CheckUpright();

        if (Upright && Mathf.Abs( body.velocity.y ) > 1f && !PlayLandingSound)
        {
            //Debug.Log("play landing sound true!");
            PlayLandingSound = true;
        }

        if (Upright) //if not diving
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) //we diving now
            {
                int DiveSpeedSign = 0;
                int DiveSpinSign = -1;
                body.freezeRotation = false;
                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                {
                    DiveSpinSign = 1;
                    DiveSpeedSign = -1;
                }
                else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                {
                    DiveSpinSign = -1;
                    DiveSpeedSign = 1;
                }


                //body.AddForceAtPosition(new Vector2(DiveSpeedRot, 0), this.transform.position + new Vector3(0, 2, 0));
                IsMoving = true;
                body.AddTorque(DiveSpinSign * Mathf.Abs((DiveSpeedRot +0.5f) / (body.velocity.magnitude + 0.5f)));
                //Debug.Log(DiveSpinSign * Mathf.Abs((DiveSpeedRot + 0.5f) / (body.velocity.magnitude + 0.5f)));
                if (DiveSpeedSign == 0)
                {
                    body.AddForce(new Vector2(35 * DiveSpinSign, 0)); // to counter the rightward torque
                }
                else
                {
                    body.AddForce(new Vector2(DiveSpeedSign * DiveSpeedForce, 0));
                }
                StartCoroutine("PlayDiveSounds");
                //Debug.Log("added torq: " + DiveSpeedRot);
            }
            else
            {
                if (Input.GetKey(KeyCode.A) && IsGrounded) //move left
                {
                    body.velocity = new Vector2(-Movespeed, body.velocity.y);
                    IsMoving = true;
                }
                if (Input.GetKey(KeyCode.D) && IsGrounded) //move right
                {
                    body.velocity = new Vector2(Movespeed, body.velocity.y);
                    IsMoving = true;
                }
                if (Input.GetKey(KeyCode.Space) && IsGrounded) //jump!
                {
                    body.velocity = new Vector2(body.velocity.x, JumpSpeed);
                    IsMoving = true;
                    if (!Playing(JumpSound))
                    {
                        Stop(FootstepsSound);
                        Play(JumpSound);
                    }
                }
                if (Input.GetKey(KeyCode.S) && IsGrounded)
                {
                    body.velocity = new Vector2(body.velocity.y * 0.5f * Time.deltaTime, body.velocity.y * 0.5f * Time.deltaTime);
                    IsMoving = true;
                }
            }

        }
        else
        {
            
            if (Input.GetKeyDown(KeyCode.W) && !CheckUpright() && body.velocity.magnitude < 1) //to get upright, OLD: && (((Mathf.Abs(body.rotation) % 360) < 95 && (Mathf.Abs(body.rotation) % 360) > 85) || ((Mathf.Abs(body.rotation) % 360) < 275 && (Mathf.Abs(body.rotation) % 360) > 265))
            {
                PlayMultiple(GetUp1Sound);
                IsMoving = true;
                StartCoroutine("FreezeRotation");
                
                // Get the current rotation of the player
                float currentRotation = body.rotation % 360;

                // Calculate the difference between the current rotation and the upright position (0 degrees)
                float rotationDifference = Mathf.DeltaAngle(currentRotation, 0);

                // Determine the torque direction and magnitude needed to rotate upright
                float torque = rotationDifference * Uprightspeed;

                // Apply the torque to the Rigidbody2D
                // //Debug.Log("it's not upright so we add torque: " + 50 + " * " + torque/Mathf.Abs(torque));
                //Debug.Log("torque: " + torque);
                body.AddTorque(torque);
                

                //body.AddTorque(50 * (torque/Mathf.Abs(torque)));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if (IsMoving) //body.velocity.magnitude < 1
        {
            GetComponent<Animator>().Play("player");
            //sprite.sprite = StaticSprite;
        }
        else
        {
            GetComponent<Animator>().Play("player_static");
            //sprite.sprite = MovingSprite;
        }
    }
    private bool CheckIsGrounded()
    {
        // Check if there is any collider intersecting the groundCheck position within the groundCheckRadius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, 0.05f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("collide"))
            {
                ////Debug.Log("IsGrouded: true");
                return true;
            }
        }
        ////Debug.Log("IsGrouded: false");
        return false;
    }
    public bool CheckUpright()
    {
        bool temp = (Mathf.Abs(body.rotation % 360) < 1);
        ////Debug.Log("upright: " + temp);
        return temp;
    }

    private void Reset()
    {
        this.transform.position = Spawn;
        this.body.velocity = new Vector2(0, 0);
        this.body.rotation = 0;
        body.freezeRotation = true;
    }
    private IEnumerator FreezeRotation()
    {
        body.freezeRotation = false;
        while (!CheckUpright())
        {
            yield return null;
        }
        //Debug.Log("waiot until condition: ");
        PlayMultiple(GetUp2Sound);
        body.rotation = 0;
        body.freezeRotation = true;
    }
    private IEnumerator PlayDiveSounds()
    {
        float pitch = 1;
        if (body.velocity.magnitude > 0.5f)
        {
            pitch = Random.Range(1, 1.5f);
            pitch = 0.5f;
        }
        else
        {
            pitch = Random.Range(0.5f, 1f);
            pitch = 1.5f;
        }
        try{AudioMan.SetPitchSFX(DiveSound, pitch);}catch{}
        Play(DiveSound);
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(() => (body.velocity.magnitude + body.angularVelocity) < 0.8f);
        Stop(DiveSound);
    }
    private void FixedUpdate()
    {
        Friction();
    }
    private void Friction()
    {
        if (IsGrounded && !IsMoving && Upright)
        {
            body.velocity = body.velocity * FrictionValue;
        }
        if (!IsGrounded && Upright)
        {
            body.velocity = body.velocity + new Vector2(0, -0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("scene"))
        {
            StartCoroutine("NextLevel");
            CanMove = false;
            AllowFootsteps = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckIsGrounded())
        {
            //Debug.Log("play landing sound false!");
            Play(LandingSound);
            PlayLandingSound = false;
        }
    }
    private IEnumerator NextLevel()
    {
        Stop(FootstepsSound);
        Play(GoalSound);
        yield return new WaitForSeconds(3);
        FindObjectOfType<ChangeToScene>().ChangeScene();
    }
    private void Play(string s)
    {
        try
        {
            AudioMan.PlaySFX(s);
        }
        catch
        {
            Debug.Log( s + " couldn't play because no audioman");
        }
    }
    private void PlayMultiple(string s)
    {
        try
        {
            AudioMan.PlayMultipleSFX(s);
        }
        catch
        {
            Debug.Log(s + " couldn't play because no audioman");
        }
    }
    private void Stop(string s)
    {
        try
        {
            AudioMan.StopPlayingSFX(s);
        }
        catch
        {
            Debug.Log(s + " couldn't stop because no audioman");
        }
    }
    private bool Playing(string s)
    {
        try
        {
            return AudioMan.PlayingSFX(s);
        }
        catch
        {
            return false;
        }
    }
}

