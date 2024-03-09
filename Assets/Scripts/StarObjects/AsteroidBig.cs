using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidBig : MonoBehaviour
{
    [SerializeField] private UnitStatsConfig _configStats;
    [SerializeField] private AsteroidSmall _prefabSmall;
    [SerializeField] private int _count = 5;
    private StarObject _asteroid;

    private void Start()
    {
        Vector3 direction = Random.insideUnitSphere;
        direction.z = 0f;
        
        _asteroid = new StarObject(_configStats.stats, direction, gameObject);
        if (TryGetComponent<Health>(out var health))
        {
            health.OnDeath.AddListener(Death);
        }
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

    private void Death(GameObject attacker)
    {
        for (int i = 0; i < _count; i++)
            SpawnAsteroidSmall();
    }

    private void SpawnAsteroidSmall()
    {
        var asteroid = Instantiate(_prefabSmall, LevelManager.Instance.spawnParent);
        asteroid.transform.position = transform.position;
        asteroid.transform.rotation = Quaternion.identity;
    }
}