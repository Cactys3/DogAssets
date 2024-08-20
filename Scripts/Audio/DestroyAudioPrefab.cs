using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudioPrefab : MonoBehaviour
{
    private float clipLength;
    public void SetClipLength(float t)
    {
        Debug.Log(clipLength + " length");
        if (t >= 0)
        {
            clipLength = t;
        }
        else
        {
            clipLength = 0.5f;
        }
        Invoke("ReturnToPool", clipLength);
    }
    
    public void SetReturnTime(float length)
    {
        Debug.Log(length + " length");
        clipLength = length;
        Invoke("ReturnToPool", clipLength);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().ReturnToPool(gameObject);
    }
}
