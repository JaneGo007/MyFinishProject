using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    //Bullet 
    [SerializeField] private GameObject bulletPrefab;
    Vector3 spawnPos;
    
    //Start because awake is in Enemy and I don't want to override
    void Start()
    {
        StartCoroutine("Shoot");
   }

    //Shoot every 1-4 seconds if the game is on
    IEnumerator Shoot()
    {
        while (playerScript.gameOn)
        {
            if (transform.position.x >= -16)
            {
                spawnPos = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                Instantiate(bulletPrefab, spawnPos, bulletPrefab.transform.rotation);
            }
            yield return new WaitForSeconds(Random.Range(1f, 4f));
        }
    }
}
