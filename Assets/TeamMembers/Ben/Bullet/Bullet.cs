using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bullet : MonoBehaviour
{
    private ParticleSystem _burstParticle;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private TrailRenderer _trailRenderer;
    private Light2DBase _lightObj;

    private float lifetime;
    public static float bulletLifetime = 10f;

    private void Awake() {
        _burstParticle = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _lightObj = GetComponentInChildren<Light2DBase>();
    }

    private void Update() {
        lifetime += Time.deltaTime;

        if (lifetime >= bulletLifetime) // Bullet will automatically destroy/return to pool after 30 seconds
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void ToggleComponents(bool state)
    {
        _spriteRenderer.enabled = state;
        _collider.enabled = state;
        _trailRenderer.enabled = state;
        _lightObj.enabled = state;
    }

    private void OnEnable() {
        lifetime = 0f;
        ToggleComponents(true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water") || collision.gameObject.layer == LayerMask.NameToLayer("ShallowWater")) return;
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerAttack")) return;

        _burstParticle.Play();
        ToggleComponents(false);

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSecondsRealtime(1f);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
