using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyCursor : MonoBehaviour
{
    [SerializeField] private Sprite DefaultSprite;
    [SerializeField] private Sprite JuicerSprite;
    [SerializeField] private Sprite FridgeOvenSprite;
    [SerializeField] private Sprite MicrowaveSprite;
    //[SerializeField] public SpriteRenderer sprite;
    [SerializeField] private GameObject collideObject;
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private Canvas CursorCanvas;
    [SerializeField] private RectTransform ImageRect;
    private string CurrentScene;
    private bool ShowCustomCursor;
    public static MyCursor instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        Cursor.visible = false;
        CurrentScene = "not a scene name!!!";
        image.enabled = false;
        image.sprite = DefaultSprite;
    }
    void Update()
    {
        if (!CurrentScene.Equals(SceneManager.GetActiveScene().name))
        {
            OnSceneLoad();
        }
        if (ShowCustomCursor != image.enabled)
        {
            image.enabled = ShowCustomCursor;
        }
        if (ShowCustomCursor)
        {
            Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

            Vector2 mousePositionCanvas;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                CursorCanvas.transform as RectTransform,
                Input.mousePosition + new Vector3(15, -15, 0),
                CursorCanvas.worldCamera,
                out mousePositionCanvas
            );

            collideObject.transform.position = mousePositionWorld;

            ImageRect.localPosition = mousePositionCanvas;

           // Debug.Log(mousePositionCanvas + " world: " + mousePositionWorld);
        }
    }
    public void ShowCursor()
    {
        image.enabled = true;
        ShowCustomCursor = true;
    }
    public void HideCursor()
    {
        image.enabled = false;
        ShowCustomCursor = false;
    }
    private void OnSceneLoad()
    {
        CurrentScene = SceneManager.GetActiveScene().name; 
        if (CurrentScene.Equals(DialogueManager.fridgeovenScene) || CurrentScene.Equals(DialogueManager.fridgeovenScene2) || CurrentScene.Equals(DialogueManager.fridgeovenScene3) || CurrentScene.Equals(DialogueManager.fridgeovenScene4) || CurrentScene.Equals(DialogueManager.fridgeovenScene5))
        {
            ShowCursor();//potentially have custom cursor for minigames
            image.sprite = FridgeOvenSprite;
        }
        if (CurrentScene.Equals(DialogueManager.microwaveScene) || CurrentScene.Equals(DialogueManager.microwaveScene2) || CurrentScene.Equals(DialogueManager.microwaveScene3) || CurrentScene.Equals(DialogueManager.microwaveScene4) || CurrentScene.Equals(DialogueManager.microwaveScene5))
        {
            ShowCursor();//potentially have custom cursor for minigames
            image.sprite = MicrowaveSprite;
        }
        switch (CurrentScene) 
        {
            case DialogueManager.dognipScene:
                HideCursor();
                break;
            case DialogueManager.ending1Scene:
                HideCursor();
                break;
            case DialogueManager.ending2Scene:
                HideCursor();
                break;
            case DialogueManager.introScene:
                HideCursor();
                break;
            case DialogueManager.juicerScene:
                ShowCursor();
                image.sprite = JuicerSprite;
                break;
            default:
                image.sprite = DefaultSprite;
                ShowCursor(); //potentially have custom cursor for rooms
                break;
        }
    }
}
