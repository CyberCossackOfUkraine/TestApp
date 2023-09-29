using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell _mazeCellPrefab;
    [SerializeField] private Transform _cellsParent;


    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeDepth;

    [SerializeField] [Range(0f, 1f)] private float _deathZoneChance;

    private MazeCell[,] _mazeGrid;
    public Transform lastCell;

    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        PopulateGrid();

        SetZones();

        GenerateMaze(null, _mazeGrid[0, 0]);

    }

    private void PopulateGrid()
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                _mazeGrid[x, z].transform.SetParent(_cellsParent);

            }
        }

        lastCell = _mazeGrid[_mazeWidth - 1, _mazeDepth - 1].gameObject.transform;

    }

    private void SetZones()
    {
        _mazeGrid[_mazeWidth - 1, _mazeDepth - 1].SetFinish();

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                if (x == 0 && z == 0) continue;

                float chance = Random.Range(0f, 1f);

                if (chance <= _deathZoneChance)
                {
                    _mazeGrid[x, z].SetDeathZone();
                }

            }
        }
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();

    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth) // Right cell
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (!cellToRight.isVisited)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0) // Left cell
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (!cellToLeft.isVisited)
            {
                yield return cellToLeft;
            }
        }
        if (z + 1 < _mazeDepth) // Front cell
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (!cellToFront.isVisited)
            {
                yield return cellToFront;
            }
        }
        if (z - 1 >= 0) // Back cell
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (!cellToBack.isVisited)
            {
                yield return cellToBack;
            }
        }

    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null) return;

        if (previousCell.transform.position.x < currentCell.transform.position.x) // previous cell is on the left
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x) // previous cell is on the right
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z) // previous cell is on the back
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        if (previousCell.transform.position.z > currentCell.transform.position.z) // previous cell is on the front
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}
