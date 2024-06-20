using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FridgeOvenPlayerMovement : MonoBehaviour
{
    [SerializeField] private float Movespeed;
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float Divespeed;
    [SerializeField] private float Uprightspeed;
    [SerializeField] private float FrictionValue;
    [SerializeField] private bool Upright;
    [SerializeField] private bool IsGrounded;
    [SerializeField] private Transform GroundCheck;
    private Rigidbody2D body;
    private Vector3 Spawn;
    private bool IsMoving;
    void Start()
    {
        FrictionValue = 0.9f;
        Upright = true;
        Spawn = this.transform.position;
        body = GetComponent<Rigidbody2D>();
        Movespeed = 4;
        JumpSpeed = 7;
        Divespeed = 30;
        Uprightspeed = 0.56f;
        body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving = false;
        Debug.Log(body.constraints);

        IsGrounded = CheckIsGrounded();
        Upright = CheckUpright();

        if (Upright) //if not diving
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) //we diving now
            {
                body.freezeRotation = false;
                if (Input.GetKey(KeyCode.A))
                {
                    Divespeed = Mathf.Abs(Divespeed);
                }
                else
                {
                    Divespeed = Mathf.Abs(Divespeed) * -1;
                }
                //body.AddForceAtPosition(new Vector2(Divespeed, 0), this.transform.position + new Vector3(0, 2, 0));
                IsMoving = true;
                body.AddTorque(Divespeed);
                Debug.Log("added torq: " + Divespeed);
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
            
            if (Input.GetKeyDown(KeyCode.W) && (((Mathf.Abs(body.rotation) % 360) < 95 && (Mathf.Abs(body.rotation) % 360) > 85) || ((Mathf.Abs(body.rotation) % 360) < 275 && (Mathf.Abs(body.rotation) % 360) > 265))) //to get upright
            {
                IsMoving = true;
                StartCoroutine("FreezeRotation");
                
                // Get the current rotation of the player
                float currentRotation = body.rotation % 360;

                // Calculate the difference between the current rotation and the upright position (0 degrees)
                float rotationDifference = Mathf.DeltaAngle(currentRotation, 0);

                // Determine the torque direction and magnitude needed to rotate upright
                float torque = rotationDifference * Uprightspeed;

                // Apply the torque to the Rigidbody2D
                // Debug.Log("it's not upright so we add torque: " + 50 + " * " + torque/Mathf.Abs(torque));
                Debug.Log("torque: " + torque);
                body.AddTorque(torque);
                

                //body.AddTorque(50 * (torque/Mathf.Abs(torque)));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
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
                //Debug.Log("IsGrouded: true");
                return true;
            }
        }
        //Debug.Log("IsGrouded: false");
        return false;
    }
    private bool CheckUpright()
    {
        bool temp = (Mathf.Abs(body.rotation % 360) < 1);
        //Debug.Log("upright: " + temp);
        return temp;
    }

    private void Reset()
    {
        this.transform.position = Spawn;
        this.body.velocity = new Vector2(0, 0);
        this.body.rotation = 0;
    }
    private IEnumerator FreezeRotation()
    {
        body.freezeRotation = false;
        while (!CheckUpright())
        {
            yield return null;
        }
        Debug.Log("waiot until condition: ");
        body.freezeRotation = true;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Next scene: " + SceneManager.GetActiveScene().name);
        }
    }
}

