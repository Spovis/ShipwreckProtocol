using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    public Transform player;
    public float detects_range = 5f;

    //update is called once per frame
    void Update()
    {
        detecting_player();
    }

    //detects the player if they are in range
    private void detecting_player()
    {
        float dist_to_player = Vector3.Distance(transform.position, player.position);

        if (dist_to_player <= detects_range){
            Debug.Log("Player is detected within range! The distance= " + dist_to_player);
            // Call specific function when the player is in range(some kind of attack function for example)
        }else{
            Debug.Log("Player is not in range. The distance= " + dist_to_player);
        }
    }
}
