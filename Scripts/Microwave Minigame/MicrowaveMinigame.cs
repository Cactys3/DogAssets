using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicrowaveMinigame : MonoBehaviour
{
    private bool GameStarted;
    private bool GamePaused;
    Rigidbody2D Wave;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D WaveShooter;
    // Start is called before the first frame update
    void Start()
    {
        Wave = GetComponent<Rigidbody2D>();
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameStarted)
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.R) && GameStarted)
        {
            ResetGame();
        }

        if (!GameStarted)
        {
            float RotationSpeed = 50;
            float angle = Mathf.Atan2(TowardsMouse(WaveShooter.position).y, TowardsMouse(WaveShooter.position).x) * Mathf.Rad2Deg;
            if (angle > (WaveShooter.rotation) + 0.5f && WaveShooter.rotation < 90)
            {
                WaveShooter.SetRotation(WaveShooter.rotation + (RotationSpeed * Time.deltaTime));
                Wave.SetRotation(WaveShooter.rotation + 90 + 180);
                Wave.position = new Vector2(-4.62f + Mathf.Cos(Mathf.Deg2Rad * WaveShooter.rotation) * 0.6528f, 1.8f + Mathf.Sin(Mathf.Deg2Rad * WaveShooter.rotation) * 0.6528f);
            }
            else if (angle < (WaveShooter.rotation) - 0.5f && WaveShooter.rotation > -90)
            {
                WaveShooter.SetRotation(WaveShooter.rotation - (RotationSpeed * Time.deltaTime));
                Wave.SetRotation(WaveShooter.rotation + 90 + 180);
                Wave.position = new Vector2(-4.62f + Mathf.Cos(Mathf.Deg2Rad * WaveShooter.rotation) * 0.6528f, 1.8f + Mathf.Sin(Mathf.Deg2Rad * WaveShooter.rotation) * 0.6528f);
                // 0.6528
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouse moiuse mouse");
            }
        }
    }

    private void StartGame()
    {
        Debug.Log("start game");
        GameStarted = true;
        //Wave.AddForce(TowardsMouse(Wave.position) * speed, ForceMode2D.Impulse);
        //Wave.AddForce(speed * new Vector2(WaveShooter.rotation.y, WaveShooter.position.x), ForceMode2D.Impulse);

        float ForceMagnitude = 10;

        Wave.AddForce(new Vector2(Mathf.Cos(WaveShooter.rotation * Mathf.Deg2Rad), Mathf.Sin(WaveShooter.rotation * Mathf.Deg2Rad)) * ForceMagnitude, ForceMode2D.Impulse);
        SetRotation();


    }
    private void ResetGame()
    {
        Debug.Log("reset game");
        GameStarted = false;
        Wave.position = new Vector2(-4f, 1.8f);
        Wave.SetRotation(0);
        Wave.velocity = new Vector2(0, 0);
        Wave.SetRotation(WaveShooter.rotation + 90 + 180);
        Wave.position = new Vector2(-4.62f + Mathf.Cos(Mathf.Deg2Rad * WaveShooter.rotation) * 0.6528f, 1.8f + Mathf.Sin(Mathf.Deg2Rad * WaveShooter.rotation) * 0.6528f);
    }
    private Vector2 TowardsMouse(Vector2 ObjectPosition)
    {
        // Get the mouse position in screen coordinates
        Vector2 MouseScreenPosition = Input.mousePosition;

        // Convert mouse position from screen coordinates to world coordinates
        Vector2 MouseWorldPosition = Camera.main.ScreenToWorldPoint(MouseScreenPosition);

        // Calculate the direction vector from the object to the mouse
        Vector2 Direction = MouseWorldPosition - ObjectPosition;

        // Optional: Normalize the direction vector to get a unit vector
        Vector2 NormalizedDirection = Direction.normalized;

        // Debugging: Draw a line in the scene view to visualize the direction
        Debug.DrawLine(ObjectPosition, MouseWorldPosition, Color.red);

        // Use the direction vector for something (e.g., move towards the mouse position)
        return NormalizedDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collider " + collision.tag);
        if (collision.tag.Equals("goal"))
        {
            WinLevel();
        }
        if (collision.tag.Equals("death"))
        {
            Debug.Log("death");
            ResetGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetRotation();
    }

    private void SetRotation()
    {
        //float angle = Mathf.Atan2(TowardsMouse(WaveShooter.position).y, TowardsMouse(WaveShooter.position).x) * Mathf.Rad2Deg;
        float angle = WaveShooter.rotation;
        Debug.Log(angle);
        Wave.SetRotation(angle + 180 + 90);
    }

    private void WinLevel()
    {
        Debug.Log("Won level, go to next level");
        //SceneManager
    }
}
