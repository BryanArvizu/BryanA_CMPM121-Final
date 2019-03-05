using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    [SerializeField] private bool infiniteAmmo = false;

    private int ammo = 128;
    private int maxAmmo = 256;

    public int requestAmmo(int request)
    {
        if (ammo > request || infiniteAmmo)
        {
            ammo -= request;
            return request;
        }
        else if (request > ammo)
        {
            int x = ammo;
            ammo = 0;
            return x;
        }
        else
            return 0;
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
