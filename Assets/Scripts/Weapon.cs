using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum FireMode : byte { Auto, Semi };

    [Header("Weapon Settings")]
    public float damage = 12f;
    public byte mode = (byte)FireMode.Semi;
    public float range = 100f;

    [Header("Magazine Settings")]
    public int mag = 24;
    public int magSize = 24;
    public float reloadTime = 2f;

    [Header("Sound Settings")]
    public AudioClip sound_fire;
    public AudioClip sound_empty;
    public AudioClip sound_reload;

    [Header("Animations")]
    public AnimationClip recoil;
    public AnimationClip reload;
    public AnimationClip sprint;
    public AnimationClip lower;

    bool Reload(float request)
    {
        return true;
    }
}
