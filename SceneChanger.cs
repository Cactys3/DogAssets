using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovementFree>().gameObject;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
