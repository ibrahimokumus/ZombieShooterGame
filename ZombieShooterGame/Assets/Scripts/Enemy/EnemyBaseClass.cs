
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseClass : MonoBehaviour
{
    protected float damageAmount;
    protected float health;
    protected float movementSpeed;
    protected float detectionRange;
    protected float attackRange;
    protected Transform[] patrolWayPoints;
    protected int experiencePoint;

    protected NavMeshAgent agent;
    [SerializeField] protected float speed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundDistance = 0.3f;
    [SerializeField] protected LayerMask groundLayerMask;
    protected Vector3 _velocity;
    protected bool isGrounded;


    protected abstract void Move();
    protected abstract void Jump();
    protected abstract void TakeDamage(float takenDamage);
    protected abstract void Attack(float damage);
   
    protected abstract void Patrol();

    protected abstract void Die();
    protected abstract void Chase();

}
