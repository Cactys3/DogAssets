using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset text;
    [SerializeField] bool OnEnableBool;
    private bool Played = false;
    private bool LateUpdateBool = false;
    private void OnEnable()
    {
        if (OnEnableBool)
        {
            LateUpdateBool = true;
        }
    }
    public void Play()
    {
        Debug.Log("Play " + text.name);
        FindObjectOfType<DialogueManager>().EnterDialogueMode(text);
        Played = true;
    }
    private void LateUpdate()
    {
        if (LateUpdateBool)
        {
            Play();
            LateUpdateBool = false;
        }
    }
}
