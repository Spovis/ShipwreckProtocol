using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float life_of_projectile = 3f; 
    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, life_of_projectile);
    }

    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
