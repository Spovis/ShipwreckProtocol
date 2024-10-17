using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private int health = 100;
    private int attackPower;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldTakeDamage()) {
            TakeDamage(25);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
        }
        if (health == 0) {
            Die();
        }
    }

    private void Die() {
        // remove the boss from the scene
        Destroy(gameObject);
        Debug.Log("Boss died!");
    }

    public int GetHealth() {
        return health;
    }

    private bool shouldTakeDamage() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // if the player is within 1 unit of the boss, take damage
        if (distanceToPlayer < 2.1) {
            return true;
        }
        return false;
    }
}
