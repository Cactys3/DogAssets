using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManageUI : MonoBehaviour
{
    [SerializeField] bool CanAccessInventory;
    [SerializeField] GameObject TabPanel;
    [SerializeField] GameObject EscPanel;
    private bool inTab;
    private bool inEsc;
    private void Start()
    {
        TabPanel.SetActive(false);
        EscPanel.SetActive(false);
    }
    void Update()
    {
        inEsc = EscPanel.activeSelf;
        inTab = TabPanel.activeSelf;
        if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
        {
            if (inTab || inEsc)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TabPanel.SetActive(false);
                    EscPanel.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    TabPanel.SetActive(false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && CanAccessInventory)
            {
                TabPanel.SetActive(true);
            }
        }
    }
    public void DisableUI()
    {
        TabPanel.SetActive(false);
        EscPanel.SetActive(false);
    }
    public bool DisplayingUI()
    {
        return (inEsc || inTab);
    }
}
