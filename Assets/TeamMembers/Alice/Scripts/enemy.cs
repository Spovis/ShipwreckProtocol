using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    public GameObject projectileTemplate;
    public Transform player;
    public float detectRange = 15f;
    private Animator animator;
    public Vector2 minBoundary = new Vector2(-74f, -18f); 
    public Vector2 maxBoundary = new Vector2(116.05f, 9.940499f); 
    private bool canAttack = true;
    public int attackDamage = 10;
    public bool is_hunter;
    public float attackPause = 2f; // it should wait 2 seconds between attacks
    public int health = 100;  
    

    // weapon hits enemy. amount of damage given by the weapon hitting it
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public delegate void ObstacleCollisionHandler();
    public event ObstacleCollisionHandler OnObstacleCollision;

 private void OnCollisionEnter2D(Collision2D collision)     
    {         
        // Handle obstacle collisions for patrol behavior
        if (collision.gameObject.CompareTag("Obstacle") && currentBehavior is PatrolBehavior)         
        {             
            Debug.Log($"Enemy collided with: {collision.gameObject.name}");             
            ((PatrolBehavior)currentBehavior).HandleObstacleCollision();
        }

        // Handle general behavior collisions
        if (currentBehavior != null)         
        {             
            currentBehavior.OnBehaviorCollisionEnter2D();         
        }     
    }     

    //enemy dies
    private void Die()
    {
        Debug.Log("Enemy died and was removed from screen");
        Destroy(gameObject);  // take enemy offscreen
    }

    public EnemyBaseBehavior currentBehavior;

        void Start(){
        SetBehavior(new IdleBehavior(this, minBoundary, maxBoundary));
    }

        public void SetBehavior(EnemyBaseBehavior newBehavior)
        {
            if (currentBehavior != null)
            {
                currentBehavior.OnExitBehavior();
            }
            currentBehavior = newBehavior;
            currentBehavior.OnEnterBehavior();
        }
        //once per frame this updates, checking for player in proximity. When noticed, it then can call a behavior.
       
        void Update(){
            if (currentBehavior != null)
            {
                currentBehavior.OnBehaviorUpdate();  // Call behavior update logic
            }

        }

    // Check if the player is within the defined boundaries
    
        
        //when colliding, enter

        //on behavior, trigger
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (currentBehavior != null)
            {
                currentBehavior.OnBehaviorTriggerEnter2D();
            }
        }
       
}
