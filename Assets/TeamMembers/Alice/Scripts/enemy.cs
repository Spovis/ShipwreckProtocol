using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    public Transform player;
    public float detectRange = 5f;
    private Animator animator;

    //update is called once per frame
    private EnemyBaseBehavior currentBehavior;

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
        void Update()
        {
            if (currentBehavior != null){
                currentBehavior.OnBehaviorUpdate();  // Call behavior update logic
            }
        }
        
        //when colliding, enter
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (currentBehavior != null)
            {
                currentBehavior.OnBehaviorCollisionEnter2D();
            }
        }
        //on behavior, trigger
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (currentBehavior != null)
            {
                currentBehavior.OnBehaviorTriggerEnter2D();
            }
        }
       
}


