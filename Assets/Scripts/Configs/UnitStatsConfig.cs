using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatsConfig", menuName = "ScriptableObjects/UnitStatsConfig" )]
public class UnitStatsConfig : ScriptableObject
{
    public string unitName;
    public UnitStats stats;
}