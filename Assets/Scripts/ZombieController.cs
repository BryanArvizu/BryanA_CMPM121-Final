using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    enum State : byte { Idle, Aggro, Attacking, Dying };

    [Header("Vision Settings")]
    [SerializeField] float visionAngle = 90f;
    [SerializeField] Vector3 rayOffset = Vector3.zero;
    [SerializeField] float forceAggroRange = 10f;

    [Space(10)]

    [Header("Attack Settings")]
    [SerializeField] float attackDmg = 10f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float attackCooldown = 1f;

    [Space(10)]

    [Header("Audio Clips")]
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip groan;

    static private GameObject player;
    private NavMeshAgent agent;
    private Animator anim;
    private Health hp;
    private AudioSource audio;

    private byte state;
    private float timer = 0f;

    void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hp = GetComponent<Health>();
        audio = GetComponent<AudioSource>();
        state = (byte)State.Idle;
    }

    void Update()
    {
        if (hp.getCurrentHealth() == 0 && state != (byte)State.Dying)
        {
            timer = 2f;
            agent.isStopped = true;
            anim.Play("fallingback");
            GetComponent<CapsuleCollider>().enabled = false;
            state = (byte)State.Dying;
        }
        else if(agent.velocity != Vector3.zero && state == (byte)State.Aggro || state == (byte)State.Idle)
        {
            anim.Play("walk_in_place");
        }
        else if(state == (byte)State.Aggro || state == (byte)State.Idle)
        {
            anim.Play("idle");
        }

        if (state == (byte)State.Idle)
        {
            IdleUpdate();
            randomGroan(0.1f);
        }

        else if (state == (byte)State.Aggro)
        {
            AggroUpdate();
            randomGroan(0.5f);
        }

        else if (state == (byte)State.Attacking)
        {
            Attack();
        }

        else if (state == (byte)State.Dying)
        {
            DyingUpdate();
        }
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    private void IdleUpdate()
    {
        float distance = GetDistanceFromPlayer();
        if (distance < 10f)
        {
            state = (byte)State.Aggro;
        }
        else if (distance < 25)
        {
            Vector3 playerDirection = player.transform.position - (transform.position + rayOffset);

            Ray ray = new Ray(transform.position + rayOffset, playerDirection);
            Debug.DrawRay(transform.position + rayOffset, playerDirection);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 50))
            {
                float angle = Vector3.Angle(transform.forward, playerDirection);

                if (hitInfo.collider.gameObject == player && angle <= visionAngle)
                {
                    state = (byte)State.Aggro;
                }
            }
        }
    }

    private void AggroUpdate()
    {
        if (GetDistanceFromPlayer() < attackRange)
        {
            agent.ResetPath();
            timer = attackCooldown;
            audio.clip = attack;
            audio.volume = 0.4f;
            audio.Play();
            anim.Play("attack");
            player.GetComponent<Health>().InflictDamage(attackDmg);
            state = (byte)State.Attacking;
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void Attack()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0f;
            state = (byte)State.Aggro;
        }
    }

    private void DyingUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0f;
            Destroy(gameObject);
        }
    }

    private void randomGroan(float odds)
    {
        float rand = Random.Range(0f, 100f);
        if(rand <= odds && !audio.isPlaying)
        {
            audio.clip = groan;
            audio.volume = 0.1f;
            audio.Play();
        }
    }
}
