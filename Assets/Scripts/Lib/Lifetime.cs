using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] private float _time;
    private Health _health;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (_time == 0f) return;
        if (TryGetComponent<Health>(out var health))
        {
            _health = health;
            StartCoroutine(LifetimeRotine());
        }
    }

    IEnumerator LifetimeRotine()
    {
        yield return new WaitForSeconds(_time);
        _health.OnDeath?.Invoke(gameObject);
    }
}
