using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallDataGenerator : MonoBehaviour
{
    private BallData[] _ballVariants;
    private System.Random randomizer = new System.Random();

    [Inject]
    private void Construct(BallData[] ballVariants)
    {
        _ballVariants = ballVariants;
    }

    public BallData GenerateData()
    {
        BallData generatedData = _ballVariants[randomizer.Next(0, _ballVariants.Length)];
        return generatedData;
    }
}
