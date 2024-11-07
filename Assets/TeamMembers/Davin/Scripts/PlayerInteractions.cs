using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : Subject
{

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.IsAttackPressed && PlayerInput.Instance.CanAttack == true)
        {
            NotifyObserver(PlayerActions.Fire);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealthPack>(out HealthPack component))
        {
            NotifyObserver(PlayerActions.Heal);
        }
        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile component2))
        {
            NotifyObserver(PlayerActions.Hurt);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Fireball>(out Fireball component1))
        {
            NotifyObserver(PlayerActions.Hurt);
        }
    }
}
