using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableByInkVariable : MonoBehaviour
{
    [SerializeField] private string InkVariableName;
    private void OnEnable()
    {
        try
        {
            if ((bool)FindObjectOfType<DialogueManager>().GetVariableStateSystem(InkVariableName))
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
        catch
        {

        }
    }
}
