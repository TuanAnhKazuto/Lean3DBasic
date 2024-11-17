using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIMangager : MonoBehaviour
{
    public GameObject fKey;

    private void Awake()
    {
        fKey.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            fKey.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            fKey.SetActive(false);
        }
    }
}
