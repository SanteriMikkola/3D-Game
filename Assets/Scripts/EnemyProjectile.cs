using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Transform projectile;
    public float EnemyDamage = 5f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Shooting>().TakeDamage(EnemyDamage);
            Debug.Log("Player take damage!");
            Destroy(projectile.gameObject);
        }
    }
}
