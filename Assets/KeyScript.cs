using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private int keyID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (other.GetComponent<KeyInventory>().addKey(keyID))
                Destroy(gameObject);
        }
    }
}
