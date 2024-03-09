using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombBullet : MonoBehaviour
{
    [SerializeField] private UnitStatsConfig _configStats;
    private StarObject _bombBullet;

    public void Init(Vector3 direction)
    {
        _bombBullet = new StarObject(_configStats.stats, direction, gameObject);
    }

    private void FixedUpdate()
    {
        _bombBullet.Move(transform);
        if (!GameArea.Instance.Contains(transform.position))
            _bombBullet.Death(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_configStats.stats.damage, gameObject);
            _bombBullet.Death(gameObject);
        }
    }
}