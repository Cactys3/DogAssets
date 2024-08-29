using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorialScript : MonoBehaviour
{
    private static bool Show = true;

    private void OnEnable()
    {
        if (!Show)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Show && Input.GetKeyDown(KeyCode.Tab))
        {
            Show = false;
            this.gameObject.SetActive(false);
        }
    }
}
