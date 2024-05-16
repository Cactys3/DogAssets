using UnityEngine;

public class PlayerMovementGrid : MonoBehaviour
{
    private bool wasHorizontal;
    private bool horizontal;
    private bool vertical;
    private float MoveSpeed;
    public Transform MovePoint;
    public LayerMask StopsMovement;
    void Start()
    {
        MoveSpeed = 3f;
        MovePoint.parent = null;
        horizontal = false;
        vertical = false;
        wasHorizontal = false;
    }

    // Update is called once per frame
    void Update()
    {
        //can't move during dialogue
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, MovePoint.position) <= 0.1f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //One Block Wide Player: if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.9f, 0, 0), new Vector2(0.9f, 0.9f), 0, StopsMovement))
                //Two Block Wide Player: if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.9f, 0, 0), new Vector2(1.9f, 0.9f), 0, StopsMovement))
                if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.9f, 0, 0), new Vector2(0.9f, 0.9f), 0, StopsMovement))
                {
                    horizontal = true;
                    //move horizontal animation
                }
                else
                {
                    horizontal = false;
                    //bump into collider animation horizontal
                }
            }
            else
            {
                horizontal = false;
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                //One Block Wide Player:if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.9f, 0), new Vector2(0.9f, 0.9f), 0, StopsMovement))
                //Two Block Wide Player:if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.9f, 0), new Vector2(1.9f, 0.9f), 0, StopsMovement))
                if (!Physics2D.OverlapBox(MovePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical") * 0.9f, 0), new Vector2(0.9f, 0.9f), 0, StopsMovement))
                {
                    vertical = true;
                    //move verical animation
                }
                else
                {
                    vertical = false;
                    //bump into collider animation vertical
                }
            }
            else
            {
                vertical = false;
            }
            //this all just makes it prioritize whichever direction was pressed last but also revert to the previous direction if that newly pressed direction is released
            if (vertical || horizontal)
            {
                if (vertical && horizontal)
                {
                    if (wasHorizontal)
                    {
                        MovePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    }
                    else
                    {
                        MovePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    }
                }
                else if (vertical)
                {
                    wasHorizontal = false;
                    MovePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                }
                else if (horizontal)
                {
                    wasHorizontal = true;
                    MovePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                }
            }
        }
    }
}

