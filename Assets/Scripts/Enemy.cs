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
    public GameObject attackpoint;
    public GameObject attackEnd;
    public float ammo = 10f;
    private bool isReloading = false;

    //States
    public float sightRange, attackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    //Health
    public float health;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
        agent.transform.LookAt(player.position);
        attackpoint.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        agent.transform.Rotate(0f, player.transform.position.x, 0f);
        agent.transform.Rotate(0f, player.transform.position.z, 0f);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!AlreadyAttacked && ammo > 0f)
        {
            if (isReloading)
                return;

            GameObject projectile = Instantiate(projectileCO, attackpoint.transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            ammo--;

            projectile.transform.LookAt(attackEnd.transform.position);

            Destroy(projectile, 2f);

            agent.transform.Rotate(0f, player.transform.position.x, 0f);
            agent.transform.Rotate(0f, player.transform.position.z, 0f);
            agent.SetDestination(transform.position);
            agent.transform.LookAt(player);

            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);

            if (ammo <= 0f)
            {
                agent.transform.Rotate(0f, player.transform.position.x, 0f);
                agent.transform.Rotate(0f, player.transform.position.z, 0f);
                agent.SetDestination(transform.position);
                StartCoroutine(Reloading());
                return;
            }
        }
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        Debug.Log("Enemy reloading!");

        yield return new WaitForSeconds(5f);

        ammo = 10f;
        isReloading = false;
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
        Debug.Log("Enemy is Dead");
    }
}
