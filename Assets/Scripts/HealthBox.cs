using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] private int heal = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            print("player");
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                print("notnull");

                if (health.Heal(heal))
                    Destroy(this.gameObject);
            }
        }
    }
}
