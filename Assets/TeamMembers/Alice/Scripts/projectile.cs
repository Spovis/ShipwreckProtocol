using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float life_of_projectile = 3f; 

    private void Start(){
        Destroy(gameObject, life_of_projectile);
    }

    private void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
          
            Destroy(gameObject);
        }
    }
}