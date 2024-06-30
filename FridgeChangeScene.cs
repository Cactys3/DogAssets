using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeChangeScene : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<ChangeToScene>().ChangeScene();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<ChangeToScene>().ChangeScene();
    }
}
