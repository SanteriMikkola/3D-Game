using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject attackEnd;
    public GameObject ProjectilePF;

    public GameObject Player;
    
    public float range;
    public float TimeBetweenAttack;
    bool alreadyAttack;
    public float damage;
    public float ammo;
    private bool isReloading = false;
    public ParticleSystem muzzleFlash;
    public float PlayerHealth;


    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1") && ammo > 0f)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && ammo <= 0f)
        {
            StartCoroutine(Reloading());
            return;
        }
    }
    private void Shoot()
    {
        muzzleFlash.Play();

        if (!alreadyAttack)
        {
            GameObject projectile = Instantiate(ProjectilePF, attackPoint.transform.position, Quaternion.identity);
            Vector3 targetPoint = (attackEnd.transform.position);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(attackPoint.transform.forward * 15f, targetPoint, ForceMode.Impulse);
            ammo--;
            
            projectile.transform.LookAt(targetPoint);
            projectile.transform.Rotate(0f, -90f, 0f);
            
            Destroy(projectile, 2f);

            alreadyAttack = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttack);
        }
    }
    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        Debug.Log("Reloading!");

        yield return new WaitForSeconds(2f);

        ammo = 10f;
        isReloading = false;
    }
    //Take Damage
    public void TakeDamage(float amount)
    {
        PlayerHealth -= amount;
        if (PlayerHealth <= 0f)
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
