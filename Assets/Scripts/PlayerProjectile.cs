using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Transform projectile;
    public float PlayerDamage = 10f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(PlayerDamage);
            Debug.Log("Enemy damaged!");
            Destroy(projectile.gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(projectile.gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(projectile.gameObject);
        }
        if (other.gameObject.CompareTag("Roof"))
        {
            Destroy(projectile.gameObject);
        }
    }
}
