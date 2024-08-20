using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScript : MonoBehaviour
{
    [SerializeField] private Sprite SkipLevel;
    [SerializeField] private Sprite AreYouSure;
    [SerializeField] private int time;
    [SerializeField] private ChangeToScene changer;
    private bool ButtonClicked;
    private bool Hovered;
    private void Start()
    {
        ButtonClicked = false;
        Hovered = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine("EnableSprite");
        if (time == 0)
        {
            time = 15;
        }
    }
    private IEnumerator EnableSprite()
    {
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = SkipLevel;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Hovered)
        {
            if (ButtonClicked)
            {
                changer.ChangeScene();
                Debug.Log("second time");
            }
            else
            {
                ButtonClicked = true;
                GetComponent<SpriteRenderer>().sprite = AreYouSure;
                Debug.Log("first time");
            }
        }
    }
    public void OnMouseEnter()
    {
        Hovered = true;
    }
    public void OnMouseExit()
    {
        Hovered = false;
    }
}
