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
    public float attackPause = 2f;
    public int health = 100;

    public EnemyBaseBehavior currentBehavior;

    void Start()
    {
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

    void Update()
    {
        if (currentBehavior != null)
        {
            currentBehavior.OnBehaviorUpdate();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //apply damage
        if (collider.CompareTag("PlayerAttack"))
        {
            TakeDamage(20);  
        }

        //change behaviors if needed
        if (currentBehavior != null)
        {
            currentBehavior.OnBehaviorTriggerEnter2D();
        }
    }

    // Damage 
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    //die when zero
    private void Die()
    {
        Debug.Log("Enemy died and was removed from screen");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //obstacle collisions for patrol behavior
        if (collision.gameObject.CompareTag("Obstacle") && currentBehavior is PatrolBehavior)
        {
            Debug.Log($"Enemy collided with: {collision.gameObject.name}");
            ((PatrolBehavior)currentBehavior).HandleObstacleCollision();
        }

        //general collisions
        if (currentBehavior != null)
        {
            currentBehavior.OnBehaviorCollisionEnter2D();
        }
    }
}
