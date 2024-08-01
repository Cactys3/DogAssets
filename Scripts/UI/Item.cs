using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool GameHasLoaded;
    //is the string of the item that the class holds
    [SerializeField] private GameObject ItemPanel;
    [SerializeField] private string itemName;
    private EventSystem eventsystem;
    private ManageInventory InventoryScript;
    private DialogueManager dialoguemanager;
    [SerializeField] private GameObject HoldButton;
    [SerializeField] private GameObject UnholdButton;
    [SerializeField] private GameObject SelectButton;
    [SerializeField] private GameObject CombineButton;
    [SerializeField] private GameObject UnselectButton;
    [SerializeField] private GameObject ItemFrame;
    [SerializeField] int MaxItemsPerRow;
    [SerializeField] int MaxItemsTotal;
    [SerializeField] int OffsetY;
    [SerializeField] int OffsetX;
    private int ItemState; //From -1 to 4, -1: Item was combined and is complete, 0: item is not obtained, 1: item is obtained, 2: item is hovered: 3: something is selected
    int ItemSlot;
    private bool MouseOnThis;
    // Start is called before the first frame update
    void Start()
    {
        MaxItemsPerRow = 2;
        MaxItemsTotal = 6;
        OffsetY = -150;
        OffsetX = 500;
        ItemSlot = 38;
        eventsystem = FindObjectOfType<EventSystem>();
        InventoryScript = FindObjectOfType<ManageInventory>();
        dialoguemanager = FindObjectOfType<DialogueManager>();
        if (itemName.IsUnityNull() || itemName ==  null)
        {
            Debug.LogError("itemName for this item class is null bro, why is it null? set it in the inspector");
        }
        SelectButton.SetActive(false);
        HoldButton.SetActive(false);
        CombineButton.SetActive(false);
        UnholdButton.SetActive(false);
        UnselectButton.SetActive(false);
        ItemFrame.SetActive(false);
    }
    /**
     * used OnEnable for item classes to check if their obtained/not obtained state changed while their GameObject was disabled
     * 
     */
    private void OnEnable()
    {
        MouseOnThis = false;
        if (!GameHasLoaded)
        {
            GameHasLoaded = true; //well i dont want this script to try to run when the level is loaded for the first time, only when the gameobject is activated after being deactivated.
            return;
        }

        if (CheckSlot() != ItemSlot) 
        {
            Debug.Log("changing itemslot from: " + ItemSlot + " to: " + CheckSlot() + " on item: " + itemName);
            SetItemSlot(CheckSlot()); //if the item slot has changed, enable/disable/move this item based on the new number;
        }

        if (ItemState == 1)
        {
            SelectButton.SetActive(false);
            HoldButton.SetActive(false);
            CombineButton.SetActive(false);
            UnholdButton.SetActive(false);
            UnselectButton.SetActive(false);
            ItemFrame.SetActive(false);
        }
        if (ItemState == 2)
        {
            ItemState = 1;
            SelectButton.SetActive(false);
            HoldButton.SetActive(false);
            CombineButton.SetActive(false);
            UnholdButton.SetActive(false);
            UnselectButton.SetActive(false);
            ItemFrame.SetActive(false);
        }
        if (ItemState == 3)
        {
            SomethingSelected(true);
        }
    }
    public int CheckSlot()
    {
        return FindObjectOfType<ManageInventory>().GetItemSlot(itemName);
    }
    public void SetItemSlot(int number)
    {
        ItemSlot = number;
        if (ItemSlot == -1)
        {
            ItemState = 0;
            ItemPanel.SetActive(false);
        }
        else
        {
            if (ItemState == -1 || ItemState == 0)
            {
                ItemState = 1;
            }
            ItemPanel.SetActive(true);
            if (ItemSlot >= MaxItemsTotal)
            {
                Debug.LogError("Trying to setup an item but itemslot is: " + ItemSlot + ", which is greater than the max:  9");
            }
            else
            {
                int Collumn = (ItemSlot % MaxItemsPerRow);
                int Row = (ItemSlot / MaxItemsPerRow);
                int y = OffsetY * Row;
                int x = OffsetX * Collumn;
                transform.localPosition = new Vector3(x, y, 0);
                Debug.Log("setting item: " + itemName + " to x: " + x + " y: " + y);
                //Debug.Log("Pos of " + itemName + ": " + transform.localPosition);
            }
        }
        Debug.Log("setting item: " + itemName + " to itemslot " + ItemSlot + " and itemstate: " + ItemState);
    }
    /**
     * True to set this item to ItemState 3, meaning it will display the combine button. This is used when an item is selected. 
     * False to set this item to ItemState 1 or 2, meaning it will act as just obtained or obtained and hovered. This is used when no items are selected.
     */
    public void SomethingSelected(bool value)
    {
        if (IsObtained())
        {
            if (value) // if something else is selected we only give option to combine
            {
                ItemState = 3;
                CombineButton.SetActive(true);
                UnholdButton.SetActive(false);
                SelectButton.SetActive(false);
                HoldButton.SetActive(false);
                UnselectButton.SetActive(false);

                if (this.IsThisSelected()) //if this is the selected item, we give option to unselect
                {
                    Debug.Log("this is selected so we set unselect to true: " + itemName);
                    UnselectButton.SetActive(true);
                    CombineButton.SetActive(false);
                }
                if (InventoryScript.SomethingHeld() && InventoryScript.GetHeldItem().Equals(itemName)) // if this is held, we give option to unhold it too
                {
                    UnholdButton.SetActive(false);
                }
            }
            else
            {
                ItemState = 1; //This should have previously been 3, now it is 1 because this is obtained and nothing else is selected
                if (InventoryScript.SomethingHovered() && InventoryScript.GetHoveredItem().Equals(itemName))
                {
                    OnHover(); //if this is being hovered, call the method to display the correct buttons
                }
            }
        }
    }
    //displays the select and hold buttons if nothing else is being hovered, if else is being hovered, displays the combine button
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOnThis = true;
        OnHover();
    }
    private void OnHover()
    {
        //Debug.Log(ItemState);
        if (ItemState == 0) //if we are sillohte we dont change anything
        {
            return;
        }
        if (ItemState == 3) //if something else is selected maybe we do an effect or something, but we dont show any new buttons
        {
            return;
        }

        InventoryScript.SetHovered(itemName);
        ItemFrame.SetActive(true);
        SelectButton.SetActive(true); //since nothing else is selected, we show select
        if (ItemState == 1)
        {
            ItemState = 2; //if we were only obtained, we are now hovered
        }
        if (InventoryScript.SomethingHeld() == true)
        {
            if (IsThisHeld())
            {
                UnholdButton.SetActive(true); //if this is the held item, show the unhold button
            }
        }
        else
        {
            HoldButton.SetActive(true); //if nothing was held before, we show the hold button
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOnThis = false;
        OnExitHover();
    }
    private void OnExitHover()
    {
        if (ItemState == 0) //if we are sillohte we dont change anything
        {
            return;
        }
        if (ItemState == 3) //if something else is selected maybe we do an effect or something, but we dont show any new buttons
        {
            return;
        }


        InventoryScript.ClearHovered(itemName);

        if (ItemState != 3 && IsObtained()) //if the mouse leaves while nothing else is selected, show no buttons and if it was only hovered, set it to obtained state
        {
            SelectButton.SetActive(false);
            HoldButton.SetActive(false);
            CombineButton.SetActive(false);
            UnholdButton.SetActive(false);
            UnselectButton.SetActive(false);
            ItemFrame.SetActive(false);
            if (ItemState == 2)
            {
                ItemState = 1;
            }
        }
    }
    //tells ManageInventory that an item is selected.
    public void OnSelectClicked()
    {
        if (!(ItemState == 3))
        {
            InventoryScript.SetSelected(itemName);
            //ItemState = 3;
            OnExitHover();
            OnHover();
        }
        else
        {
            Debug.LogWarning("Clicked Select button for an item but another item was already selected!");
        }
    }
    // tells ManageUI to close the inventory and sets the INK global itemHeld value to the itemName string
    public void OnHoldClicked()
    {
        InventoryScript.SetHold(itemName);

        OnExitHover();
        OnHover();
    }
    //tests if the item can be combined, if it cannot the unique text respective to trying to combine those two items displays and all items are unselected.If it succeeds, it tells manageInventory that those two items are combined.
    public void OnCombineClicked()
    {
        InventoryScript.TryCombine(itemName);

        OnExitHover();
        OnHover();
    }
    public void OnUnholdClicked()
    {
        InventoryScript.ClearHeldItem(itemName);

        OnExitHover();
        OnHover();
    }
    public void OnUnselectClicked()
    {
        if (IsThisSelected() && ItemState == 3)
        {
            InventoryScript.ClearSelected(itemName);

            OnExitHover();
            OnHover();
        }
        else
        {
            Debug.LogWarning("Clicked on Unselected() but this was not selected?");
        }
    }
    private bool IsThisHeld()
    {
        if (InventoryScript.SomethingHeld() == false)
        {
            return false;
        }
        return InventoryScript.GetHeldItem().Equals(itemName);
    }
    private bool IsThisSelected()
    {
        if (InventoryScript.SomethingSelected() == false)
        {
            return false;
        }
        return InventoryScript.GetSelectedItem().Equals(itemName);
    }

    private bool IsObtained()
    {
        return (ItemState != 0 && ItemState != -1);
    }
    public string GetName()
    {
        return itemName;
    }
    public int GetState()
    {
        return ItemState;
    }
    public void Refresh()
    {
        /**   if (CheckSlot() == -1) //if this item should be disabled
           {
               OnExitHover();

               return;
           } */

        if (CheckSlot() == -1) //if this has been disabled, disable it
        {
            OnExitHover();
            OnEnable();
            
           // ItemPanel.SetActive(false);
        }
        else
        {
            if (MouseOnThis) //if this should be hovered, update it and then hover it
            {
                OnExitHover();
                OnEnable();
                OnHover();
            }
            else // if this shouldn't be hoverd, update it then unhover it
            {
                OnExitHover();
                OnEnable();
            }
        }
    }
}
