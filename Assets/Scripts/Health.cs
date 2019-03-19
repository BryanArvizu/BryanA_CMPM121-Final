using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = true;

    public bool invulnerable = false;

    private void Start()
    {
        if(gameObject.tag == "Player")
            EventManager.TriggerEvent("Health", currentHealth);
    }

    public void InflictDamage(float dmg)
    {
        if(!invulnerable)
        {
            currentHealth -= dmg;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            if (gameObject.tag == "Player")
                EventManager.TriggerEvent("Health", currentHealth);

            if (currentHealth == 0 && destroyOnDeath)
            {
                Destroy(this.gameObject);
            }
            else if (currentHealth == 0)
            {
                invulnerable = true;
            }
        }
    }
    
    public bool Heal(float heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            if (gameObject.tag == "Player")
                EventManager.TriggerEvent("Health", currentHealth);
            return true;
        }
        return false;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
