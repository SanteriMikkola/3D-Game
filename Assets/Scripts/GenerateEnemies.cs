using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemy;
    public float xPos;
    public float zPos;
    public float enemyCount;

    void Start()
    {
        StartCoroutine(EnemyLocation());
    }

    IEnumerator EnemyLocation()
    {
        while (enemyCount < 5)
        {
            xPos = Random.Range(-18f, 20f);
            zPos = Random.Range(23f, -7f);
            Instantiate(enemy, new Vector3(xPos, 2.6f, zPos), Quaternion.identity);

            yield return new WaitForSeconds(0.2f);

            enemyCount++;
        }
    }
}
