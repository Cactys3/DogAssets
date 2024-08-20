using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObtainedItemScript : MonoBehaviour
{
    public Sprite dogfoodImage;
    public Sprite defaultitemImage;
    public Sprite flimsykeyImage;
    public Sprite clayImage;
    public Sprite poopImage;
    public Sprite candyImage;
    public Sprite Image;
    public Sprite moldableclayImage;
    public Sprite keymoldImage;
    public Sprite tastykeymoldImage;
    public Sprite stinkyfilledkeymoldImage;
    public Sprite stinkykeyImage;
    public Sprite tastykeyImage;
    [SerializeField] private TextMeshProUGUI ItemText;
    private bool ChildrenEnabled;
    [SerializeField] private Image ItemImage;
    private void Start()
    {
        ItemText.text = "";

        SetChildState(false);
    }
    private void SetChildState(bool b)
    {
        ChildrenEnabled = b;
        int index = 0;
        while (index < transform.childCount)
        {
            transform.GetChild(index).gameObject.SetActive(b);
            index++;
        }
    }
    public void AddedItem(string name)
    {
        if (ChildrenEnabled)
        {
            StartCoroutine("WaitUntilLastItemDisplayed", name);
            return; //if it's already saying they got another item, wait until that's done and talk about this item - will only work if max two items are obtained at the same time i think
        }
        bool IsAnItem = true;
        SetChildState(true);
        switch (name)
        {
            case ManageInventory.dogfood:

                ItemText.text = "You Got Some Dogfood!";
                ItemImage.sprite = dogfoodImage;
                break;
            case ManageInventory.stinkyfilledkeymold:

                ItemText.text = "You Got a Stinky Filled Key Mold!";
                ItemImage.sprite = stinkyfilledkeymoldImage;
                break;
            case ManageInventory.tastykeymold:

                ItemText.text = "You Got a Tasty Key Mold!";
                ItemImage.sprite = tastykeymoldImage;
                break;
            case ManageInventory.keymold:

                ItemText.text = "You Got a Key Mold!";
                ItemImage.sprite = keymoldImage;
                break;
            case ManageInventory.moldableclay:

                ItemText.text = "You Got Some Moldable Clay!";
                ItemImage.sprite = moldableclayImage;
                break;
            case ManageInventory.candy:

                ItemText.text = "You Got Some Candy!";
                ItemImage.sprite = candyImage;
                break;
            case ManageInventory.poop:

                ItemText.text = "You Got Some Poop!";
                ItemImage.sprite = poopImage;
                break;
            case ManageInventory.clay:

                ItemText.text = "You Got Some Clay!";
                ItemImage.sprite = clayImage;
                break;
            case ManageInventory.flimsykey:

                ItemText.text = "You Got a Flimsy Key!";
                ItemImage.sprite = flimsykeyImage;
                break;
            case ManageInventory.defaultitem:

                ItemText.text = "You Got an Item!";
                ItemImage.sprite = defaultitemImage;
                break;
            case ManageInventory.tastykey:

                ItemText.text = "You Got a Tasty Key!";
                ItemImage.sprite = tastykeyImage;
                break;
            case ManageInventory.stinkykey:

                ItemText.text = "You Got a Stinky Key!";
                ItemImage.sprite = stinkykeyImage;
                break;
            default:
                SetChildState(false);
                Debug.LogWarning("called obtain item but name was invalid: " + name);
                IsAnItem = false;
                break;
        }
        if (IsAnItem)
        {
            FindObjectOfType<AudioManager>().PlaySFX("obtained_item");
            StartCoroutine("DisablePopup");
        }
    }
    private IEnumerator DisablePopup()
    {
        yield return new WaitForSeconds(4);

        SetChildState(false);
    }
    private IEnumerator WaitUntilLastItemDisplayed(string name)
    {

        yield return new WaitUntil(() => !ChildrenEnabled);
        AddedItem(name);
    }
}
