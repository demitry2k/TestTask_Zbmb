using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketView : MonoBehaviour
{
    [SerializeField] private Grid _viewGrid;

    public void UpdateBallPositions(ref Ball[,] _cellsGrid)
    {
        for (int column = 0; column < _cellsGrid.GetLength(0); column++)
        {
            for (int row = 0; row < _cellsGrid.GetLength(1); row++)
            {
                if (_cellsGrid[column, row] != null)
                {
                    Vector3 worldBallPosition = _viewGrid.GetCellCenterWorld(new Vector3Int(column, row, 0)) - _viewGrid.cellGap / 2; //Вычитаю половину гэпа, чтобы получить реальный визуальный центр ячейки
                    _cellsGrid[column, row].transform.DORotate(Vector2.zero, 0.1f);
                    DOTween.Sequence()
                    .Append(_cellsGrid[column, row].transform.DOMoveX(worldBallPosition.x, 0.1f))
                    .Append(_cellsGrid[column, row].transform.DOMove(worldBallPosition, 1f).SetEase(Ease.InOutQuad));
                    _cellsGrid[column, row].Rigidbody.isKinematic = true;
                    _cellsGrid[column, row].Rigidbody.velocity = Vector2.zero;
                    _cellsGrid[column, row].Rigidbody.angularVelocity = 0f;
                }
            }
        }
    }

}
