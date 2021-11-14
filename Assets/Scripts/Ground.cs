using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private EnemySpawnManager spawnManager; //Spawn manager for speed of the ground calculation 
    private Player playerScript; //Player script for checking is the game on
    public float speed; 

    //Do on awake
    void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<EnemySpawnManager>();
    }
    
    //Moving ground right to create illusion that the player is moving
    void Update()
    {
        if (playerScript.gameOn)
        {
            speed = spawnManager.enemySpeed / 2;
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (transform.position.x <= -104.8f) transform.position = new Vector3(104.7f, transform.position.y, transform.position.z);

        }

    }
}
