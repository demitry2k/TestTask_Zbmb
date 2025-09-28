using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Ball : MonoBehaviour
{
    private BallCollection _collection;
    private BallData _data;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ParticleSystem _particleEffectForDestroy;
    public BallData Data { get => _data; set => _data = value; }
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }

    public void Initialize(BallData ballData, BallCollection collection)
    {
        tag = "Ball";
        _data = ballData;
        _spriteRenderer.sprite = _data.sprite;
        _spriteRenderer.color = _data.color;
        _collection = collection;
    }

    public void Destroy()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        _spriteRenderer.enabled = false;
        _particleEffectForDestroy.Play();
        yield return new WaitForSeconds(2f);
        _collection.Destroy(this);
    }
}
