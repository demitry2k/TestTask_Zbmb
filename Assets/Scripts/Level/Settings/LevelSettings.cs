using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSettings : MonoBehaviour
{
    [SerializeField] private SwingData _swingData;
    [SerializeField] private BallData[] _ballVariants;
    [SerializeField] private GameObject _backgroundPrefab;

    public void Initialize(SwingData swingData, BallData[] ballVariants, GameObject backgroundPrefab)
    {
        _swingData = swingData;
        _ballVariants = ballVariants;
        _backgroundPrefab = backgroundPrefab; 
    }

    public SwingData SwingData { get => _swingData; }
    public BallData[] BallVariants { get => _ballVariants; }
    public GameObject BackgroundPrefab { get => _backgroundPrefab; }
}
