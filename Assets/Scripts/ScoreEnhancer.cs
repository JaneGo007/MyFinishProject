using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEnhancer : MonoBehaviour
{
    protected float speed = 5f;
    protected float rotationSpeed = 0.2f;
    protected Ground groundScript;  //Spawn manager for getting the speed
    protected Player playerScript; //Player script for checking is the game on

    //Do on awake
    void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        groundScript = GameObject.FindWithTag("Ground").GetComponent<Ground>();
    }

    //Move & rotate
    void Update()
    {
        if (playerScript.gameOn)
        {
            speed = groundScript.speed;
            transform.Rotate(Vector3.forward, rotationSpeed);
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
    }

    //Do on collision
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("DeleteBorder") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
