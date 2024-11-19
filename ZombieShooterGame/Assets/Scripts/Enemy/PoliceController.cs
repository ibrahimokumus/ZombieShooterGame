using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PoliceController : EnemyBaseClass
{


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;
    }


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




    public override void TakeDamage(float takenDamage)
    {
        health -= takenDamage;
        if (health <= 0f)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("TakenDamageTrigger");
        }
    }
    protected override void Attack()
    {
        if (Time.time > coolDownTimer)
        {
            
            animator.SetTrigger("AttackTrigger");
            coolDownTimer = Time.time + attackRate;
        }
    }

    protected override void Patrol()
    {
        if (agent != null)
        {
            agent.speed = walkSpeed;
            if (Vector3.Distance(patrolWayPoints[waypointIndex].position, transform.position) <= agent.stoppingDistance)
            {
                waypointIndex = (waypointIndex + 1) % patrolWayPoints.Length;

            }
            agent.SetDestination(patrolWayPoints[waypointIndex].position);
        }
    }

    protected override void Die()
    {
        KeyController keyController = FindObjectOfType<KeyController>();
        DoorController doorController = FindObjectOfType<DoorController>();
        TaskController taskController = FindObjectOfType<TaskController>();
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;

        isDied = true;
        animator.SetTrigger("IsDiedTrigger");
        keyController.MakeVisibleKey(2);
        taskController.taskOrderIndex++;
        taskController.AssignTask();
        //doorController.doorOrderIndex++;
        StartCoroutine(doorController.RotateDoorSmooth(doorController.doorOrderIndex, -90));
        
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
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                Attack();
            }

        }
    }

}
