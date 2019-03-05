using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class WeaponController : MonoBehaviour
{
    public GameObject particles;

    private enum FireMode : byte { Auto, Semi};

    [SerializeField] private byte mode = (byte)FireMode.Semi;

    private float damage = 1f;
    private float range = 100f;
    private int mag = 24;
    private int magSize = 24;

    private PlayerController player;
    private AmmoInventory ammoBag;

    void Start()
    {
        player = GetComponent<PlayerController>();
        ammoBag = GetComponent<AmmoInventory>();
    }

    void Update()
    {
        if(mag > 0)
        {
            if (Input.GetButton("Fire") && mode == (byte)FireMode.Auto)
                Fire();
            else if (Input.GetButtonDown("Fire") && mode == (byte)FireMode.Semi)
                Fire();
        }

        if (Input.GetButtonDown("Reload"))
            Reload();
    }

    private void Fire()
    {
        Camera cam = player.GetPlayerCamera();
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, range))
        {
            if (hitInfo.collider != null)
            {
                Instantiate(particles, hitInfo.point, Quaternion.Euler(hitInfo.point - hitInfo.collider.transform.position));
                print("hit!");
                Health health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.InflictDamage(damage);
                }
            }
        }
    }

    private void Reload()
    {
        if (mag < magSize && ammoBag.getAmmo() > 0)
        {
            mag += ammoBag.requestAmmo(magSize - mag);
        }
    }
}
