using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBounce : MonoBehaviour
{

    //change speed
    float speed = 5f;

    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to new Y
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
