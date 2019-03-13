using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = true;

    private void Start()
    {
        if(gameObject.tag == "Player")
            EventManager.TriggerEvent("Health", currentHealth);
    }

    public void InflictDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
            currentHealth = 0;

        if (gameObject.tag == "Player")
            EventManager.TriggerEvent("Health", currentHealth);

        if (currentHealth == 0 && destroyOnDeath)
        {
            Destroy(this.gameObject);
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
}
