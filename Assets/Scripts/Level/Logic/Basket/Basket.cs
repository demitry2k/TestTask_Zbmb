using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class Basket : MonoBehaviour
{
    [SerializeField] private BasketView _basketView;
    [SerializeField] private Rope _rope;
    private LevelStats _levelStats;
    private BallCollection _ballCollection;
    private Ball[,] _cellsGrid = new Ball[3,3];

    [Inject]
    private void Construct(BallCollection collection, LevelStats levelStats)
    {
        _ballCollection = collection;
        _levelStats = levelStats;
    }
    public void RegisterBall(Ball newBall, int column)
    {
        if (!IsColumnFilled(column))
        {
            if (CheckIfBallIsNotAddedAlready(newBall))
            {
                AddBall(newBall, column);
                SortCells();
                _rope.SpawnBall();
            }
        }
        else
        {
            //возврат на верЄвку
            _rope.ConnectBall(newBall);
        }
    }

    private bool CheckIfBallIsNotAddedAlready(Ball ball)
    {
        bool ballExists = false;
        for (int column = 0; column < 3; column++)
        {
            for (int row = 0; row < 3; row++)
            {
                if (_cellsGrid[column,row] == ball)
                {
                    ballExists = true;
                }
            }
        }
        return !ballExists;
    }

    private bool IsColumnFilled(int column)
    {
        int filledCellsCount = 0;
        for (int row = 0; row < 3; row++)
        {
            if (_cellsGrid[column, row] != null)
            {
                filledCellsCount++;
            }
        }
        return filledCellsCount == 3 ? true : false;
    }
    public void AddBall(Ball newBall, int column)
    {
        _cellsGrid[column, 2] = newBall;
    }

    //"—ортировка" таблицы шариков, чтобы шарики начинались с 0 индекса и распологались друг за другом, дл€ имитации гравитации
    private async void SortCells()
    {
        for (int column = 0; column < 3; column++)
        {
            int filledCellsCount = 0;
            for (int row = 0; row < 3; row++)
            {
                if (_cellsGrid[column, row] != null)
                {
                    _cellsGrid[column, filledCellsCount] = _cellsGrid[column, row];
                    if (filledCellsCount != row)
                    {
                        _cellsGrid[column, row] = null;
                    }
                    filledCellsCount++;
                }
            }
        }
        _basketView.UpdateBallPositions(ref _cellsGrid);
        //DebugCells();
        await Task.Delay(1500);
        CheckMatches();
        CheckFillment();

        if (CheckForGapBetweenCells(_cellsGrid))
        {
            SortCells();
        }
    }

    private void CheckMatches()
    {
        Ball[,] previousState = _cellsGrid;

        CheckVerticalMatches();
        CheckHorizontalMatches();
        CheckDiagonalMatches();
    }

    private bool CheckForGapBetweenCells(Ball[,] ballsGrid)
    {
        bool isGapBetweenCells = false;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j < 2 && ballsGrid[i, j] == null && ballsGrid[i, j+1] != null)
                {
                    isGapBetweenCells = true;
                }
            }
        }
        return isGapBetweenCells;
    }

    private void CheckVerticalMatches()
    {
        for (int column = 0; column < 3; column++)
        {
            //„тобы не повтор€ть код - вынес обследование и очистку €чеек в отдельный метод, куда передаютс€ конкретные индексы
            List<(int column, int row)> indexesToObserve = new();
            for (int row = 0; row < 3; row++)
            {
                indexesToObserve.Add((column, row));
            }
            ObserveCellsForMatch(indexesToObserve);

        }
    }
    private void CheckHorizontalMatches()
    {
        for (int row = 0; row < 3; row++)
        {
            List<(int column, int row)> indexesToObserve = new();
            for (int column = 0; column < 3; column++)
            {
                indexesToObserve.Add((column, row));
            }
            ObserveCellsForMatch(indexesToObserve);
        }
    }
    private void CheckDiagonalMatches()
    {
        List<(int column, int row)> indexesToObserveLR = new();
        List<(int column, int row)> indexesToObserveeRL = new();
        for (int index = 0; index < 3; index++)
        {
            indexesToObserveLR.Add((index, index));
            indexesToObserveeRL.Add((index, 2-index));
        }
        ObserveCellsForMatch(indexesToObserveLR);
        ObserveCellsForMatch(indexesToObserveeRL);
    }

    //ќбследуем €чейки по списку их индексов, записанных в кортежи
    private void ObserveCellsForMatch(List<(int col, int row)> indexesToObserve)
    {
        //ќбходим обследуемые €чейки, провер€ем, все ли заполнены
        int filledCellsCount = 0;
        for (int i = 0; i < indexesToObserve.Count; i++)
        {
            if (_cellsGrid[indexesToObserve[i].col, indexesToObserve[i].row] != null)
            {
                filledCellsCount++;
            }
        }
        //≈сли заполнены все - сравниваем типы содержимого в €чейках
        if (filledCellsCount == 3)
        {
            if (_cellsGrid[indexesToObserve[0].col, indexesToObserve[0].row].Data.type == _cellsGrid[indexesToObserve[1].col, indexesToObserve[1].row].Data.type &&
                _cellsGrid[indexesToObserve[1].col, indexesToObserve[1].row].Data.type == _cellsGrid[indexesToObserve[2].col, indexesToObserve[2].row].Data.type)
            {
                //≈сли совпадают - чистим €чейки
                for (int i = 0; i < indexesToObserve.Count; i++)
                {
                    _levelStats.AddPoints(_cellsGrid[indexesToObserve[i].col, indexesToObserve[i].row].Data.price);
                    _cellsGrid[indexesToObserve[i].col, indexesToObserve[i].row].Destroy();
                    _cellsGrid[indexesToObserve[i].col, indexesToObserve[i].row] = null;
                }
            }
        }
    }

    private void CheckFillment()
    {
        bool areAllColumnsFilled = true;
        for (int columns = 0; columns < 3; columns++)
        {
            areAllColumnsFilled = areAllColumnsFilled & IsColumnFilled(columns);
        }
        if (areAllColumnsFilled)
        {
            _levelStats.GameOver();
        }
    }

    private void DebugCells()
    {
        string output = "";
        for (int i = 0; i < _cellsGrid.GetLength(0); i++)
        {
            for (int j = 0; j < _cellsGrid.GetLength(1); j++)
            {
                if (_cellsGrid[i, j] != null)
                {
                    output += _cellsGrid[i, j].Data.type[0].ToString() + ' ';
                }
                else
                {
                    output += "- ";
                }
            }
            output += '\n';
        }
        Debug.Log(output);
    }
}
