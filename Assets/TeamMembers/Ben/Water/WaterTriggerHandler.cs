using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private GameObject _splashParticles;

    private BoxCollider2D _coll;
    private InteractableWater _water;

    private void Awake()
    {
        _coll = GetComponent<BoxCollider2D>();
        _water = GetComponentInParent<InteractableWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack")) return;

        Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();

        if(rb != null)
        {
            ObjectPoolManager.SpawnObject(_splashParticles, collision.transform.position + Vector3.down, Quaternion.identity, PoolType.WaterParticles);

            int multiplier = rb.velocity.y < 0 ? -1 : 1;

            float vel = rb.velocity.y * _water.forceMultiplier;
            vel = Mathf.Clamp(Mathf.Abs(vel), 0f, _water.MaxForce);
            vel *= multiplier;

            _water.Splash(collision, vel);
        }
    }
}
