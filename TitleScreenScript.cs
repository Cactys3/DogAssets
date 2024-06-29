using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    [SerializeField] private Sprite ExitNotHovered;
    [SerializeField] private Sprite ExitHovered;
    [SerializeField] private Sprite StartNotHovered;
    [SerializeField] private Sprite StartHovered;
    [SerializeField] private SpriteRenderer ExitSprite;
    [SerializeField] private SpriteRenderer StartSprite;
    [SerializeField] private Sprite Title1;
    [SerializeField] private Sprite Title2;
    [SerializeField] private Sprite Title3;
    [SerializeField] private Sprite Title4;
    [SerializeField] private Sprite Title5;
    [SerializeField] private SpriteRenderer TitleSprite;

    private void Start()
    {
        StartSprite.sprite = StartHovered;
        ExitSprite.sprite = ExitNotHovered;
        TitleSprite.sprite = Title1;
        StartCoroutine("TitleAnim");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ExitSprite.sprite == ExitHovered)
            {
                ExitSprite.sprite = ExitNotHovered;
                StartSprite.sprite = StartHovered;
            }
            else
            {
                ExitSprite.sprite = ExitHovered;
                StartSprite.sprite = StartNotHovered;
            }
        }



        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ExitSprite.sprite == ExitHovered)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("Load Managers");
            }
        }
    }

    private IEnumerator TitleAnim()
    {
        if (TitleSprite.sprite == Title1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
        if (TitleSprite.sprite == Title1)
        {
            TitleSprite.sprite = Title2;
        }
        else if (TitleSprite.sprite == Title2)
        {
            TitleSprite.sprite = Title3;
        }
        else if (TitleSprite.sprite == Title3)
        {
            TitleSprite.sprite = Title4;
        }
        else if (TitleSprite.sprite == Title4)
        {
            TitleSprite.sprite = Title5;
        }
        else
        {
            TitleSprite.sprite = Title1;
        }
        StartCoroutine("TitleAnim");
    }
}
