using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    public CharacterEventSO healthEvent;

    private void OnEnable() {
        healthEvent.OnEventRaised += onHealthEvent;
    }

    private void OnDisable() {
        healthEvent.OnEventRaised -= onHealthEvent;
    }

    private void onHealthEvent(Character character) {
        float persentage = character.currentHealth / character.maxHealth;
        playerStatBar.SetHealth(persentage);
    }
}
