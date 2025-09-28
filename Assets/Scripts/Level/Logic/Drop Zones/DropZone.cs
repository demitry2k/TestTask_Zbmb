using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] int _column = 0;
    [SerializeField] private Basket _basketData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            _basketData.RegisterBall(collision.GetComponent<Ball>(), _column);
        }
    }
}
