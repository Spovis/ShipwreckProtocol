using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Subject
{
    private int health = 5;
    [SerializeField] string reloadSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //lowers health
        if (Input.GetKeyDown(KeyCode.K) && health != 0)
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
        }
        if(health == 0) 
        { 
            SceneManager.LoadScene(reloadSceneName);
        }
    }
}
