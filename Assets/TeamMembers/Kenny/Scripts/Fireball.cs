using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;  // Ensure direction is a unit vector
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;  // Move the fireball
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);  // Destroy the fireball when it leaves the screen
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // if its the player
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
