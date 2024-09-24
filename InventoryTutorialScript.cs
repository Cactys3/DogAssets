using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorialScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            this.gameObject.SetActive(false);
        }
    }
}
