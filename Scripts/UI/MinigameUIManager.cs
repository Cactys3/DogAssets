using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUIManager : MonoBehaviour
{
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
        if (inTab || inEsc)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                TabPanel.SetActive(false);
                EscPanel.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TabPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabPanel.SetActive(true);
            Time.timeScale = 0;
        }

    }
    public void DisableUI()
    {
        TabPanel.SetActive(false);
        EscPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public bool DisplayingUI()
    {
        return (inEsc || inTab);
    }
}

