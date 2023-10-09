using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthBar;
    public Image healthDelayBar;
    public Image powerBar;

    private void Start() {
        healthDelayBar.fillAmount = 1;
    }

    private void Update() {
        if (healthDelayBar.fillAmount > healthBar.fillAmount)
            healthDelayBar.fillAmount -= Time.deltaTime;
    }

    internal void SetHealth(float persentage)
    {
        healthBar.fillAmount = persentage;
    }
}
