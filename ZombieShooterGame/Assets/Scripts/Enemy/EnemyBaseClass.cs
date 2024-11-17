
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseClass : MonoBehaviour
{
    
    [SerializeField] protected float health = 100f;
    protected float runSpeed = 2.5f;
    protected float walkSpeed = 1f;
    protected float detectionDistance = 10f;
    protected float attackRange = 1.6f;
    [SerializeField] protected Transform[] patrolWayPoints;
    protected int experiencePoint;
    protected int waypointIndex = 0;
    protected NavMeshAgent agent;
    [SerializeField] protected GameObject player;
    protected Animator animator;

    [SerializeField] protected Transform key;

    public bool canSeePlayer = false;
    protected float coolDownTimer;
    protected float attackRate= 1f;

    public bool isDied = false;
   
    public abstract void TakeDamage(float takenDamage);
    protected abstract void Attack();
    
    protected abstract void Patrol();

    protected abstract void Die();
    protected abstract void Chase();

   

}
