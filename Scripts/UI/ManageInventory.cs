using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ManageInventory : MonoBehaviour
{
    private string[] ListOfItemNames;
    private Item[] ListOfItems;
    private List<string> EnabledItems;
    [SerializeField] private ManageUI UIScript;
    private DialogueManager dialoguemanager;
    [SerializeField] private TextMeshProUGUI HeldDescriptionText;
    [SerializeField] private TextMeshProUGUI HoveredDescriptionText;
    private string HeldItem;
    private bool somethingIsHeld;
    private string HoveredItem;
    private bool somethingIsHovered;
    private string SelectedItem;
    private bool somethingIsSelected;

    public const string dogfood = "has_dogfood";
    public const string defaultitem = "has_default";
    public const string flimsykey = "has_flimsykey";
    public const string clay = "has_clay";
    public const string poop = "has_poop";
    public const string candy = "has_candy";
    public const string moldableclay = "has_moldableclay";
    public const string keymold = "has_keymold";
    public const string tastykeymold = "has_tastyfilledkeymold";
    public const string stinkyfilledkeymold = "has_stinkyfilledkeymold";
    public const string stinkykey = "has_stinkykey";
    public const string tastykey = "has_tastykey";


    void Start()
    {
        //    UIScript = FindAnyObjectByType<ManageUI>();
        dialoguemanager = FindAnyObjectByType<DialogueManager>();
        //   HeldDescriptionText = GameObject.FindGameObjectWithTag("hovered").GetComponent<TextMeshProUGUI>();
        //   HoveredDescriptionText = GameObject.FindGameObjectWithTag("held").GetComponent<TextMeshProUGUI>();

        ListOfItems = GameObject.FindObjectsByType<Item>(FindObjectsInactive.Include, 0);
        ListOfItemNames = new string[] { dogfood, defaultitem, flimsykey, clay, poop, candy, moldableclay, keymold, stinkyfilledkeymold, tastykeymold, stinkykey, tastykey };
        EnabledItems = new List<string>
        {
            "hello"
        };
        EnabledItems.Clear();

        UpdateItemStateList(); //Sets the EnabledItems list properly.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            HoveredDescriptionText.text = "";
            HeldDescriptionText.text = "Holding no item!";
        }
    }
    /**
     * Sets the EnabledItems list properly
     */
    public void UpdateItemStateList()
    {


        //first we need to make sure the EnabledItems list is complete and correctly ordered. Here we also set disabled items.
        foreach (Item i in ListOfItems)
        {
            if ((bool)DialogueManager.GetInstance().GetVariableStateSystem(i.GetName())) //if the item should be enabled               ((bool)FindObjectOfType<DialogueManager>().GetVariableStateSystem(i.GetName()))
            {
                if (!EnabledItems.Contains(i.GetName())) //if item is newly being added to the list
                {
                    EnabledItems.Add(i.GetName()); //add it to the list
                }
            }
            else //if the item should be disabled
            {
                if (EnabledItems.Contains(i.GetName())) //if item was previously enabled, remove it from the list
                {
                    EnabledItems.Remove(i.GetName());
                }
            }
        }
    }
    /**
    private void UpdateItemStates()
    {
        //now that the EnabledItems list is correct, we setup the EnabledItems
        foreach (Item i in ListOfItems)
        {
            if (EnabledItems.Contains(i.GetName())) //if it should be enabled
            {
                i.SetState(EnabledItems.IndexOf(i.GetName())); //Set its ItemState to its place in the list
            }
            else //if it should be disabled
            {
                i.SetState(-1); //set it to -1 which means it won't display itself
            }
        }
    } */
    public int GetItemSlot(string name)
    {
        if (EnabledItems.Contains(name))
        {
            return EnabledItems.IndexOf(name);
        }
        return -1;
    }
    /**
    public void SetState(string name, bool state) old, replaced by UpdateItemState();
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
    }**/

    public void ClearHovered(string name)
    {
        if (somethingIsHovered && HoveredItem.Equals(name))
        {
            somethingIsHovered = false;
            HoveredItem = null;

            HeldDescriptionText.text = "Holding no item!";
            //HoveredDescriptionText.text = "";

        }
        else
        {
            Debug.LogWarning("clearHovered was called when something else was already being hovered? " + name + " != " + HoveredItem);
        }
    }
    public void SetHovered(string name)
    {
        HoveredItem = name;
        somethingIsHovered = true;

        switch (name)
        {
            case dogfood:
                HoveredDescriptionText.text = "dogfood stinky...";
                break;
            case stinkyfilledkeymold:
                HoveredDescriptionText.text = "filled key mold, you could cook i";
                break;
            case tastykeymold:
                HoveredDescriptionText.text = "filled key mold, you could cook it?";
                break;
            case keymold:
                HoveredDescriptionText.text = "keymold, u like... freeze it? Or no! You add the material to it!";
                break;
            case moldableclay:
                HoveredDescriptionText.text = "Moldable Clay... You could put something in it to create a mold!";
                break;
            case candy:
                HoveredDescriptionText.text = "Candy! Yum! Don't eat!";
                break;
            case poop:
                HoveredDescriptionText.text = "Poopy! Yum! Don't eat!";
                break;
            case clay:
                HoveredDescriptionText.text = "Clay! It's hard tho... Maybe dip it in some water so you can work with it!";
                break;
            case flimsykey:
                HoveredDescriptionText.text = "Toy key! Kids like you would love this!";
                break;
            case defaultitem:
                HoveredDescriptionText.text = "What?! This is the default?!! You can't have this!!! GIVE IT BACK NOW.";
                break;
            case tastykey:
                HoveredDescriptionText.text = "i want to eat it (try using it on that locked door in the living room)";
                break;
            case stinkykey:
                HoveredDescriptionText.text = "i want to eat it (try using it on that locked door in the living room)";
                break;
            default:
                Debug.LogWarning("SetHovered(name) was called with a string name that isn't one of the items: " + name);
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
        RefreshItems();
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
        RefreshItems();
    }
    public void SetHold(string name)
    {
        if (!somethingIsHeld)
        {
            if (ListOfItemNames.Contains(name))
            {
                HeldDescriptionText.text = "Holding a " + name;
                HeldItem = name;
                somethingIsHeld = true;
                FindObjectOfType<DialogueManager>().SetVariableStateSystem("holding_item", true);
                FindObjectOfType<DialogueManager>().SetVariableStateSystem("held_item", name);
                UIScript.DisableUI();
            }
            else
            {
                Debug.LogWarning("SetHold() was called with a string name: " + name + " that isn't one of the items");
            }
            switch (name)
            {
                case dogfood:
                    HeldDescriptionText.text = "Holding dogfood";
                    break;
                case stinkyfilledkeymold:
                    HeldDescriptionText.text = "Holding a keymold filled with poop...   i guess you could bake it to make the key?";
                    break;
                case tastykeymold:
                    HeldDescriptionText.text = "Holding a keymold filled with poop...   i guess you could bake it to make the key?";
                    break;
                case "has_keymold":
                    HeldDescriptionText.text = "Holding a keymold! Why?";
                    break;
                case "has_moldableclay":
                    HeldDescriptionText.text = "Holding moldableclay! Why?";
                    break;
                case "has_candy":
                    HeldDescriptionText.text = "Holding candy! Why?";
                    break;
                case "has_poop":
                    HeldDescriptionText.text = "Holding poop! Why?";
                    break;
                case "has_clay":
                    HeldDescriptionText.text = "Holding clay! It's a little hard though, dipping it in some water might do the trick!";
                    break;
                case "has_flimsykey":
                    HeldDescriptionText.text = "Holding a toy key! It's a bit too malleable to open any doors though...";
                    break;
                case "has_default":
                    HeldDescriptionText.text = "Holding the default item! This does nothing! It's not a real item! Give it back!";
                    break;
                case tastykey:
                    HoveredDescriptionText.text = "Holding a key that I want to eat! It should be solid enough to open a door.";
                    break;
                case stinkykey:
                    HoveredDescriptionText.text = "Holding a key made of poop! Why did you do this? You had other options! It should be solid enough to open a door though...";
                    break;
                default:
                    Debug.LogWarning("SetHold(name) was called with a string name that isn't one of the items: " + name);
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
                FindObjectOfType<DialogueManager>().SetVariableStateSystem("holding_item", false);
                FindObjectOfType<DialogueManager>().SetVariableStateSystem("held_item", "");

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
    public void TryCombine(string ItemTwo)
    {
        string ItemOne = SelectedItem;
        string Message = "Failed to combine: " + ItemOne + " and " + ItemTwo;
        if (SelectedItem == null || ItemTwo == null || ItemTwo.Equals(ItemOne))
        {
            Debug.LogError("trying to combine but selected item is null!!!");
            FailCombine(ItemOne, ItemTwo, Message);
        }
        else
        {
            bool SuccessfullyCombined = false;
            switch (ItemTwo) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
            {
                case dogfood:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case defaultitem:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case flimsykey:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:
                            Message = "combined moldable clay and flimsy key!!!";
                            SuccessfullyCombined = true;
                            Combine(keymold, ItemOne, ItemTwo, Message);
                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case clay:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case poop:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:
                            Message = "combined keymold and poop!!!";
                            SuccessfullyCombined = true;
                            Combine(stinkyfilledkeymold, ItemOne, ItemTwo, Message);
                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case candy:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:
                            Message = "combined keymold and candy!!!";
                            SuccessfullyCombined = true;
                            Combine(tastykeymold, ItemOne, ItemTwo, Message);
                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case moldableclay:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:
                            Message = "combined moldable clay and flimsy key!!!";
                            SuccessfullyCombined = true;
                            Combine(keymold, ItemOne, ItemTwo, Message);
                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case tastykeymold:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case stinkyfilledkeymold:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case stinkykey:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case tastykey:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:

                            break;
                        case candy:

                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                case keymold:
                    switch (ItemOne) //fail: FailCombine(string ItemOne, string ItemTwo) suceed: Combine("new item", ItemOne, ItemTwo);
                    {
                        case dogfood:

                            break;
                        case defaultitem:

                            break;
                        case flimsykey:

                            break;
                        case clay:

                            break;
                        case poop:
                            Message = "combined keymold and poop!!!";
                            SuccessfullyCombined = true;
                            Combine(stinkyfilledkeymold, ItemOne, ItemTwo, Message);
                            break;
                        case candy:
                            Message = "combined keymold and candy!!!";
                            SuccessfullyCombined = true;
                            Combine(tastykeymold, ItemOne, ItemTwo, Message);
                            break;
                        case moldableclay:

                            break;
                        case tastykeymold:

                            break;
                        case stinkyfilledkeymold:

                            break;
                        case stinkykey:

                            break;
                        case tastykey:

                            break;
                        case keymold:

                            break;
                        default:
                            Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                            break;
                    }
                    break;
                default:
                    Debug.LogWarning("failed to combine because one item wasn't a listed item: " + ItemOne + " and " + ItemTwo);
                    break;
            }
            if (!SuccessfullyCombined)
            {
                FailCombine(ItemOne, ItemTwo, Message);
            }
        }
    }
    private void Combine(string NewItem, string ItemOne, string ItemTwo, string Message)
    {
        FindObjectOfType<DialogueManager>().SetVariableStateSystem(NewItem, true); //set the two combined items to false (dialogue manager will call SetState())
        FindObjectOfType<DialogueManager>().SetVariableStateSystem(ItemOne, false);
        FindObjectOfType<DialogueManager>().SetVariableStateSystem(ItemTwo, false);
        Debug.Log("just combined items: " + ItemOne + " and " + ItemTwo + " into item: " + NewItem);
        ClearSelected(SelectedItem);

        RefreshItems();
        //UIScript.DisableUI(); if we want to instead disable UI and display the combined message
        //TODO: use Message to display the successfully combined thing!
    }
    private void FailCombine(string ItemOne, string ItemTwo, string Message)
    {
        //TODO: use Message to display the failed combined thing!
        //TODO: put up some UI text message that that combine attetmpt failed, have custom text for certian combinations (or all of them)
        Debug.Log("just failed to combined items: " + ItemOne + " and " + ItemTwo);
        ClearSelected(SelectedItem);
        RefreshItems();
    }

    private void RefreshItems()
    {
        UpdateItemStateList();
        foreach (Item i in ListOfItems)
        {
            i.Refresh();
        }
    }
}
