using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private int health = 100;
    private int attackPower;
    public Transform player;
    private Animator animator;
    private BossStateMachine stateMachine = new BossStateMachine();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.GetState().OnStateUpdate(gameObject, animator);

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
        stateMachine.UpdateState(1, animator);
    }

    public int GetHealth() {
        return health;
    }

    private bool shouldTakeDamage() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < 2.1) {
            return true;
        }
        return false;
    }
}
