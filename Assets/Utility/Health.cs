using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable {

    private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    void Start() {
        currentHealth = maxHealth;
    }

    public float healthAsPercentage {
        get {
            return currentHealth / (float)maxHealth;
        }
    }

    public virtual void TakeDamage(int damageAmount) {
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
        if (currentHealth == 0) Destroy(gameObject);
    }
}
