using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Move
    private float verticalInput;
    private float horizontalInput;
    private float speed = 12f;  
    private float leftMoveBorder = 12.5f;
    private float updownMoveBorder = 27f;
    //Health
    public bool gameOn = true;
    [SerializeField] private Slider healthSlider;
    protected float maxHealth = 20f;
    [SerializeField] protected float valueHealth = 20f;
    //Animation
    [SerializeField] private GameObject animationBody;
    private Animator playerAnim;
    //Particles & music
    [SerializeField] private ParticleSystem bloodParticle;
     //Score
    [SerializeField] private Text ScoreText;
    private int score = 0;
    //"You died" screen
    [SerializeField] private GameObject deadScreen;
    [SerializeField] private Text deadScreenText;

    void Awake()
    {
        //Health set slider 
        healthSlider.maxValue = maxHealth;
        healthSlider.gameObject.SetActive(true);
        //Animation connect to Animator
        playerAnim = animationBody.GetComponent<Animator>();
        //Score text
        ScoreText.text = "Score: " + score;
        //Start coroutines that increase speed & score
        StartCoroutine("increasingSpeed");
        StartCoroutine("increasingScore");
        //Dead screen set not active 
        deadScreen.SetActive(false);
    }

    /// <summary>
    /// updownMoveBorder
    /// </summary>

    void Update()
    {
        //Do if player is dead
        if (valueHealth <= 0)
        {
            gameOn = false;
            playerAnim.SetTrigger("Dead");
            healthSlider.gameObject.SetActive(false);
            deadScreen.SetActive(true);
            deadScreenText.text = "You died! Score:" + score;
        }
        //Do if player is alive
        else
        {
            //Move player
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
            if (transform.position.z > leftMoveBorder) transform.position = new Vector3(transform.position.x, transform.position.y, leftMoveBorder);
            else if (transform.position.z < -leftMoveBorder) transform.position = new Vector3(transform.position.x, transform.position.y, -leftMoveBorder);
            if (transform.position.x > updownMoveBorder) transform.position = new Vector3(updownMoveBorder, transform.position.y, transform.position.z);
            else if (transform.position.x < -updownMoveBorder) transform.position = new Vector3(-updownMoveBorder, transform.position.y, transform.position.z);


            //Move slider
            healthSlider.gameObject.transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for collision with enemy
        if (collision.gameObject.CompareTag("EnemyBullet")) valueHealth -= 1f;
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyShooter")) valueHealth -= 3f;
        else if (collision.gameObject.CompareTag("EnemySlow")) valueHealth -= 5f;
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyShooter") 
                    || collision.gameObject.CompareTag("EnemySlow")) bloodParticle.Play();

        //Check for collision with enhancer
        if (collision.gameObject.CompareTag("Score5")) score+= 5;
        else if (collision.gameObject.CompareTag("Score20")) score+=20;
        else if (collision.gameObject.CompareTag("Heal"))
        {
            if (valueHealth + 5 <= maxHealth) valueHealth += 5;
            else valueHealth = maxHealth;
        }

        healthSlider.value = valueHealth;
        ScoreText.text = "Score: " + score;

    }

    //Increasing speed every 5 seconds
    IEnumerator increasingSpeed()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(5);
            speed += 0.5f;
        }
    }

    //Increasing score every 2 seconds
    IEnumerator increasingScore()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(2);
            score++;
            ScoreText.text = "Score: " + score;
        }
    }

    //For reloading the game
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
}






