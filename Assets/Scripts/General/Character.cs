using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Basic Attributes")]
    public float maxHealth;
    public float currentHealth;

    [Header("Invulnerability")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool isInvulnerable;


    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (isInvulnerable) {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0) {
                isInvulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker) {
        // Debug.Log(attacker.damage);
        if (isInvulnerable) return;
        currentHealth -= attacker.damage;
        TriggerInvulnerable();
    }

    private void TriggerInvulnerable() {
        if (!isInvulnerable) {
            isInvulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
    
}
