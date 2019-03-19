using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    enum State : byte { Idle, Aggro, Attacking };

    [SerializeField] float visionAngle = 90f;
    [SerializeField] Vector3 rayOffset = Vector3.zero;

    [SerializeField] float forceAggroRange = 10f;

    [SerializeField] float attackDmg = 10f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float attackCooldown = 1f;

    static private GameObject player;
    private NavMeshAgent agent;
    private Animator anim;

    private byte state;
    private float timer = 0f;

    void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = (byte)State.Idle;
    }

    void Update()
    {
        if(agent.velocity != Vector3.zero)
        {
            anim.Play("Walk");
        }

        if (state == (byte)State.Idle)
        {
            IdleUpdate();
        }

        else if (state == (byte)State.Aggro)
        {
            AggroUpdate();
        }

        else if (state == (byte)State.Attacking)
        {
            Attack();
        }
    }

    float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    void IdleUpdate()
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

    void AggroUpdate()
    {
        if (GetDistanceFromPlayer() < attackRange)
        {
            agent.ResetPath();
            timer = attackCooldown;
            anim.Play("attack");
            player.GetComponent<Health>().InflictDamage(attackDmg);
            state = (byte)State.Attacking;
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void Attack()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0f;
            state = (byte)State.Aggro;
        }
    }
}
