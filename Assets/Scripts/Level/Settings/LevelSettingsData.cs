using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsData", menuName = "ScriptableObjects/LevelSettingsData", order = 3)]
public class LevelSettingsData : ScriptableObject
{
    public SwingData swingData;
    public BallData[] ballVariants;
    public GameObject backgroundPrefab;
}
