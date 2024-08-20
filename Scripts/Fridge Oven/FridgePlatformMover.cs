using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgePlatformMover : MonoBehaviour
{
    public MovementDirection DirectionVar = new MovementDirection();
    [SerializeField] float RightMax;
    [SerializeField] float LeftMax;
    [SerializeField] float UpMax;
    [SerializeField] float DownMax;
    [SerializeField] Vector3 StartPos;
    [SerializeField] bool HorizontalBool; //True = Right, False = Left
    [SerializeField] bool VerticalBool; //True = Up, False = Down
    [SerializeField] bool SquareBool; //True = Vertical, False = Horizontal
    [SerializeField] int SquareDirection; //should be -1 for Clockwise or 1 for CounterClockwise (maybe?)
    bool PlayerOnPlatform;
    public float Speed; 
    private Rigidbody2D body;
    private void Start()
    {
        transform.position = StartPos;
        //body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        switch (DirectionVar)
        {
            case MovementDirection.Horizontal:
                Horizontal();
                break;
            case MovementDirection.Vertical:
                Vertical();
                break;
            case MovementDirection.SquareMovement:
                SquareMovement();
                break;
            default:
                Debug.LogError("FridgePlatformMover on object: " + this.name + " isn't setup");
                break;
        }
    }
    private void Horizontal()
    {
        if (HorizontalBool)
        {
            //body.velocity = new Vector2(Speed, 0);
            if (transform.position.x >= RightMax)
            {

                HorizontalBool = !HorizontalBool;
                PlaySound();
            }
        }
        else
        {
            if (transform.position.x <= LeftMax)
            {
                HorizontalBool = !HorizontalBool;
                PlaySound();
            }
            //body.velocity = new Vector2(Speed * -1, 0);
        }
    }
    private void Vertical()
    {
        if (VerticalBool)
        {
            //body.MovePosition(new Vector2(StartPos.x, body.position.y + Speed));
           // body.AddForce(new Vector2(0, Speed));
            //body.velocity = new Vector2(0, Speed);
            transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
            if (transform.position.y >= UpMax)
            {
                VerticalBool = false;
                PlaySound();
                Debug.Log("changed to Up");
            }
        }
        else
        {
            //body.MovePosition(new Vector2(StartPos.x, body.position.y + (-1 * Speed)));
            //body.AddForce(new Vector2(0, Speed * -1));
            //body.velocity = new Vector2(0, Speed * -1);
            transform.position += new Vector3(0, Speed * -1 * Time.deltaTime, 0);
            if (transform.position.y <= DownMax)
            {
                Debug.Log("changed to Down");
                VerticalBool = true;
                PlaySound();
            }
        }
        if (PlayerOnPlatform && FindObjectOfType<FridgeOvenPlayerMovement>().CheckUpright())
        {
            GameObject collision = FindObjectOfType<FridgeOvenPlayerMovement>().gameObject;
            collision.transform.position = new Vector2(collision.transform.position.x, transform.position.y + 0.881f);
            Debug.Log("setpos");
        }
    }
    private void SquareMovement()
    {
        if (SquareBool)
        {

        }
        else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = false;
        }
    }
    private void PlaySound()
    {
        FindObjectOfType<AudioManager>().PlayingSFX("f_platform");
    }
}

public enum MovementDirection
{
    Horizontal,
    Vertical,
    SquareMovement
};


