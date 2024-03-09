using UnityEngine;

[System.Serializable]
public struct UnitStats
{
    [Header("Game")] 
    public int gamePoints;
    [Header("Fight")]
    public float health;
    public float damage;
    public float firerate;
    [Header("Movement")]
    public float speed;
    public float speedRotation;
    [Header("FX")] 
    public ParticleSystem hitFX;
    public ParticleSystem deathFX;
}