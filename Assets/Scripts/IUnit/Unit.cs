using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : IMovable
{
    public UnitStats UnitStats;
    public Vector3 Direction;
    private GameObject _gameObject;

    public Unit(UnitStats unitStats, Vector3 direction, GameObject gameObject)
    {
        UnitStats = unitStats;
        Direction = direction;
        Init(gameObject);
    }

    public virtual void Init(GameObject gameObject)
    {
        _gameObject = gameObject;
        if (gameObject.TryGetComponent<Health>(out var health))
        {
            health.ChangeHealth(UnitStats.health);
            health.OnDeath.AddListener(Death);
            health.OnHit.AddListener(Hit);
        }
    }

    public virtual void Death(GameObject attacker)
    {
        if (UnitStats.deathFX)
            GameObject.Instantiate(UnitStats.deathFX, _gameObject.transform.position, Quaternion.identity);
        GameObject.Destroy(_gameObject);
    }
    
    public virtual void Hit(float health)
    {
        if (UnitStats.hitFX)
            GameObject.Instantiate(UnitStats.hitFX, _gameObject.transform.position, Quaternion.identity);
    }

    public virtual void Move(Transform transform)
    {
        transform.position += Direction * (UnitStats.speed * Time.fixedDeltaTime);
    }

    public virtual void Rotate(Transform transform)
    {
        transform.Rotate(0f, 0f, UnitStats.speedRotation * Time.fixedDeltaTime);
    }
}