using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : MonoBehaviour
{
    [SerializeField] private UnitStatsConfig _configStats;
    private StarObject _asteroid;
    private void Start()
    {
        Vector3 direction = Random.insideUnitSphere;
        direction.z = 0f;
        
        _asteroid = new StarObject(_configStats.stats, direction, gameObject);
    }
    private void FixedUpdate()
    {
        _asteroid.Move(transform);
        _asteroid.Rotate(transform);
        GameArea.Instance.TryChangePosition(transform);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out var health) && other.CompareTag("Player"))
        {
            health.TakeDamage(_configStats.stats.damage, gameObject);
        }
    }
}
