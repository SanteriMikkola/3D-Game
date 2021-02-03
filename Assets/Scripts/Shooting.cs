using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject ProjectilePF;

    public GameObject Player;
    
    public float range;
    public float TimeBetweenAttack;
    bool alreadyAttack;
    public float damage;
    public float ammo;
    private bool isReloading = false;
    float Yrotate;


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
        if (!alreadyAttack)
        {
            GameObject projectile = Instantiate(ProjectilePF, attackPoint.transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            ammo--;

            Yrotate = Player.transform.rotation.y;

            Yrotate = Mathf.Clamp(0f, Yrotate, 0f);

            projectile.transform.rotation = Quaternion.Euler(0f, Yrotate, 0f);

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
}
