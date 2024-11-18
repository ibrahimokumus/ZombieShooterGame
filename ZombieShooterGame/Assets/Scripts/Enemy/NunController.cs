
using UnityEngine;
using UnityEngine.AI;

public class NunController : EnemyBaseClass
{


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDied) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (canSeePlayer)
        {
            if (distanceToPlayer < detectionDistance)
            {
                Chase();
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            Patrol();
        }


        animator.SetFloat("Speed", agent.speed);// ajanin hizana bagli olarak, animasyonlar gecis yapar
    }

   

    protected override void Attack()
    {
        if (Time.time > coolDownTimer)
        {
            animator.SetTrigger("AttackTrigger");
            coolDownTimer = Time.time + attackRate;
        }
    }

    public override void TakeDamage(float takenDamage)
    {
        health -= takenDamage;
        if (health <= 0f)
        {
           // KeyUpComing();
            Die();
        }
        else
        {
            animator.SetTrigger("TakenDamageTrigger");
        }
    }
    protected override void Patrol()
    {
        if (agent != null)
        {
            agent.speed = walkSpeed;
            if (Vector3.Distance(patrolWayPoints[waypointIndex].position, transform.position) < agent.stoppingDistance)
            {
                waypointIndex = (waypointIndex + 1) % patrolWayPoints.Length;

            }
            agent.SetDestination(patrolWayPoints[waypointIndex].position);
        }
    }

    protected override void Chase()
    {
        if (agent != null)
        {
            agent.speed = runSpeed;
            agent.SetDestination(player.transform.position);

            if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
            {
                agent.speed = 0f;
                Attack();
            }

        }
    }

    protected override void Die()
    {
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        
        isDied = true;
        animator.SetTrigger("IsDiedTrigger");
        KeyController keyController = FindObjectOfType<KeyController>();
        keyController.MakeVisibleKey(keyController.taskOrderIndex);
    }

    

}
