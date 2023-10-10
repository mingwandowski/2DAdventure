using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Basic Attributes")]
    public float maxHealth;
    public float currentHealth;

    [Header("Invulnerability")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool isInvulnerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDeath;

    private void Start() {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void Update() {
        if (isInvulnerable) {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0) {
                isInvulnerable = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Water")) {
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDeath?.Invoke();
        }
    }

    public void TakeDamage(Attack attacker) {
        // Debug.Log(attacker.damage);
        if (isInvulnerable) return;
        currentHealth -= attacker.damage;
        if (currentHealth <= 0) {
            currentHealth = 0;
            // Dead
            OnDeath?.Invoke();
        } else {
            TriggerInvulnerable();
            OnTakeDamage?.Invoke(attacker.transform);
        }

        OnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable() {
        if (!isInvulnerable) {
            isInvulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
    
}
