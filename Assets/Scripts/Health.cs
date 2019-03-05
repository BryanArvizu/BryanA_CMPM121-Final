using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = true;

    public void InflictDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth == 0 && destroyOnDeath)
        {
            Destroy(this.gameObject);
        }
    }
    
    public void Heal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
