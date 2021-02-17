using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Transform projectile;
    public float DAMAGE = 10f;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Shooting shooting = GetComponent<Shooting>();
            //shooting.damage = DAMAGE;
            Enemy enemy = GetComponent<Enemy>();
            enemy.TakeDamage(DAMAGE);
            Debug.Log("Enemy damaged!");
            Destroy(projectile.gameObject);
        }
    }
}
