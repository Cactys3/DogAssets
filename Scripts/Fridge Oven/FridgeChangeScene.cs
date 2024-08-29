using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeChangeScene : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ChangeToScene>().ChangeScene();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<ChangeToScene>().ChangeScene();
    }

    private IEnumerator ChangeScene()
    {
        float time = 0.5f;
        try
        {
            FindObjectOfType<AudioManager>().PlaySFX(FridgeOvenPlayerMovement.GoalSound);
            time = FindObjectOfType<AudioManager>().GetLengthSFX(FridgeOvenPlayerMovement.GoalSound);
        }
        catch
        {

        }
        yield return new WaitForSecondsRealtime(time);
        FindObjectOfType<ChangeToScene>().ChangeScene();
    }
}
