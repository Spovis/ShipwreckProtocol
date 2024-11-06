using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : Subject
{

    // Update is called once per frame
    void Update()
    {
        //lowers health
        /*if (Input.GetKeyDown(KeyCode.K) && health != 0)
        {
            health--;
            NotifyObserver(PlayerActions.Hurt);
            Debug.Log("Health lowered to " + health);
        }
        //raises health up to 5
        else if (Input.GetKeyDown(KeyCode.J) && health < 5)
        {
            health++;
            NotifyObserver(PlayerActions.Heal);
            Debug.Log("Health raised to " + health);
        }*/
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
    }
}
