using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    ParticleSystem _burstParticle;

    SpriteRenderer _spriteRenderer;
    CircleCollider2D _collider;

    private float lifetime;

    private void Awake() {
        _burstParticle = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        lifetime += Time.deltaTime;

        if (lifetime >= 30f)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void ToggleComponents(bool state)
    {
        _spriteRenderer.enabled = state;
        _collider.enabled = state;
    }

    private void OnEnable() {
        lifetime = 0f;
        ToggleComponents(true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player") || collision.CompareTag("PlayerAttack")) return;

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
