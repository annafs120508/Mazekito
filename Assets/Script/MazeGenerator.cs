using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeHeight;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase[] _tiles;
    [SerializeField] private GameObject _mazeCellPrefab;

    private MazeCell[,] _mazeGrid;

    private void Start()
    {

        _mazeGrid = new MazeCell[_mazeWidth, _mazeHeight];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                GameObject mazeCellObj = Instantiate(_mazeCellPrefab, new Vector3(x, y, 0), Quaternion.identity);
                MazeCell mazeCell = mazeCellObj.GetComponent<MazeCell>();

                if (mazeCell != null)
                {
                    _mazeGrid[x, y] = mazeCell;
                }
                else
                {
                    Debug.LogError("MazeCell Component is Missing From Prefab");
                }
            }
        }

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                _tileMap.SetTile(new Vector3Int(x, y, 0), _tiles[0]);
            }
        }

        StartCoroutine(GenerateMaze());
    }

    private IEnumerator GenerateMaze()
    {
        MazeCell startCell = _mazeGrid[0, 0];
        yield return GenerateMazeRecursive(null, startCell);

        GenerateGridFaces();
    }

    private IEnumerator GenerateMazeRecursive(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWall(previousCell, currentCell);

        yield return new WaitForSeconds(0.05f);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                yield return GenerateMazeRecursive(currentCell, nextCell);
            }
        }
        while (nextCell != null);
    }

    private void GenerateGridFaces()
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                int faceValue = 0;

                if (isCellSolid(x, y + 1)) faceValue |= 1;
                if (isCellSolid(x + 1, y)) faceValue |= 2;
                if (isCellSolid(x, y - 1)) faceValue |= 4;
                if (isCellSolid(x - 1, y)) faceValue |= 8;

                TileBase tile = GetTileForFace(faceValue);

                if (tile != null)
                {
                    _tileMap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }

    private bool isCellSolid(int x, int y)
    {
        if (x >= 0 && x < _mazeWidth && y >= 0 && y < _mazeHeight)
        {
            return _mazeGrid[x, y].isVisited;
        }
        return false;
    }

    private void UpdateTileAtPosition(Vector3Int position)
    {
        int tileIndex = 0;

        if (hasNeighbor(position.x, position.y + 1))
        {
            tileIndex += 1;
        }

        if (hasNeighbor(position.x + 1, position.y))
        {
            tileIndex += 2;
        }

        if (hasNeighbor(position.x, position.y - 1))
        {
            tileIndex += 4;
        }

        if (hasNeighbor(position.x - 1, position.y))
        {
            tileIndex += 8;
        }

        TileBase tile = GetTileForFace(tileIndex);

        if (tile != null)
        {
            _tileMap.SetTile(position, tile);
        }
    }

    private bool hasNeighbor(int x, int y)
    {
        Vector3Int neighborPosition = new Vector3Int(x, y, 0);

        return _tileMap.HasTile(neighborPosition);
    }

    private TileBase GetTileForFace(int faceValue)
    {
        if (faceValue >= 0 && faceValue < _tiles.Length)
        {
            return _tiles[faceValue];
        }
        else
        {
            Debug.LogError($"Face value {faceValue} is out of bounds for the _tiles array.");
            return null;
        }
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedNeighbors(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private List<MazeCell> GetUnvisitedNeighbors(MazeCell cell)
    {
        List<MazeCell> neighbors = new List<MazeCell>();

        int x = (int)cell.transform.position.x;
        int y = (int)cell.transform.position.y;

        if (x + 1 < _mazeWidth && !_mazeGrid[x + 1, y].isVisited) neighbors.Add(_mazeGrid[x + 1, y]);
        if (x - 1 >= 0 && !_mazeGrid[x - 1, y].isVisited) neighbors.Add(_mazeGrid[x - 1, y]);
        if (y + 1 < _mazeHeight && !_mazeGrid[x, y + 1].isVisited) neighbors.Add(_mazeGrid[x, y + 1]);
        if (y - 1 >= 0 && !_mazeGrid[x, y - 1].isVisited) neighbors.Add(_mazeGrid[x, y - 1]);

        return neighbors;
    }

    private void ClearWall(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        Vector3Int previousPosition = new Vector3Int((int)previousCell.transform.position.x, (int)previousCell.transform.position.y, 0);
        Vector3Int currentPosition = new Vector3Int((int)currentCell.transform.position.x, (int)currentCell.transform.position.y, 0);

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            _tileMap.SetTile(new Vector3Int(previousPosition.x + 1, previousPosition.y, 0), null);
            return;
        }
    }
}
