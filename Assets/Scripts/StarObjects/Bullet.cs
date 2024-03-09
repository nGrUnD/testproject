using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private ParticleSystem _hitFX;
    private GameObject _owner;

    public void Init(GameObject owner)
    {
        _owner = owner;
    }

    private void FixedUpdate()
    {
        var direction = transform.up * (_speed * Time.fixedDeltaTime);
        transform.position += direction;
        if (!GameArea.Instance.Contains(transform.position))
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_owner == other.gameObject) return;
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_damage, _owner);
            if (_hitFX)
                Instantiate(_hitFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
