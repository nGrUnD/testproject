using UnityEngine;

public class StarObject : Unit
{
    public StarObject(UnitStats unitStats, Vector3 direction, GameObject gameObject) : base(unitStats, direction, gameObject)
    {
        UnitStats = unitStats;
        Direction = direction;
        Init(gameObject);
    }

    public override void Death(GameObject attacker)
    {
        base.Death(attacker);
        if (attacker.CompareTag("Player"))
            LevelManager.Instance.AddGamePoints(UnitStats.gamePoints);
    }

    public override void Move(Transform transform)
    {
        base.Move(transform);
    }
    
    public override void Rotate(Transform transform)
    {
        base.Rotate(transform);
    }
}