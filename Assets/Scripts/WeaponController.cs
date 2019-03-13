using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerController))]

public class WeaponController : MonoBehaviour
{
    public event Action<float> deathEvent;

    public GameObject particles;
    private AudioSource weaponAudio;
    public Weapon weapon;

    private PlayerController player;
    private AmmoInventory ammoBag;
    private Animator anim;

    private bool isDead = false;

    void Start()
    {
        deathEvent = Death;

        player = GetComponent<PlayerController>();
        ammoBag = GetComponent<AmmoInventory>();

        if(weapon != null)
            anim = weapon.GetComponent<Animator>();

        if (gameObject.tag == "Player" && weapon != null)
            EventManager.TriggerEvent("MagCount", weapon.mag);
        else if (gameObject.tag == "Player" && weapon == null)
            EventManager.TriggerEvent("MagCount", 0);

        weaponAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead)
        {
            if (weapon != null && weapon.mag > 0)
            {
                if (Input.GetButton("Fire") && weapon.mode == (byte)Weapon.FireMode.Auto)
                {
                    Fire();
                }
                else if (Input.GetButtonDown("Fire") && weapon.mode == (byte)Weapon.FireMode.Semi)
                {
                    Fire();
                }
            }
            else if (weapon != null)
            {

                if (Input.GetButtonDown("Fire"))
                {
                    weaponAudio.clip = weapon.sound_empty;
                    weaponAudio.Play();
                }
            }

            if (Input.GetButtonDown("Reload"))
            {
                Reload();
                anim.Play("Reload");
            }
        }
    }

    private void Fire()
    {
        Camera cam = player.GetPlayerCamera();
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        weapon.mag--;
        weaponAudio.clip = weapon.sound_fire;
        weaponAudio.Play();

        if (Physics.Raycast(ray, out hitInfo, weapon.range))
        {
            if (hitInfo.collider != null)
            {
                Instantiate(particles, hitInfo.point, Quaternion.Euler(hitInfo.point - hitInfo.collider.transform.position));
                print("hit!");
                Health health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.InflictDamage(weapon.damage);
                }
            }
        }

        print(weapon.mag);
        if (gameObject.tag == "Player")
            EventManager.TriggerEvent("MagCount", weapon.mag);
    }

    private void Reload()
    {
        if (weapon != null && weapon.mag < weapon.magSize && ammoBag.getAmmo() > 0)
        {
            weaponAudio.clip = weapon.sound_reload;
            weaponAudio.Play();

            weapon.mag += ammoBag.requestAmmo(weapon.magSize - weapon.mag);

            if (gameObject.tag == "Player")
                EventManager.TriggerEvent("MagCount", weapon.mag);
        }
    }

    
    private void OnEnable()
    {
        //EventManager.StartListening("Health", deathEvent);
    }

    private void OnDisable()
    {
        //EventManager.StopListening("Health", deathEvent);
    }

    private void Death(float hp)
    {
        if (hp <= 0)
        {
            isDead = true;
            player.GetComponent<PlayerController>().GetUICamera().enabled = false;
        }
    }
}
