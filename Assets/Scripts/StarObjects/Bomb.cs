using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [System.Serializable]
    private class BulletDirection
    {
        public List<Vector3> directions = new List<Vector3>();
    }
    [SerializeField] private BombBullet _prefabBombBullet;
    [SerializeField] private List<BulletDirection> _bulletDirections = new List<BulletDirection>();
    [SerializeField] private UnitStatsConfig _configStats;

    private StarObject _bomb;
    private Health _health;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _bomb = new StarObject(_configStats.stats, Vector3.left, gameObject);

        if (TryGetComponent<Health>(out var health))
        {
            health.OnDeath.AddListener(Death);
        }
    }

    private void FixedUpdate()
    {
        _bomb.Move(transform);
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (GameArea.Instance.TryChangeDirection(out var newDirection, transform))
        {
            _bomb.Direction = newDirection;
        }
    }

    private void Death(GameObject attacker)
    {
        if (_prefabBombBullet)
        {
            int variation = Random.Range(0, _bulletDirections.Count);
            for (int i = 0; i < 4; i++)
            {
                Vector3 direction = _bulletDirections[variation].directions[i];
                var bullet = Instantiate(_prefabBombBullet, LevelManager.Instance.spawnParent);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
                bullet.Init(direction);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out var health) && other.CompareTag("Player"))
        {
            health.TakeDamage(_configStats.stats.damage, gameObject);
        }
    }
}