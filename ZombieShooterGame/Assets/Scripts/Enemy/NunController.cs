
using UnityEngine;
using UnityEngine.AI;

public class NunController : EnemyBaseClass
{

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Move()
    {
        // Zemin kontrolü
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }



        /*

        // Hedef konumu belirle
        if (move.magnitude >= 0.1f)
        {
            Vector3 targetPosition = transform.position + move;
            agent.SetDestination(targetPosition);
        }

        // Zýplama ve yer çekimi
        _velocity.y += _gravity * Time.deltaTime;
        if (isGrounded)
        {
            agent.baseOffset = _velocity.y * Time.deltaTime;
        }

        */

    }

    protected override void Jump()
    {
        if (isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
        }
    }
    protected override void Attack(float damage)
    {
        throw new System.NotImplementedException();
    }

    protected override void TakeDamage(float takenDamage)
    {
        throw new System.NotImplementedException();
    }
    protected override void Patrol()
    {
        throw new System.NotImplementedException();
    }

    protected override void Chase()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }




    

   
}
