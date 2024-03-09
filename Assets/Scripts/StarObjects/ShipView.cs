using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : MonoBehaviour
{
    [SerializeField] private UnitStatsConfig _configStats;
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private float _speedDamping = 0.1f;

    private Ship _ship;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _ship = new Ship(_configStats.stats, Vector3.zero, gameObject, _speedDamping);

        UICanvas.Instance.UpdateHealthPoints((int)_configStats.stats.health);
    }
    
    private void FixedUpdate()
    {
        _ship.Move(transform);
        _ship.Rotate(transform);
    }

    private void Update()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = BulletPool.Instance.CreateBullet(_bulletParent.position, _bulletParent.rotation);
            bullet.Init(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ufo")) return;
        
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_configStats.stats.damage, gameObject);
        }
    }
}
