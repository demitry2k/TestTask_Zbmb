using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwingData", menuName = "ScriptableObjects/SwingData", order = 2)]
public class SwingData : ScriptableObject
{
    public float ropeLength;
    public float swingAngle;
    public float swingSpeed;
}
