using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterBullet : MonoBehaviour
{
    //Getting player coordinates
    private GameObject player;
    private Vector3 playerPos;
    private Player playerScript;
    protected EnemySpawnManager spawnManager; //Spawn manager for speed of the bullet calculation
    private float speedBullet; //The actual speed 
    
    //Do on awake
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<EnemySpawnManager>();
        speedBullet = spawnManager.enemySpeed + 10f;
        playerScript = player.GetComponent<Player>();
        //Calculation of the trajectory
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 goToPos = playerPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(goToPos, Vector3.forward);
        transform.rotation = rotation;
    }


    //Move bullet if the game is on
    void Update()
    {
        if (playerScript.gameOn == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speedBullet);
        }

    }

    //Destroy on collision
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
