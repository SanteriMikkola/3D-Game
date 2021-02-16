using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEffect : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject particleEffect;
    public GameObject PointLight;
    public GameObject Trail;
    public GameObject ProjectilePF;


    public void Awake()
    {
        GameObject trail = Instantiate(ProjectilePF.gameObject, ProjectilePF.transform.position, Quaternion.identity);

        trail.transform.LookAt(ProjectilePF.transform.position);

        particleEffect.transform.position = attackPoint.transform.position;
    }
}
