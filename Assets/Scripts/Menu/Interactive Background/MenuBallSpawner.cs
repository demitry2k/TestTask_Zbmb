using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBallSpawner : MonoBehaviour
{
    [SerializeField] private MenuBall _ballPrefab;
    [SerializeField] private BallData[] _ballVariants;
    private List<MenuBall> _list = new List<MenuBall>();
    private System.Random randomizer = new System.Random();

    public List<MenuBall> List { get => _list; set => _list = value; }

    public void Spawn()
    {
        MenuBall ball = Instantiate(_ballPrefab, transform);
        ball.transform.localPosition = new Vector3(UnityEngine.Random.Range(-3f,3f), 0f, 0f);
        _list.Add(ball);
        ball.Initialize(GenerateData());
    }
    private BallData GenerateData()
    {
        BallData generatedData = _ballVariants[randomizer.Next(0, _ballVariants.Length)];
        return generatedData;
    }

    public void ClearList()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            Destroy(_list[i].gameObject);
        }
        List.Clear();
    }
}
