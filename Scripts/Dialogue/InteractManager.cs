using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    List<DialogueTrigger> list;
    int ActiveTrigger;
    void Start()
    {
        list = new List<DialogueTrigger>();
        ActiveTrigger = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && list.Count > 1)
        {
            ChangeActiveTrigger();
        }
    }
    private void ChangeActiveTrigger()
    {
        int i = 0;
        ActiveTrigger++;
        ActiveTrigger = ActiveTrigger % list.Count;
        foreach (DialogueTrigger d in list)
        {
            if (i == ActiveTrigger)
            {
                d.SetActive(true);
                //Debug.Log("Active Dialogue Trigger: " + d.gameObject.name);
            }
            else
            {
                //Debug.Log("Deactive Dialogue Trigger: " + d.gameObject.name);
                d.SetActive(false);
            }
            i++;
        }
    }
    public void AddTrigger(DialogueTrigger trigger)
    {
        if (list.Count == 0)
        {
            list.Add(trigger);
            trigger.SetActive(true);
        }
        else
        {
            list.Add(trigger);
            trigger.SetActive(false);
        }
    }
    public void RemoveTrigger(DialogueTrigger trigger)
    {
        if (list.IndexOf(trigger) == ActiveTrigger)
        {
            if (list.Count > 1)
            {
                list.Remove(trigger);
                ChangeActiveTrigger();
            }
            else
            {
                list.Clear();
                ActiveTrigger = 0;
            }
        }
        else
        {
            list.Remove(trigger);
        }
    }
}
