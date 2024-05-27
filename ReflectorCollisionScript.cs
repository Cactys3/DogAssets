using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorCollisionScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponentInParent<ReflectorScript>().OnSideCollision(collision, this.tag);
    }
}
