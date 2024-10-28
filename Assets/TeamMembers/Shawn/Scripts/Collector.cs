using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectables collectable = collision.GetComponent<ICollectables>();
        if(collectable != null )
        {
            collectable.Collect();
        }
    }
}
