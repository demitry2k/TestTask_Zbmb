using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallCollection : MonoBehaviour //Изначально это был юнити-пул, но он вызывал баги, теперь просто хранилище шаров,
{
    [SerializeField] private Ball _ballPrefab;
    private List<Ball> _list = new List<Ball>();
    private BallDataGenerator _ballDataGenerator;

    [Inject]
    private void Construct(BallDataGenerator ballDataGenerator)
    {
        _ballDataGenerator = ballDataGenerator;
    }

    public Ball Get()
    {
        Ball ball = Instantiate(_ballPrefab, transform);
        _list.Add(ball);
        ball.Initialize(_ballDataGenerator.GenerateData(), this);
        return ball;
    }

    public void Destroy(Ball ballInstance)
    {
        _list.Remove(ballInstance);
        Destroy(ballInstance.gameObject);
    }
}
