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
        particleEffect.transform.position = attackPoint.transform.position;
        Trail.transform.position = ProjectilePF.transform.position;
    }
}
