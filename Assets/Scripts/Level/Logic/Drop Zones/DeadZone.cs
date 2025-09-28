using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeadZone : MonoBehaviour
{
    LevelStats _levelStats;
    [SerializeField] private Rope _rope;
    [Inject]
    private void Construct(LevelStats levelStats)
    {
        _levelStats = levelStats;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            _rope.ConnectBall(collision.GetComponent<Ball>());
            _levelStats.RemovePoints(100);
        }
    }
}
