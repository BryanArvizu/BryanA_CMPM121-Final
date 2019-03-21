using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private bool[] keyInventory;

    private void Start()
    {
        keyInventory = new bool[3];
    }

    public bool hasKey(int index)
    {
        return keyInventory[index];
    }

    public bool addKey(int index)
    {
        if (!keyInventory[index])
        {
            keyInventory[index] = true;
            return true;
        }
        else
            return false;
    }

    public bool removeKey(int index)
    {
        if (keyInventory[index])
        {
            keyInventory[index] = false;
            return true;
        }
        else
            return false;
    }
}
