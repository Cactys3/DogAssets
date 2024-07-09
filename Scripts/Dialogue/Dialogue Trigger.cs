using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Keybind To Trigger NPC")]
    [SerializeField] private KeyCode interactKeybind;
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset text;
    [Header("Sprites")]
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite InactiveSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool playerInRange;
    private bool Active;
    private void Awake()
    {
        Active = true;
        visualCue.SetActive(false);
        playerInRange = false;
        //interactKeybind = KeyCode.E;
    }
    private void Update()
    {
        if (playerInRange && !FindObjectOfType<DialogueManager>().dialogueIsPlaying)
        {
            if (Active)
            {
                visualCue.SetActive(true);
                spriteRenderer.sprite = ActiveSprite;
                if (Input.GetKey(interactKeybind))
                {
                    FindObjectOfType<DialogueManager>().EnterDialogueMode(text);
                }
            }
            else
            {
                visualCue.SetActive(true);
                spriteRenderer.sprite = InactiveSprite;
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<InteractManager>().AddTrigger(this);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<InteractManager>().RemoveTrigger(this);
            playerInRange = false;
        }
    }

    public void SetActive(bool b)
    {
        Active = b;
    }
}
