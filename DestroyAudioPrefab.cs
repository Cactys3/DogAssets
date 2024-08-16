using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudioPrefab : MonoBehaviour
{
    private float startTime;
    private float clipLength = -1;
    private float time = 0;
    private void Awake()
    {
        startTime = Time.time;
    }
    void Update()
    {
        time = Time.time - startTime;
        if (clipLength != -1 && time > clipLength)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetClipLength(float t)
    {
        if (t >= 0)
        {
            clipLength = t;
        }
        else
        {
            clipLength = 2;
        }
    }
}
