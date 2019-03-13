using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    [SerializeField] private bool infiniteAmmo = false;

    private int ammo = 128;
    private int maxAmmo = 256;

    private void Start()
    {
        if (gameObject.tag == "Player")
            EventManager.TriggerEvent("AmmoMax", ammo);
    }

    public int requestAmmo(int request)
    {
        int x = 0; // Value to be returned

        if (infiniteAmmo)
            return request;

        if (ammo > request) // If more ammo than requested, return full amount
        {
            x = request;
            ammo -= request;
        }
        else // If less ammo than requested, return whatever is left
        {
            x = ammo;
            ammo = 0;
        }

        if (gameObject.tag == "Player")
            EventManager.TriggerEvent("AmmoMax", ammo);
        return x;
    }

    public bool addAmmo(int x)
    {
        if (ammo < maxAmmo)
        {
            ammo += x;
            if (ammo > maxAmmo)
                ammo = maxAmmo;
            if (gameObject.tag == "Player")
                EventManager.TriggerEvent("AmmoMax", ammo);
            return true;
        }
        else
            return false;
    }

    public int getAmmo()
    {
        return ammo;
    }

    public int getMaxAmmo()
    {
        return maxAmmo;
    }
}
