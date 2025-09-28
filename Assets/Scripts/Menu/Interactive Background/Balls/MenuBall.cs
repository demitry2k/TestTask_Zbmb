using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    private BallData _data;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public BallData Data { get => _data; set => _data = value; }

    public void Initialize(BallData ballData)
    {
        tag = "Ball";
        _data = ballData;
        _spriteRenderer.sprite = _data.sprite;
        _spriteRenderer.color = _data.color;
    }

}
