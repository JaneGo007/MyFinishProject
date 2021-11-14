using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //For spawning enemy
    [SerializeField] private GameObject enemyNormalPrefab;
    [SerializeField] private GameObject enemySlowPrefab;
    [SerializeField] private GameObject enemyShootPrefab;
    public float enemySpeed = 6f;
    private Vector3 spawnPos;
    private float repeatRate = 5f;
    private float healsRepeatRate = 10f;
    private bool canSpawn = true;
    private bool canSpawnEnhancer = true;
    //Player script for checking is the game on
    private Player playerScript;
    //For enhancers
    [SerializeField] private GameObject enhancerScore5Prefab;
    [SerializeField] private GameObject enhancerScore20Prefab;
    [SerializeField] private GameObject enhancerHealPrefab;

    //Do on awake
    void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        StartCoroutine("IncreasingSpeed");
    }

    //Choose which enemy to spawn & where to spawn
    void Update()
    {
        if (playerScript.gameOn && canSpawn)
        {
            spawnPos = new Vector3(45, 1, Random.Range(-12.5f, 12.5f));
            int whatToSpawn = Random.Range(0, 3);
            if (whatToSpawn == 0) Instantiate(enemyNormalPrefab, spawnPos, enemyNormalPrefab.transform.rotation);
            else if (whatToSpawn == 1) Instantiate(enemySlowPrefab, spawnPos, enemySlowPrefab.transform.rotation);
            else if (whatToSpawn == 2) Instantiate(enemyShootPrefab, spawnPos, enemyShootPrefab.transform.rotation);
            StartCoroutine("SpawnEnemyWait");
        }

        if (playerScript.gameOn && canSpawnEnhancer)
        {
            spawnPos = new Vector3(45, 1.2f, Random.Range(-12f, 12f));
            int whatEnhancerToSpawn = Random.Range(0, 4);
            if (whatEnhancerToSpawn == 0 || whatEnhancerToSpawn == 1) Instantiate(enhancerScore5Prefab, spawnPos, enhancerScore5Prefab.transform.rotation);
            else if (whatEnhancerToSpawn == 2) Instantiate(enhancerScore20Prefab, spawnPos, enhancerScore20Prefab.transform.rotation);
            else if (whatEnhancerToSpawn == 3) Instantiate(enhancerHealPrefab, spawnPos, enhancerHealPrefab.transform.rotation);
            StartCoroutine("SpawnEnhancerWait");
        }
    }

    //Spawn enemy rollback
    IEnumerator SpawnEnemyWait()
    {
        canSpawn = false;
        yield return new WaitForSeconds(repeatRate);
        canSpawn = true;
    }

    //Increasing speed every 3 seconds
    IEnumerator IncreasingSpeed()
        {
            while (playerScript.gameOn)
            {
                yield return new WaitForSeconds(3);  
                enemySpeed += 1;
                if (repeatRate >= 0.7f) repeatRate -= 0.25f;
            }
        }

    //Spawn enhancer rollback
    IEnumerator SpawnEnhancerWait()
    {

        canSpawnEnhancer = false;
        yield return new WaitForSeconds(healsRepeatRate);
        if (healsRepeatRate >= 3.2f) healsRepeatRate -= 0.2f;
        canSpawnEnhancer = true;
    }
}
