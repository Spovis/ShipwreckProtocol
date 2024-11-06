using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private int health = 100;
    private int attackPower;
    public Transform player;
    private Animator animator;
    public Rigidbody2D fireballPrefab;
    private BossStateMachine stateMachine = new BossStateMachine();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.GetState().OnStateUpdate(gameObject, animator, stateMachine, fireballPrefab, player);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        stateMachine.GetState().OnTriggerEnter2D(collider, this);
    }

    public void TakeDamage(int damage) {
        stateMachine.UpdateState(2, animator);
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
}
