using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammo = 48;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            AmmoInventory inventory = other.gameObject.GetComponent<AmmoInventory>();
            if(inventory != null)
            {
                if (inventory.addAmmo(ammo))
                    Destroy(this.gameObject);
            }
        }
    }
}
