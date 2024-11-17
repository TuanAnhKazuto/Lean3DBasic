using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject npcChatPanel;
    public TextMeshProUGUI chatText;
    public GameObject fKey;
    [HideInInspector] public bool isChating;


    private void Awake()
    {
        fKey.SetActive(false);
        npcChatPanel.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fKey.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            isChating = true;
            fKey.SetActive(false);
            npcChatPanel.SetActive(true);
            chatText.text = "Nhin cai lon.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            fKey.SetActive(false);
            npcChatPanel.SetActive(false);
            isChating = false;
        }
    }
}
