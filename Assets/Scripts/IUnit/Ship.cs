using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Unit
{
    private float _shipSpeed;
    private float _speedDamping;
    private float _rotationZ;
    
    public Ship(UnitStats unitStats, Vector3 direction, GameObject gameObject, float speedDamping) : base(unitStats, direction, gameObject)
    {
        UnitStats = unitStats;
        Direction = direction;
        _speedDamping = speedDamping;
        Init(gameObject);
    }

    public override void Death(GameObject attacker)
    {
        base.Death(attacker);
        LevelManager.Instance.PlayerLose();
    }

    public override void Hit(float health)
    {
        base.Hit(health);
        if (health < 0) health = 0f;
        UICanvas.Instance.UpdateHealthPoints((int)health);
    }

    public override void Move(Transform transform)
    {
        var vertical = Input.GetAxis("Vertical");
        _shipSpeed = Mathf.Lerp(_shipSpeed, Mathf.Max(vertical, 0), _speedDamping);
        transform.position += transform.up * (_shipSpeed * UnitStats.speed * Time.fixedDeltaTime);
        
        GameArea.Instance.TryChangePosition(transform);
    }

    public override void Rotate(Transform transform)
    {
        var horizontal = -Input.GetAxis("Horizontal");
        _rotationZ += horizontal * UnitStats.speedRotation * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0f,0f, _rotationZ);
    }

}
