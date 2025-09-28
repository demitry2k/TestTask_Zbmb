using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RopeSwing : MonoBehaviour
{
    private float _ropeLength = 0f;
    private float _swingAngle = 0f;
    private float _swingSpeed = 0f;
    private float gravity;
    private float _secondMeter;

    [Inject]
    public void Construct(SwingData swingData)
    {
        _ropeLength = swingData.ropeLength;
        _swingAngle = swingData.swingAngle;
        _swingSpeed = swingData.swingSpeed;
    }

    void Start()
    {
        gravity = Physics2D.gravity.y * -1; //Используем гравитацию уровня
    }

    void Update()
    {
        _secondMeter += Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, 0f,  MathPendulum(_swingAngle, gravity, _ropeLength, _secondMeter, _swingSpeed));
    }

    private float MathPendulum(float swingAngle, float gravity, float pendulumLength, float time, float speed)
    {
        //Формула гармонических колебаний из википедии с мной добавленным множетелем speed
        float result = swingAngle * Mathf.Sin(Mathf.Sqrt(pendulumLength / gravity) * time * speed); 
        return result;
    }
}
