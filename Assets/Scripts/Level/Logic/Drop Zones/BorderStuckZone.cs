using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderStuckZone : MonoBehaviour
{
    [SerializeField] private Rope _rope;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            StartCoroutine(ReturnOfStuckedCoroutine(collision.GetComponent<Ball>()));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ReturnOfStuckedCoroutine(Ball ball)
    {
        yield return new WaitForSeconds(5f);
        _rope.ConnectBall(ball);
    }
}
