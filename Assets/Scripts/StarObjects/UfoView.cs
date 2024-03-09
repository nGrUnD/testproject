using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UfoView : MonoBehaviour
{
    [SerializeField] private UnitStatsConfig _configStats;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletParent;
    private StarObject _ufo;
    private Vector3 _targetPosition;
    private GameObject _player;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _player = GameObject.FindWithTag("Player");
        _ufo = new StarObject(_configStats.stats, Vector3.zero, gameObject);
        ChangeTargetPosition();
        
        StartCoroutine(FirePerTime());
    }

    private void FixedUpdate()
    {
        _ufo.Move(transform);
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            ChangeTargetPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out var health) && other.CompareTag("Player"))
        {
            health.TakeDamage(_configStats.stats.damage, gameObject);
        }
    }

    private IEnumerator FirePerTime()
    {
        var timeout = new WaitForSeconds(_configStats.stats.firerate);
        while (true)
        {
            yield return timeout;
            Fire();
        }
    }

    private void Fire()
    {
        if (!_player) return;

        Vector3 targetPosition = _player.transform.position;
        targetPosition.x += Random.Range(-5f, 5f);
        targetPosition.y += Random.Range(-5f, 5f);
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        var bullet = Instantiate(_bulletPrefab, LevelManager.Instance.spawnParent);
        bullet.transform.position = _bulletParent.position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        bullet.Init(gameObject);
    }

    private void ChangeTargetPosition()
    {
        _targetPosition = GameArea.Instance.GetRandomPoint();
        _ufo.Direction = (_targetPosition - transform.position).normalized;
    }
}
