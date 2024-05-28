using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ReflectorScript : MonoBehaviour
{
    [SerializeField] Transform blue;
    [SerializeField] Transform pink;
    [SerializeField] float MaxAngle;
    [SerializeField] float MinAngle;
    [SerializeField] GameObject SelectedVisual;
    [SerializeField] GameObject HoverVisual;
    MicrowaveMinigame wave;
    Rigidbody2D waveBody;
    private float AngleMethodValue;
    private bool AngleMethodBool;
    private bool ObjectHovered;
    private bool ObjectSelected;
    private int ObjectState; //0: game started (disabled), 1: neutral, 2: selected
    // Start is called before the first frame update
    void Start()
    {
        AngleMethodBool = false;
        AngleMethodValue = 0;
        ObjectState = 1;
        SetSelected(false);
        SetHovered(false);
        wave = FindObjectOfType<MicrowaveMinigame>();
        waveBody = wave.gameObject.GetComponent<Rigidbody2D>();
        if (wave == null)
        {
            Debug.LogError("couldn't find microwave minigame script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ObjectState);
        //change objectstate if needed
        if (FindObjectOfType<MicrowaveMinigame>().IsGameStarted())
        {
            ChangeObjectState(0); //go into play mode
        }
        else if (ObjectState == 0)
        {
            ChangeObjectState(1); //go out of play mode
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (ObjectState == 2)
            {
                ChangeObjectState(1); //deselect it
            }
            else if (ObjectHovered && ObjectState == 1)
            {
                ChangeObjectState(2); //select it
            }
        }

        //work based on objectstate, maybe do nothing here?
        switch (ObjectState)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:
                Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
                //when selected, set rotation to point towards the cursor
                float RotationSpeed = 50;
                float angle2 = Mathf.Atan2(TowardsMouse(body.position).y, TowardsMouse(body.position).x) * Mathf.Rad2Deg;
                float angle = ToCircle(angle2);
                float bodyAngle = ToCircle(body.rotation + 90);

                if ((AngleMethodValue < 0) && !(bodyAngle > MinAngle) || (AngleMethodValue > 0) && !(bodyAngle < MaxAngle))
                {
                    AngleMethodValue = 1;
                    AngleMethodBool = false;
                }
                if ((angle > bodyAngle + 0.05f) && (bodyAngle < MaxAngle))
                {
                    if (angle > MaxAngle)
                    {
                        if (AngleMethodBool)
                        {
                            body.SetRotation(body.rotation + (RotationSpeed * Time.deltaTime * AngleMethodValue));
                        }
                    }
                    else
                    {
                        AngleMethodBool = true;
                        AngleMethodValue = 1;
                        body.SetRotation(body.rotation + (RotationSpeed * Time.deltaTime));
                    }
                }
                else if ((angle < bodyAngle - 0.05f) && (bodyAngle > MinAngle))
                {
                    if (angle < MinAngle)
                    {
                        if (AngleMethodBool)
                        {
                            body.SetRotation(body.rotation + (RotationSpeed * Time.deltaTime * AngleMethodValue));
                        }
                    }
                    else
                    {
                        AngleMethodBool = true;
                        AngleMethodValue = -1;
                        body.SetRotation(body.rotation - (RotationSpeed * Time.deltaTime));
                    }
                }




                //Debug.Log(angle);
                //Debug.Log(bodyAngle);
                break;
            default:

                break;

        }
    }

    private float ToCircle(float num)
    {
        if (num < 0)
        {
            num = 360 + num;
        }
        num = num % 360f;
        return num;
    }

    private void ChangeObjectState(int NewState)
    {
        if (NewState == ObjectState)
        {
            Debug.Log("Tried switching reflector object state to what it already was: " + ObjectState);
        }

        //work based on objectstate
        switch (NewState)
        {
            case 0:
                if (ObjectState == 2)
                {
                    SetSelected(false);
                }
                if (ObjectHovered)
                {
                    SetHovered(false);
                }
                ObjectState = 0;
                break;
            case 1:
                if (ObjectState == 2)
                {
                    SetSelected(false);
                }
                ObjectState = 1;
                break;
            case 2:
                SetHovered(false);
                SetSelected(true);
                ObjectState = 2;
                break;
            default:
                Debug.LogError("Reflector script called changeobjectstate with the wrong number (impossible)");
                break;

        }
    }

    public void OnSideCollision(Collider2D collision, string reflectorTag)
    {
        Transform a = waveBody.gameObject.transform;
        switch (reflectorTag)
        {
            case "pink reflector":
                StartCoroutine("DisableReflectors");
                Vector2 velocity = waveBody.velocity;
                waveBody.position = blue.position;
                waveBody.velocity = new Vector2(waveBody.velocity.x, waveBody.velocity.y) * new Vector2(blue.up.x + 1, -(blue.up.y)); //multiplies normalized rotation of pink/blue part of the reflector to the velocity of the wave to impart the reflector's direction
                break;
            case "blue reflector":
                StartCoroutine("DisableReflectors");
                Vector2 velocity2 = waveBody.velocity;
                waveBody.position = pink.position;
                waveBody.velocity = new Vector2(waveBody.velocity.x, waveBody.velocity.y) * new Vector2(pink.up.x + 1, -(pink.up.y)); //multiplies normalized rotation of pink/blue part of the reflector to the velocity of the wave to impart the reflector's direction
                break;
            default:
                Debug.LogWarning("called onsidecollision but wasn't blue or pink reflector tag");
                break;
        }

    }

    private IEnumerator DisableReflectors()
    {
        Debug.Log("ienumerators");
        blue.gameObject.SetActive(false);
        pink.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        blue.gameObject.SetActive(true);
        pink.gameObject.SetActive(true);
    }

    private void OnMouseEnter()
    {
        if (ObjectState == 1)
        {
            SetHovered(true);
        }
    }

    private void OnMouseExit()
    {
        if (ObjectState == 1)
        {
            SetHovered(false);
        }
    }
    private void SetSelected(bool b)
    {
        ObjectSelected = b;
        switch (ObjectSelected)
        {
            case true:
                SelectedVisual.SetActive(true);
                break;
            case false:
                SelectedVisual.SetActive(false);
                break;
        }
    }
    private void SetHovered(bool b)
    {
        ObjectHovered = b;
        switch (ObjectHovered)
        {
            case true:
                HoverVisual.SetActive(true);
                break;
            case false:
                HoverVisual.SetActive(false);
                break;
        }
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
}
