using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;

    public void UpdateBar(int maxHP, int curHP)
    {
        fillBar.fillAmount = (float)curHP / (float)maxHP;
    }
}
