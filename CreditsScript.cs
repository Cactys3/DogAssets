using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private bool EndGame = false;
    [SerializeField] private float LowestPoint = -5.376f;
    private float velocity = 0.6f;
    private bool pause = true;
    private void Start()
    {
        StartCoroutine("Wait");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        pause = false;
    }
    IEnumerator End()
    {
        Application.OpenURL("https://forms.gle/9u2wfGr32NBC2Bq98");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pause = !pause;
        }
        if (transform.position.y > LowestPoint)
        {
            if (!pause)
            {
                transform.position = new Vector2(0, transform.position.y - (velocity * Time.deltaTime));
            }
        }
        else if (EndGame)
        {
            StartCoroutine("End");
            EndGame = false;
        }
    }
}
