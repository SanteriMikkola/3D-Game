using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Transform projectile;
    public float Damage = 10f;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Shooting shooting = GetComponent<Shooting>();
            //Enemy enemy = GetComponent<Enemy>();
            //enemy.EnemyDamage = Damage;
            shooting.TakeDamage(Damage);
            Debug.Log("Player take damage!");
            Destroy(projectile.gameObject);
        }
    }
}
