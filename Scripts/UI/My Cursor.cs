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
    [SerializeField] public SpriteRenderer sprite;
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
        sprite.enabled = false;
        sprite.sprite = DefaultSprite;
    }
    void Update()
    {
        if (!CurrentScene.Equals(SceneManager.GetActiveScene().name))
        {
            OnSceneLoad();
        }
        if (ShowCustomCursor != sprite.enabled)
        {
            sprite.enabled = ShowCustomCursor;
        }
        if (ShowCustomCursor)
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        }
    }
    public void ShowCursor()
    {
        sprite.enabled = true;
        ShowCustomCursor = true;
    }
    public void HideCursor()
    {
        sprite.enabled = false;
        ShowCustomCursor = false;
    }
    private void OnSceneLoad()
    {
        CurrentScene = SceneManager.GetActiveScene().name; 
        if (CurrentScene.Equals(DialogueManager.fridgeovenScene) || CurrentScene.Equals(DialogueManager.fridgeovenScene2) || CurrentScene.Equals(DialogueManager.fridgeovenScene3) || CurrentScene.Equals(DialogueManager.fridgeovenScene4) || CurrentScene.Equals(DialogueManager.fridgeovenScene5))
        {
            ShowCursor();//potentially have custom cursor for minigames
            sprite.sprite = FridgeOvenSprite;
        }
        if (CurrentScene.Equals(DialogueManager.microwaveScene) || CurrentScene.Equals(DialogueManager.microwaveScene2) || CurrentScene.Equals(DialogueManager.microwaveScene3) || CurrentScene.Equals(DialogueManager.microwaveScene4) || CurrentScene.Equals(DialogueManager.microwaveScene5))
        {
            ShowCursor();//potentially have custom cursor for minigames
            sprite.sprite = MicrowaveSprite;
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
                sprite.sprite = JuicerSprite;
                break;
            default:
                sprite.sprite = DefaultSprite;
                ShowCursor(); //potentially have custom cursor for rooms
                break;
        }
    }
}
