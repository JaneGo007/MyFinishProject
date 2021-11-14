using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemySpawnManager spawnManager;  //Spawn manager for getting the enemy speed
    protected Player playerScript; //Player script for checking is the game on
    //Animation
    [SerializeField] private GameObject animationBody; 
    protected Animator enemyAnim;

    //Do on awake
    void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<EnemySpawnManager>();
        enemyAnim = animationBody.GetComponent<Animator>();
    }

    //Move enemy
    void Update()
    {
        if (playerScript.gameOn) transform.Translate(Vector3.left * Time.deltaTime * spawnManager.enemySpeed);
        else
        {
            enemyAnim.SetTrigger("Die");
        }
    }

    //Check collision
    void OnCollisionEnter(Collision collision) //If CompareTag does the same in the end - do it on one!
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeleteBorder"))
        {
            Delete();
        }
    }

    //Do when enemy should be deleted
    protected virtual void Delete()
    {
        Destroy(gameObject);
    }
}
