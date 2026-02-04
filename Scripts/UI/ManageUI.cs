using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManageUI : MonoBehaviour
{
    [SerializeField] bool CanAccessInventory;
    [SerializeField] GameObject TabPanel;
    [SerializeField] GameObject EscPanel;
    public bool inTab;
    public bool inEsc;
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
                if (inTab && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)))
                {
                    FindObjectOfType<AudioManager>().PlayMultipleSFX("inventory_close");
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //Time.timeScale = 1;
                    TabPanel.SetActive(true);
                    EscPanel.SetActive(true);
                    TabPanel.SetActive(false);//twice
                    EscPanel.SetActive(false);//twice

                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    TabPanel.SetActive(true);//twice
                    TabPanel.SetActive(false);
                    //Time.timeScale = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscPanel.SetActive(false);//twice
                EscPanel.SetActive(true);
                EscPanel.SetActive(false);//twice
                EscPanel.SetActive(true);
                //Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && CanAccessInventory)
            {
                TabPanel.SetActive(false);
                TabPanel.SetActive(true);
                TabPanel.SetActive(false);//flip flop so it resets and everything is set up??
                TabPanel.SetActive(true);
                TabPanel.SetActive(false);//Just do it again
                TabPanel.SetActive(true);
                StartCoroutine(EnableTab());
                FindObjectOfType<AudioManager>().PlayMultipleSFX("inventory_open");
                //Time.timeScale = 0;
            }
        }
    }
    IEnumerator EnableTab()
    {
        TabPanel.SetActive(false);
        TabPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        TabPanel.SetActive(false);
        TabPanel.SetActive(true);
    }
    public void DisableUI()
    {
        TabPanel.SetActive(false);
        EscPanel.SetActive(false);
        //Time.timeScale = 1;
    }
    public bool DisplayingUI()
    {
        return (inEsc || inTab);
    }
}
