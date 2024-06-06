using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCursor : MonoBehaviour
{
    private void Start()
    {
        //FindObjectOfType<DialogueManager>().SetVariables();
        Cursor.visible = false;
    }
    void Update()
    {
        Debug.Log(Input.mousePosition);
        this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }
}
