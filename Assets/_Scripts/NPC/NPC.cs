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
    Coroutine coroutine;
    public Character character;

    public string[] chat;


    private void Awake()
    {
        fKey.SetActive(true);  

        fKey.SetActive(false);
        npcChatPanel.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (character.onDead) return;
            fKey.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F) && !isChating)
        {
            if (character.onDead) fKey.SetActive(false); return;
            isChating = true; // Đánh dấu đang trong trạng thái hội thoại
            fKey.SetActive(false);
            npcChatPanel.SetActive(true);
            coroutine = StartCoroutine(ReadChat());
        }
    }


    IEnumerator ReadChat()
    {
        foreach(var line in chat)
        {
            chatText.text = "";
            for (int i = 0; i < line.Length; i++)
            {
                chatText.text += line[i];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            fKey.SetActive(false);
            npcChatPanel.SetActive(false);
            isChating = false;
            // Kiểm tra nếu coroutine không null trước khi dừng nó
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null; // Đặt lại giá trị để tránh lỗi lần sau
            }
        }
    }
}
