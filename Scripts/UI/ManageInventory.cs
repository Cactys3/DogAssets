using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManageInventory : MonoBehaviour
{

    private Item[] ListOfItems;
    [SerializeField] private ManageUI UIScript;
    [SerializeField] private DialogueManager dialoguemanager;
    [SerializeField] private TextMeshProUGUI HeldDescriptionText;
    [SerializeField] private TextMeshProUGUI HoveredDescriptionText;
    private string HeldItem;
    private bool somethingIsHeld;
    private string HoveredItem;
    private bool somethingIsHovered;
    private string SelectedItem;
    private bool somethingIsSelected;
    void Start()
    {
        ListOfItems = GameObject.FindObjectsByType<Item>(0);

        Debug.Log(ListOfItems.Length);
        //go to dialoguemanager and ask what the state of each item is, then set the state of each item to that
        foreach (Item i in ListOfItems)
        {
            i.SetState((bool)FindObjectOfType<DialogueManager>().GetVariableStateSystem(i.GetName()));
            Debug.Log(FindObjectOfType<DialogueManager>().GetVariableStateSystem(i.GetName()));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(string name, bool state)
    {
        foreach (Item i in ListOfItems)
        {
            if (i.GetName().Equals(name))
            {
                i.SetState(state);
                return;
            }
        }
        Debug.LogWarning("called ManageInventory.SetState() but with a name we dont have an item for! name was: " + name);
    }

    public void ClearHovered(string name)
    {
        if (somethingIsHovered && HoveredItem.Equals(name))
        {
            somethingIsHovered = false;
            HoveredItem = null;

            HoveredDescriptionText.text = "Item Name: this is the default text the item description textbox would display given";
        }
        else
        { 
            Debug.LogWarning("clearHovered was called when something else was already being hovered?");
        }
    }
    public void SetHovered(string name)
    {
        HoveredItem = name;
        somethingIsHovered = true;

        switch (name)
        {
            case "has_dogfood":
                HoveredDescriptionText.text = "this is the description for dogfood";
                break;
            case "has_filledkeymold":
                HoveredDescriptionText.text = "has_filledkeymold";
                break;
            case "has_keymold":
                HoveredDescriptionText.text = "has_keymold";
                break;
            case "has_moldableclay":
                HoveredDescriptionText.text = "has_moldableclay";
                break;
            case "has_candy":
                HoveredDescriptionText.text = "has_candy";
                break;
            case "has_poop":
                HoveredDescriptionText.text = "has_poop";
                break;
            case "has_clay":
                HoveredDescriptionText.text = "has_clay";
                break;
            case "has_flimsykey":
                HoveredDescriptionText.text = "has_flimsykey";
                break;
            case "has_default":
                HoveredDescriptionText.text = "has_default";
                break;
            default:
                Debug.LogWarning("SetHovered(name) was called with a string name that isn't one of the items");
                break;
        }
    }
    public void SetSelected(string name)
    {
        if (!somethingIsSelected)
        {
            somethingIsSelected = true;
            SelectedItem = name;

            foreach (Item i in ListOfItems)
            {
                i.SomethingSelected(true);
            }

            Debug.Log("set selected to " + SelectedItem);
        }
        else
        {
            Debug.LogWarning("tried to set selected to " + name + " but selected item was already set: " + SelectedItem);
        }
    }
    public void ClearSelected(string name)
    {
        if (somethingIsSelected && name.Equals(SelectedItem))
        {
            somethingIsSelected = false;
            SelectedItem = null;

            foreach (Item i in ListOfItems)
            {
                i.SomethingSelected(false);
            }
        }
        else
        {
            Debug.LogWarning("Called ClearSelected but nothing was selected or clearselected(string) was called with the wrong name: " + name + " != " + SelectedItem);
        }
    }
    public void SetHold(string name)
    {
        if (somethingIsHeld == false)
        {
            HeldItem = name;
            somethingIsHeld = true;
            UIScript.DisableUI();

            switch (name)
            {
                case "has_dogfood":
                    HeldDescriptionText.text = "this is the description for dogfood";
                    break;
                case "has_filledkeymold":
                    HeldDescriptionText.text = "has_filledkeymold";
                    break;
                case "has_keymold":
                    HeldDescriptionText.text = "has_keymold";
                    break;
                case "has_moldableclay":
                    HeldDescriptionText.text = "has_moldableclay";
                    break;
                case "has_candy":
                    HeldDescriptionText.text = "has_candy";
                    break;
                case "has_poop":
                    HeldDescriptionText.text = "has_poop";
                    break;
                case "has_clay":
                    HeldDescriptionText.text = "has_clay";
                    break;
                case "has_flimsykey":
                    HeldDescriptionText.text = "has_flimsykey";
                    break;
                case "has_default":
                    HeldDescriptionText.text = "has_default";
                    break;
                default:
                    Debug.LogWarning("SetHold(name) was called with a string name that isn't one of the items");
                    break;
            }
        }
        else
        {
            Debug.LogError("tried to set an item to being held but an item was already held");
        }
        
    }
    public bool SomethingHeld()
    {
        return somethingIsHeld;
    }
    public bool SomethingSelected()
    {
        return somethingIsSelected;
    }
    public bool SomethingHovered()
    {
        return somethingIsHovered;
    }
    public string GetHeldItem()
    {
        return HeldItem;
    }
    public string GetSelectedItem()
    {
        return SelectedItem;
    }
    public string GetHoveredItem()
    {
        return HoveredItem;
    }
    public void ClearHeldItem(string name)
    {
        if (somethingIsHeld)
        {
            if (HeldItem.Equals(name))
            {
                HeldItem = null;
                somethingIsHeld = false;

                HeldDescriptionText.text = "Item Name: this is the default text the item description textbox would display given";
            }
            else
            {
                Debug.LogWarning("tried to clearhelditem with + " + name + " as an input but held item was " + HeldItem);
            }
        }
        else
        {
            Debug.LogWarning("tried to clearhelditem with + " + name + " as an input but somethingisheld was false");
        }
    }
    public void AddItem(string name)
    {
        foreach (Item i in ListOfItems)
        {
            if ((i.GetName()).Equals(name))
            {
                i.SetState(true);
                Debug.Log("just added the item: " + name);
                return;
            }
        }
        Debug.Log("didn't find the item: " + name);
    }
    public void TryCombine(string ItemTwo)
    {
        string Combined = "";
        string ItemOne = SelectedItem;
        if (SelectedItem == null)
        {
            Debug.LogError("trying to combine but selected item is null!!!");
        }
        if (ItemOne.CompareTo(ItemTwo) < 0)
        {
            Combined = ItemOne + ItemTwo;
        }
        else if (ItemOne.CompareTo(ItemTwo) > 0)
        {
            Combined = ItemTwo + ItemOne;
        }
        else
        {
            Debug.LogError("comparing these two strings to combine these two items resulted in equal strings?");
        }
        switch (Combined)
        {
            case "flimsykeymoldableclay":
                //It then sets the new item to true by calling DialogueMangager (It dones't have to set it true by the listofitems)
                Combine(Combined);
                break;
            default:
                foreach (Item i in ListOfItems)
                {
                    i.SomethingSelected(false);
                    //TODO: put up some UI text message that that combine attetmpt failed, have custom text for certian combinations (or all of them)
                }
                break;
        }
    }
    private void Combine(string Combined)
    {
        foreach (Item i in ListOfItems)
        {
            switch(Combined)
            {
                case "keymold":
                    FindObjectOfType<DialogueManager>().SetVariableStateSystem(Combined, false); //set the two combined items to false (dialogue manager will call SetState())
                    FindObjectOfType<DialogueManager>().SetVariableStateSystem(SelectedItem, false);
                    FindObjectOfType<DialogueManager>().SetVariableStateSystem("has_keymold", true); //set the new item to true  (dialogue manager will call SetState())
                    Debug.Log("just combined items: " + Combined + " and " + SelectedItem + " into item: " + "keymold");
                    break;
            }
        }
    }

    public DialogueManager GetDialogueManager()
    {
        return dialoguemanager;
    }
}
