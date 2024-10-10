using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool DisableAfterPlay = false;
    [SerializeField] private bool DontDimAfterPlay = false;
    private const float DimAmount = 0.25f;
    [Header("Sound Effect To Play")]
    [SerializeField] private bool PlayEnterSoundOnce;
    [SerializeField] private string EnterSoundName;
    [SerializeField] private string ExitSoundName;
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
    private bool WaitingForInteract = false;
    private InteractManager interactManager;
    private DialogueManager dialogueMan;
    private AudioManager audioMan;

    static bool InteractedAlready = false;
    private void Awake()
    {
        Active = true;
        visualCue.SetActive(false);
        playerInRange = false;
        //interactKeybind = KeyCode.E;

    }
    private void Start()
    {
        interactManager = FindObjectOfType<InteractManager>();
        dialogueMan = FindObjectOfType<DialogueManager>();
        audioMan = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if (playerInRange && !dialogueMan.dialogueIsPlaying && !WaitingForInteract)
        {
            if (Active)
            {
                visualCue.SetActive(true);
                spriteRenderer.sprite = ActiveSprite;
                if (Input.GetKey(interactKeybind))
                {
                    if (!ExitSoundName.IsUnityNull() && !ExitSoundName.Equals(""))
                    {
                        dialogueMan.SetDialogueExitSound(ExitSoundName, true);
                    }
                    else
                    {
                        dialogueMan.SetDialogueExitSound("silent", false);
                    }

                    if (PlayEnterSoundOnce && !InteractedAlready)
                    {
                        Debug.Log("1");
                        InteractedAlready = true;
                        audioMan.PlaySFX(EnterSoundName);

                        StartCoroutine("DelayInteract", 0.5f + audioMan.GetLengthSFX(EnterSoundName)); //wait until the enter sound has played to start dialogue
                    }
                    else if (!PlayEnterSoundOnce && (!EnterSoundName.IsUnityNull() && !EnterSoundName.Equals("")))
                    {
                        Debug.Log("2");
                        audioMan.PlaySFX(EnterSoundName);

                        StartCoroutine("DelayInteract", 0.5f + audioMan.GetLengthSFX(EnterSoundName)); //wait until the enter sound has played to start dialogue
                    }
                    else
                    {
                        Debug.Log("3");
                        StartStory();
                    }
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
    IEnumerator DelayInteract(float time)
    {
        FindObjectOfType<PlayerMovementFree>().DialogueTriggerStoppingMovement = true;
        WaitingForInteract = true;
        yield return new WaitForSecondsRealtime(time);
        WaitingForInteract = false;
        FindObjectOfType<PlayerMovementFree>().DialogueTriggerStoppingMovement = false;
        StartStory();
    }
    public void StartStory()
    {
        InteractedAlready = true;
        dialogueMan.EnterDialogueMode(text);
        if (DisableAfterPlay)
        {
            dialogueMan.DisableAfterPlay(this.transform.parent.gameObject);
        }
        if (!DontDimAfterPlay)
        {
            Color c = spriteRenderer.color;
            c.a = DimAmount;
            spriteRenderer.color = c;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactManager.AddTrigger(this);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactManager.RemoveTrigger(this);
            playerInRange = false;
        }
    }

    public void SetActive(bool b)
    {
        Active = b;
    }
}
