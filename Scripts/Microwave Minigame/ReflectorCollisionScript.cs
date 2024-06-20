using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorCollisionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            this.GetComponentInParent<ReflectorScript>().OnSideCollision(collision, this.tag);
        }
    }
}
