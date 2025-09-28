using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rope : MonoBehaviour
{
    [SerializeField] private LevelControlsHandler _controlsHandler;
    [SerializeField] private RopeSwing _swingScript;
    private BallCollection _ballCollection;
    private Ball _attachedBall;
    [SerializeField] private FixedJoint2D _ballJoint;

    [Inject]
    private void Construct(BallCollection collection, LevelStats levelStats)
    {
        _ballCollection = collection;
        levelStats.onGameOver.AddListener(DestroyBall);
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _controlsHandler.Action.AddListener(ReleaseBall);
        SpawnBall();
    }

    public void SpawnBall()
    {
        if (_attachedBall == null)
        {
            ConnectBall(_ballCollection.Get());
        }
    }
    public void ConnectBall(Ball ball)
    {
        if (_attachedBall == null)
        {
            _attachedBall = ball;
            _attachedBall.transform.position = _ballJoint.transform.position;
            _ballJoint.connectedBody = _attachedBall.Rigidbody;
        }
    }
    public void ReleaseBall()
    {
        if (_attachedBall != null)
        {
            _attachedBall.Rigidbody.isKinematic = false;
            _ballJoint.connectedBody = null;
            _attachedBall = null;
        }
    }

    public void DestroyBall()
    {
        if (_attachedBall != null)
        {
            _ballJoint.connectedBody = null;
            Destroy(_attachedBall.gameObject);
        }
    }
}
