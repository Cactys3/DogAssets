using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //is the string of the item that the class holds
    [SerializeField] private string itemName;
    private EventSystem eventsystem;
    private ManageInventory InventoryScript;
    private DialogueManager dialoguemanager;
    [SerializeField] private GameObject HoldButton;
    [SerializeField] private GameObject UnholdButton;
    [SerializeField] private GameObject SelectButton;
    [SerializeField] private GameObject CombineButton;
    [SerializeField] private GameObject UnselectButton;
    [SerializeField] private Sprite Silhouette;
    [SerializeField] private Sprite FullColor;
    [SerializeField] private Sprite CheckMark;
    private int ItemState; //From -1 to 4, -1: Item was combined and is complete, 0: item is not obtained, 1: item is obtained, 2: item is hovered: 3: something is selected

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /**
     * 0 = not obtained, 1 = obtained, -1 = combined and completed
     */
    public void SetState(bool state)
    {
        Debug.Log("SetState(" + state + ") for " + itemName);
        switch (state)
        {
            case false:
                ItemState = 0;
                this.gameObject.GetComponent<Image>().sprite = Silhouette;
                break;
            case true:
                ItemState = 1;
                this.gameObject.GetComponent<Image>().sprite = FullColor;
                break;
            default:
                ItemState = -1;
                this.gameObject.GetComponent<Image>().sprite = CheckMark; //this is the case that it's 
                break;
        }
    }
    /**
     * used OnEnable for item classes to check if their obtained/not obtained state changed while their GameObject was disabled
     * 
     */
    public bool CheckState()
    {
        if (dialoguemanager != null)
        {
            Debug.LogWarning("dialogue mangaer is not set to anything, maybe bug idk");
        }
        return (bool)dialoguemanager.GetVariableStateSystem(itemName);
     
    }
    public void SetState(int state)
    {
        Debug.Log("SetState(" + state + ") for " + itemName);
        switch (state)
        {
            case 0:
                ItemState = 0;
                this.gameObject.GetComponent<Image>().sprite = Silhouette;
                break;
            case 1:
                ItemState = 1;
                this.gameObject.GetComponent<Image>().sprite = FullColor;
                break;
            case -1:
                ItemState = -1;
                this.gameObject.GetComponent<Image>().sprite = CheckMark; //this is the case that it's 
                break;
        }

    }
    private void OnEnable()
    {
        if (CheckState() == false)
        {
            SetState(false);
        }
        if (ItemState == 1)
        {
            SelectButton.SetActive(false);
            HoldButton.SetActive(false);
            CombineButton.SetActive(false);
            UnholdButton.SetActive(false);
            UnselectButton.SetActive(false);
        }
        if (ItemState == 2)
        {
            ItemState = 1;
            SelectButton.SetActive(false);
            HoldButton.SetActive(false);
            CombineButton.SetActive(false);
            UnholdButton.SetActive(false);
            UnselectButton.SetActive(false);
        }
        if (ItemState == 3)
        {
            SomethingSelected(true);
        }
        if (ItemState == -1)
        {
            //should be setup already
        }
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
        OnHover();
    }
    private void OnHover()
    {
        Debug.Log(ItemState);
        if (ItemState == 0) //if we are sillohte we dont change anything
        {
            return;
        }
        if (ItemState == 3) //if something else is selected maybe we do an effect or something, but we dont show any new buttons
        {
            return;
        }

        InventoryScript.SetHovered(itemName);
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

}
