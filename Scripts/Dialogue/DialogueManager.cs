using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
//TODO: gave error, i dont know if i need this: using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{

    // variable for the load_globals.ink JSON
    [Header("Load Globals JSON")]
    //[SerializeField] private TextAsset loadGlobalsJSON;

    //custom rich text tags
    private const string DELETE_CUSTOM = "del";
    private const string WAIT_CUSTOM = "wait";
    private const string SPACE_CUSTOM = "space";

    //ink tags
    private const string SOUND_TAG = "sound";
    private const string NAME_TAG = "name";
    private const string IMAGE_TAG = "image";
    private const string LAYOUT_TAG = "layout";
    private const string UNSKIPPABLE = "unskippable";
    private const string TYPING_SPEED = "typingspeed";
    private const string AUTO_SKIP = "autoskip";
    private const string CHANGE_SCENE = "scene";
    //TODO: add ink tags (or tie to the portrait tag): sound effect, font, font color, font size, delay until next dialogue
    [Header("Parameters and Tags")]
    private const float defaultTypingSpeed = 0.02f;
    private const float NarratorTypingSpeed = 0.02f;
    private const float DogTypingSpeed = 0.02f;
    private const float MicrowaveTypingSpeed = 0.02f;
    private const float FridgeTypingSpeed = 0.02f;
    private const float OvenTypingSpeed = 0.02f;
    private const float FridgeOvenTypingSpeed = 0.02f;
    private const float JuicerTypingSpeed = 0.02f;
    private float typingSpeed = 0.02f;
    private const string defaultTypingSound = "silent";
    private string currentTypingSound;
    //Scenes
    public const string demoendScene = "Demo End";
    public const string lockpickingScene = "Lockpicking Intro";
    public const string titleScene = "Titlescreen_Island";
    public const string introScene = "Intro Animation";
    public const string mudScene = "Mud Room";
    public const string kitchenScene = "Kitchen Dining Room";
    public const string bathroomScene = "Bathroom 1";
    public const string officeScene = "Office Room";
    public const string deckScene = "Deck";
    public const string livingScene = "Living Room";
    public const string dognipScene = "dognip";
    public const string bossScene = "Final Boss";
    public const string ending1Scene = "Ending1";
    public const string ending2Scene = "Ending2";
    public const string endchoiceScene = "EndChoice";
    public const string creditScene = "Credits";
    public const string juicerScene = "Juicer Minigame";
    public const string fridgeovenScene = "Fridge Level 1";
    public const string fridgeovenScene2 = "Fridge Level 2";
    public const string fridgeovenScene3 = "Fridge Level 3";
    public const string fridgeovenScene4 = "Fridge Level 4";
    public const string fridgeovenScene5 = "Fridge Level 5";
    public const string fridgeovenScene6 = "Fridge Level 6";
    public const string fridgeovenScene7 = "Fridge Level 7";
    public const string fridgeovenScene8 = "Fridge Level 8";
    public const string fridgeovenScene9 = "Fridge Level 9";
    public const string microwaveScene = "Microwave 1";
    public const string microwaveScene2 = "Microwave 2";
    public const string microwaveScene3 = "Microwave 3";
    public const string microwaveScene4 = "Microwave 4";
    public const string microwaveScene5 = "Microwave 5";
    public const string microwaveScene6 = "Microwave 6";
    public const string microwaveScene7 = "Microwave 7";
    //Names
    private const string NarratorName = "narrator";
    private const string DogName = "dog";
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;
    private bool canSkipTyping = true;
    private float autoSkip = 0f;
    private bool CanStartNewLine = true;
    //Sounds
    private const string NarratorSound = "narrator";
    private const string DogSound = "dog";
    private const string DogSoundBark = "dog_bark";
    private const string DogSoundGrowl = "dog_growl";
    private const string DogSoundWhine = "dog_whine";
    private const string DogSoundWoof = "dog_woof";
    private const string DogSoundSniff = "dog_sniff";
    private const string MicrowaveSound = "microwave";
    private const string FridgeSound = "fridge";
    private const string OvenSound = "oven";
    private const string FridgeOvenSound = "fridgeoven";
    private const string JuicerSound = "juicer";

    //Layouts
    private const string LayoutUP = "up";
    private const string LayoutDown = "down";
    // TODO: implement keybinds (this is the continue dialogue keybind)
    private KeyCode SubmitKeybind = KeyCode.Space;
    private static DialogueManager instance;
    private AnimationClip[] portraitClips;
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject canContinueIcon;

    [SerializeField] private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    private String currentLine;
    public bool dialogueIsPlaying { get; private set; } = false;

    private string DialogueExitSound;
    private bool DialogueExitSoundBool;

    private int LineNum = 0;

    private GameObject DisableAfterPlayObject;

    private DialogueVariables dialogueVariables;
    
    private void Awake()
    {
        // pass that variable to the DIalogueVariables constructor in the Awake method

        //dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        dialogueVariables = FindObjectOfType<DialogueVariables>();

        if (instance != null)
        {
            Debug.LogWarning("multiple instances of dialogue manager in scene");
        }
        instance = this;
    }
    private void Start()
    {
        SetVariables();
    }
    public void SetVariables()
    {
       // dialoguePanel = GameObject.FindGameObjectWithTag("dpanel");
      //  dialogueText = GameObject.FindGameObjectWithTag("dtext").GetComponent<TextMeshProUGUI>();
     //   displayNameText = GameObject.FindGameObjectWithTag("dname").GetComponent<TextMeshProUGUI>();
      //  canContinueIcon = GameObject.FindGameObjectWithTag("dicon");
     //   portraitAnimator = GameObject.FindGameObjectWithTag("dportrait").GetComponent<Animator>();
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        DisableAfterPlayObject = null;

        typingSpeed = defaultTypingSpeed;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        portraitClips = portraitAnimator.runtimeAnimatorController.animationClips;
        //Debug.Log(portraitClips.ToString());
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        canContinueIcon.SetActive(false);
    }
    private void Update()
    {
       //Debug.log(LineNum);
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(SubmitKeybind) && currentStory.currentChoices.Count == 0 && canContinueToNextLine)
        {
            ContinueStory();
        }

        if (canContinueIcon.activeSelf != canContinueToNextLine)
        {
            canContinueIcon.SetActive(canContinueToNextLine);
            //Debug.Log(canContinueToNextLine);
        }
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true); //USED TO BE SET ACTIVE HERE, NOW IS SET ACTIVE IN DisplayLine Coroutine

        dialogueVariables.StartListening(currentStory);

        resetEncounterTags();
        LineNum = 1;
        ContinueStory();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //if a line is already being displayed, stop it
            if (displayLineCoroutine != null)
            {
                Debug.LogWarning("DisplayLineCoroutine was already playing, this is weird");
                StopCoroutine(displayLineCoroutine);
            }
            //do we make it go character by character right here?
            //makes currentStory skip lines that are blank
            currentLine = currentStory.Continue();
            HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(currentLine));
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        if (DialogueExitSoundBool)
        {
            FindObjectOfType<AudioManager>().PlaySFX(DialogueExitSound);
        }

        if (DisableAfterPlayObject != null)
        {
            //Debug.Log("disable: " + DisableAfterPlayObject.name);
            DisableAfterPlayObject.SetActive(false);
            DisableAfterPlayObject = null;
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        LineNum++;
        //hides certain UI while typing out dialogue
        canContinueIcon.SetActive(false);
        HideChoices();

        //dont allow player to skip onto the next line before they see the current line fully printed out
        bool skip = false;
        canContinueToNextLine = false;

        bool addedAngleBracket = false;

        bool isAddingRichTextTag = false;
        bool isAddingCustomText = false;
        StringBuilder customText = new StringBuilder();
        int bracketIndex = 0;
        int visibleCharacters = 0;


        //Debug.Log("Start, VisibleCharacters: " + dialogueText.maxVisibleCharacters + " line: " + line.ToCharArray().Length);
        //empty the displayed text
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        int TestingValue = 0;

        //yield return new WaitForSeconds(0.1f);//wait for the tags to be handled cause that can take a sec


        if (line.Trim() == "")
        {
            Debug.LogWarning("skipped a blank line: " + LineNum);
            //yield return new WaitForSecondsRealtime(2f);
            ContinueStory();

        }
        else
        {
            foreach (char letter in line.ToCharArray())
            {
                
                //Debug.Log(typingSpeed + " character " + TestingValue);
                visibleCharacters++;
                if (visibleCharacters > line.Length + 1)
                {
                    Debug.LogError("nasty dialogue interact bug is still here");
                }
                //allows the dialogue to be skipped after 3 characters are displayed
                if (canSkipTyping && Input.GetKeyDown(SubmitKeybind) && visibleCharacters > 2)
                {
                    skip = true;
                    Debug.Log("skip100");
                }
                if (letter == '<')
                {
                    bracketIndex++;
                    if (bracketIndex == 1)
                    {
                       //Debug.log("<, start adding rich text");
                        isAddingRichTextTag = true;
                        addedAngleBracket = false;
                    }
                    if (bracketIndex == 2)
                    {
                       //Debug.log("<<, start adding custom text");
                        isAddingCustomText = true;
                        isAddingRichTextTag = false;
                    }
                    if (bracketIndex != 1 && bracketIndex != 2)
                    {
                       //Debug.log("<<<, there are too many <");
                        Debug.LogWarning("you typed three angle brackets in a row in an ink file, this is bad");
                    }
                }
                else if (isAddingCustomText)
                {
                    //Debug.Log(">");
                    if (letter != '<' && letter != '>')
                    {
                        customText.Append(letter);
                    }
                    if (letter == '>')
                    {
                       //Debug.log("<<>, done with custom: " + customText.ToString());
                        // '>' means that the tag is fully typed out, so now we Handle Custom Tags
                        //Debug.Log(customText.ToString());

                        string[] splitTag = customText.ToString().Split(':');
                        if (splitTag.Length != 2)
                        {
                            switch (customText.ToString())
                            {//if it's a one word tag then it goes to this switch statement
                                case DELETE_CUSTOM:
                                    //Debug.Log("delete triggered");
                                    DisplayChoices();
                                    yield return new WaitForSeconds(0.1f);
                                    HideChoices();

                                    // Calculate the number of characters to delete
                                    int charactersToDelete = Mathf.Min(dialogueText.maxVisibleCharacters, dialogueText.text.Length);

                                    // Adjust maxVisibleCharacters to hide the portion that has already been revealed
                                    dialogueText.maxVisibleCharacters -= charactersToDelete;

                                    // Adjust the displayed text to remove the deleted portion
                                    dialogueText.text = dialogueText.text.Substring(charactersToDelete);

                                    break;
                                default:
                                    Debug.LogWarning("Tag wasn't one of the custom tags" + tag);
                                    break;
                            }
                            // Remove the custom tag from the dialogueText.text
                            dialogueText.text = dialogueText.text.Replace("<<" + customText.ToString() + ">", "");
                        }
                        else
                        {
                            string tagValue = splitTag[1].Trim();
                            string tagKey = splitTag[0].Trim();
                            switch (tagKey)
                            {//if it's a two word tag then it goes to this switch statement
                                case WAIT_CUSTOM:
                                    //Debug.Log("wait triggered");
                                    if (!skip)
                                    {
                                        yield return new WaitForSeconds(float.Parse(tagValue));
                                    }
                                    break;
                                case SPACE_CUSTOM:
                                    //Debug.Log("space triggered");
                                    int spaceCount = int.Parse(tagValue);
                                    // Calculate the number of characters that are currently visible
                                    int visibleChars = Mathf.Min(dialogueText.text.Length, dialogueText.maxVisibleCharacters);
                                    // Insert spaces at the point where the visible text ends
                                    dialogueText.text = dialogueText.text.Insert(visibleChars, new string(' ', spaceCount));
                                    // Increase maxVisibleCharacters to make the added spaces visible
                                    dialogueText.maxVisibleCharacters += spaceCount;
                                   //Debug.log("SpaceCustom, VisibleCharacters: " + dialogueText.maxVisibleCharacters);
                                    break;
                                default:
                                    Debug.LogWarning("Tag wasn't one of the custom tags" + tag);
                                    break;
                            }
                            // Remove the custom tag from the dialogueText.text
                            dialogueText.text = dialogueText.text.Replace("<<" + customText.ToString() + ">", "");
                        }
                        customText = new StringBuilder();
                        bracketIndex = 0;
                        addedAngleBracket = true;
                        isAddingCustomText = false;
                        isAddingRichTextTag = false;
                    }
                }
                else if (isAddingRichTextTag)
                {//add the rich text tag info and stuff
                    //Debug.Log("<>");
                    if (!addedAngleBracket)
                    {
                        dialogueText.text += '<';
                        addedAngleBracket = true;
                    }
                    dialogueText.text += letter;
                    if (letter == '>')
                    {
                        customText = new StringBuilder();
                        bracketIndex = 0;
                        addedAngleBracket = true;
                        isAddingCustomText = false;
                        isAddingRichTextTag = false;
                    }
                }
                else
                {//add the letter and play a sound
                 //dialogueText.text += letter;
                    dialogueText.maxVisibleCharacters++;
                   //Debug.log(letter + "VisibleCharacters: " + dialogueText.maxVisibleCharacters + " skip: " + skip + " typespeed: " + typingSpeed);
                    //TODO: add a thing that makes it so a word jumps to the next line if it can't fit completely on the current line
                    if (!letter.ToString().Equals(" ") && !skip && currentTypingSound != "silent" && !isAddingCustomText)
                    {
                        //Debug.Log("Play Typing Sound: " + LineNum);
                        FindObjectOfType<AudioManager>().PlayTypeSound(currentTypingSound);
                    }
                    else if (letter.ToString().Equals(" "))
                    {
                        //TODO: maybe add here a thing for people who pause after completing a word?
                    }
                    //if we are skipping to the end, type all letters without miniscule delay
                    if (!skip) 
                    {
                        yield return new WaitForSeconds(typingSpeed);
                        //Debug.Log(typingSpeed + " type " + TestingValue);
                        TestingValue++;
                    }
                    else
                    {
                        //Debug.Log("skip2");
                        yield return new WaitForSeconds(0.001f);
                        //Debug.Log(typingSpeed + "no type"  + TestingValue);
                    }
                }
            }
            if (autoSkip > 0)
            {
                yield return new WaitForSeconds(autoSkip);
                ContinueStory();
            }
            // display choices if any
            DisplayChoices();
            canContinueIcon.SetActive(true);
            canContinueToNextLine = true;
        }
        LineNum--;
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogWarning("can't handle any more choices, stopped displaying choices");
        }

        int index = 0;

        //enable and initilize the choi8ces up to the amount of choices for this story
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }
    private void HideChoices()
    {
        foreach (GameObject c in choices)
        {
            c.SetActive(false);
        }
    }

    public void MakeChoice(int i)
    {
        if (canContinueToNextLine)
        {
            HideChoices();//NEWSTUFF
            currentStory.ChooseChoiceIndex(i);
            ContinueStory();
        }
    }
    private void resetEncounterTags()
    {
        //reset typing sound
        currentTypingSound = defaultTypingSound;
        //reset dialouge  UI
        displayNameText.text = "???";
        //portraitAnimator.Play("default");
        //layoutAnimator.Play("transparent");
        //Debug.Log("layout: " + "transparent");
        //reset image
        portraitAnimator.Play("default");
    }
    private void resetLineTags()
    {
        //default dont autoskip (0 means don't autoskip)
        autoSkip = 0f;
        //reset typing speed
        typingSpeed = defaultTypingSpeed;
        //can skip by default
        canSkipTyping = true;
    }
    private void HandleTags(List<String> tags)
    {
        resetLineTags();
        foreach (string tag in tags)
        {
            //make a list w/ the tag and tag value
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                switch (tag)
                {//if it's a one word tag then it goes to this switch statement
                    case UNSKIPPABLE:
                        canSkipTyping = false;
                        break;
                    default:
                        Debug.LogWarning("Tag failed to parse correctly" + tag);
                        break;
                }
            }
            else
            {
                string tagValue = splitTag[1].Trim();
                string tagKey = splitTag[0].Trim();

                switch (tagKey)
                {//if it's a two word tag then it goes to this switch statement
                    case NAME_TAG:
                        ChangeName(tagValue);
                        break;
                    case SOUND_TAG:
                        ChangeSound(tagValue);
                        break;
                    case TYPING_SPEED:
                        typingSpeed = float.Parse(tagValue);
                        break;
                    case CHANGE_SCENE:
                        ChangeScene(tagValue);
                        break;
                    case AUTO_SKIP:
                        autoSkip = float.Parse(tagValue);
                        break;
                    case IMAGE_TAG:
                        ChangePortrait(tagValue);
                        break;
                    case LAYOUT_TAG:
                        ChangeLayout(tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag isn't one of the defined keys: " + tag);
                        break;
                }
            }
        }
        //displayLineCoroutine = StartCoroutine(DisplayLine(currentLine));
    }
    private void ChangePortrait(string name)
    {
        bool isValidPortraitName = false;
        if (!portraitClips.IsUnityNull())
        {
            foreach (AnimationClip a in portraitClips)
            {
                if (a.name.Equals(name))
                {
                    isValidPortraitName = true;
                }
            }
        }
        else
        {
            Debug.LogWarning("PortraitClips is Unity Null again...");
        }
        if (isValidPortraitName)
        {
            portraitAnimator.Play(name);
        }
        else
        {
            Debug.LogWarning("Portrait Tag Value: " + name + " is not in the list of animation clips, playing default"); // (maybe it's named wrong in INK/portrait animation clip)
            portraitAnimator.Play("default");
        }
    }
    private void ChangeLayout(string name)
    {
        //Debug.Log("layout: " + name);
        switch (name)
        {
            case LayoutUP:
                layoutAnimator.Play("up");
                break;
            case LayoutDown:
                layoutAnimator.Play("down");
                break;
            default:
                layoutAnimator.Play("up");
                Debug.LogWarning("Couldn't Find Layout Tag: " + name);
                break;
        }
    }
    private void ChangeName(string name)
    {
        if (name != null)
        {
            displayNameText.text = name;
        }
        else
        {
            displayNameText.text = "???";
        }
    }
    private void ChangeSound(string name)
    {
        switch (name)
        {
            case DogSoundBark:
                currentTypingSound = "dog1";
                break;
            case DogSound:
                currentTypingSound = "dog1"; 
                break;
            case DogSoundGrowl:
                currentTypingSound = "dog3"; 
                break;
            case DogSoundWhine:
                currentTypingSound = "dog3"; 
                break;
            case DogSoundWoof:
                currentTypingSound = "dog4"; 
                break;
            case DogSoundSniff:
                currentTypingSound = "dog2";
                break;
            case NarratorSound:
                currentTypingSound = NarratorSound;
                break;
            case MicrowaveSound:
                currentTypingSound = MicrowaveSound;
                break;
            case FridgeSound:
                currentTypingSound = FridgeSound;
                break;
            case FridgeOvenSound:
                currentTypingSound = FridgeOvenSound;
                break;
            case OvenSound:
                currentTypingSound = OvenSound;
                break;
            case JuicerSound:
                currentTypingSound = JuicerSound;
                break;
            default:
                currentTypingSound = defaultTypingSound;
                Debug.LogWarning("Couldn't Find Sound Tag: " + name);
                break;
        }
    }
    public void ChangeScene(String name)
    {
        switch (name) //try to load scene by name
        {
            case "next":
                //Debug.Log("on this scene: " + SceneManager.GetActiveScene().buildIndex + ", changing to that + 1 which is: " + SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "kitchen":
                SceneManager.LoadScene(kitchenScene);
                return;
            case "office":
                SceneManager.LoadScene(officeScene);
                return;
            case "mud":
                SceneManager.LoadScene(mudScene);
                return;
            case "bath":
                SceneManager.LoadScene(bathroomScene);
                return;
            case "living":
                SceneManager.LoadScene(livingScene);
                return;
            case "dognip":
                SceneManager.LoadScene(dognipScene);
                break;
            case "boss":
                SceneManager.LoadScene(bossScene);
                break;
            case "end1":
                SceneManager.LoadScene(ending1Scene);
                break;
            case "end2":
                SceneManager.LoadScene(ending2Scene);
                break;
            case "endchoice":
                SceneManager.LoadScene(endchoiceScene);
                break;
            case "juicer":
                SceneManager.LoadScene(juicerScene);
                break;
            case "microwave":
                SceneManager.LoadScene(microwaveScene);
                break;
            case "fridgeoven":
                SceneManager.LoadScene(fridgeovenScene);
                break;
            default:
                Debug.LogWarning("tried to change scene through INK but may have encountered error with scenename: " + name);
                break; 
               
        }
    }
    public Ink.Runtime.Object GetVariableStateInk(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("tried to get variable of name: " + variableName + " but didn't find in dictionary");
        }
        return variableValue;
    }

    public void OnApplicationQuit()
    {
        SaveInk();
    }

    private void SaveInk()
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }
        else
        {
            Debug.LogWarning("Tried to save Ink Variables but dialogueVariables was null");
        }
    }

    public void SetVariableStateSystem(string name, System.Object value)
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SetVariable(name, value);
            GameObject.FindObjectOfType<ManageInventory>().UpdateItemStateList();
        }
        else
        {
            Debug.LogWarning("Tried to set an Ink variable, but Variables but dialogueVariables was null");
        }
    }//FindObjectOfType<DialogueManager>().SetDialogueVariable("unlocked_deck", true);       this is to do it anywhere
    public System.Object GetVariableStateSystem(string name)
    {
        if (dialogueVariables != null)
        {
            return dialogueVariables.GetVariable(name);
        }
        Debug.LogWarning("Tried to get an Ink variable, but Variables but dialogueVariables was null");
        return null;
    }

    public void SetDialogueExitSound(string s, bool b)
    {
        DialogueExitSound = s;
        DialogueExitSoundBool = b;
    }

    public void DisableAfterPlay(GameObject obby)
    {
        //Debug.Log("disable after play: " + obby.name);
        DisableAfterPlayObject = obby;
    }
}
