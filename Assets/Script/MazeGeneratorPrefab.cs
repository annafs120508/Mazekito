using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGeneratorPrefab : MonoBehaviour
{
    [SerializeField] private MazeCellPrefabScript _mazeCellPrefab;
    private int _mazeHeight;
    private int _mazeWidth;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase[] _tiles;
    [SerializeField] private GameObject _spritePrefab;  // Sprite yang akan ditempatkan di pojok
    [SerializeField] private GameObject _targetPrefab;  // Target yang akan ditempatkan di pojok atau jalan buntu
    [SerializeField] private GameObject winPanel;       // Panel kemenangan

    private MazeCellPrefabScript[,] _mazeGrid;

    void Start()
    {
        _mazeWidth = PlayerPrefs.GetInt("MazeWidth", _mazeWidth);
        _mazeHeight = PlayerPrefs.GetInt("MazeHeight", _mazeHeight);
        Debug.Log($"Maze width: {_mazeWidth}, Maze height: {_mazeHeight}");

        _mazeGrid = new MazeCellPrefabScript[_mazeWidth, _mazeHeight];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                _mazeGrid[x, y] = Instantiate(_mazeCellPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);
        ReplacePrefabWithTiles();
        PlaceSpriteAndTarget();  // Panggil fungsi untuk menempatkan sprite dan target setelah maze selesai dibuat
    }

    private void GenerateMaze(MazeCellPrefabScript previousCell, MazeCellPrefabScript currentCell)
    {
        Stack<MazeCellPrefabScript> cellStack = new Stack<MazeCellPrefabScript>();
        cellStack.Push(currentCell);

        while (cellStack.Count > 0)
        {
            currentCell = cellStack.Pop();
            currentCell.Visit();

            if (previousCell != null)
            {
                ClearWalls(previousCell, currentCell);
            }

            var unvisitedCells = GetUnvisitedCells(currentCell).ToList();

            if (unvisitedCells.Count > 0)
            {
                previousCell = currentCell;

                // Shuffle unvisited cells randomly
                var nextCell = unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
                cellStack.Push(currentCell);
                cellStack.Push(nextCell);
            }
        }
    }

    private IEnumerable<MazeCellPrefabScript> GetUnvisitedCells(MazeCellPrefabScript currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int y = (int)currentCell.transform.position.y;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, y];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, y];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (y + 1 < _mazeHeight)
        {
            var cellToFront = _mazeGrid[x, y + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (y - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, y - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCellPrefabScript previousCell, MazeCellPrefabScript currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.y < currentCell.transform.position.y)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.y > currentCell.transform.position.y)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    private void ReplacePrefabWithTiles()
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                MazeCellPrefabScript cell = _mazeGrid[x, y];

                TileBase tile = GetTileForFace(cell.face);

                if (tile != null)
                {
                    _tileMap.SetTile(new Vector3Int(x, y, 0), tile);
                }

                Destroy(cell.gameObject);
            }
        }
    }

    private TileBase GetTileForFace(int faceValue)
    {
        if (faceValue >= 0 && faceValue < _tiles.Length)
        {
            return _tiles[faceValue];
        }
        else
        {
            return null;
        }
    }

    private void PlaceSpriteAndTarget()
    {
        List<Vector2Int> corners = new List<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(_mazeWidth - 1, 0),
            new Vector2Int(0, _mazeHeight - 1),
            new Vector2Int(_mazeWidth - 1, _mazeHeight - 1)
        };

        Vector2Int spritePosition = corners[Random.Range(0, corners.Count)];
        Vector3 spriteWorldPosition = _tileMap.CellToWorld(new Vector3Int(spritePosition.x, spritePosition.y, 0)) + _tileMap.cellSize / 2;
        Instantiate(_spritePrefab, spriteWorldPosition, Quaternion.identity);

        corners.Remove(spritePosition);

        Vector2Int targetPosition = corners[Random.Range(0, corners.Count)];
        Vector3 targetWorldPosition = _tileMap.CellToWorld(new Vector3Int(targetPosition.x, targetPosition.y, 0)) + _tileMap.cellSize / 2;

        GameObject target = Instantiate(_targetPrefab, targetWorldPosition, Quaternion.identity);
        target.tag = "Target";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}