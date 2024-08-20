using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDebug : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This: " + this.name + ", Trigger: " + other.name + other.tag + " trigger");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("This: " + this.name + ", Trigger: " + collision.name + collision.tag + " trigger");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("This: " + this.name + ", Trigger: " + collision.gameObject.name + collision.gameObject.tag + " trigger");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("This: " + this.name + ", Trigger: " + collision.gameObject.name + collision.gameObject.tag + " trigger");
    }
}
