using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "ScriptableObjects/BallData", order = 1)]
public class BallData : ScriptableObject
{
    public string type;
    public Color color;
    public Sprite sprite;
    public int price;
}
