using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavesConfig", menuName = "ScriptableObjects/WavesConfig")]
public class WavesConfig : ScriptableObject
{
    public List<Wave> waves;
}