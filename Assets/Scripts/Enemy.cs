using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Transform enemy;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;

    //Attacking
    public float TimeBetweenAttacks;
    bool AlreadyAttacked;
    public GameObject projectileCO;

    //States
    public float sightRange, attackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    //Health
    public float health = 10f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        CapsuleCollider capCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            Patroling();
        }
        if (PlayerInSightRange && !PlayerInAttackRange)
        {
            PlayerChase();
        }
        if (PlayerInSightRange && PlayerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!WalkPointSet)
        {
            SearchWalkPoint();
        }

        if (WalkPointSet)
        {
            agent.SetDestination(WalkPoint);
        }

        Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

        if (DistanceToWalkPoint.magnitude < 1f)
        {
            WalkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float RandomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float RandomX = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, whatIsGround))
            WalkPointSet = true;
    }

    private void PlayerChase()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!AlreadyAttacked)
        {
            GameObject projectile = Instantiate(projectileCO, player.transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            
            Destroy(projectile, 2f);

            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }

    //Take Damage
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Kill();
        }
    }

    //Destroy
    public void Kill()
    {
        Destroy(gameObject);
    }
}
