using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGeneratorPrefab : MonoBehaviour
{
    [SerializeField] private MazeCellPrefabScript _mazeCellPrefab;
    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeHeight;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase[] _tiles;

    private MazeCellPrefabScript[,] _mazeGrid;

    IEnumerator Start()
    {
        _mazeGrid = new MazeCellPrefabScript[_mazeWidth, _mazeHeight];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                _mazeGrid[x, y] = Instantiate(_mazeCellPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

        yield return GenerateMaze(null, _mazeGrid[0, 0]);

        ReplacePrefabWithTiles();
    }

    private IEnumerator GenerateMaze(MazeCellPrefabScript previousCell, MazeCellPrefabScript currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        yield return new WaitForSeconds(0.05f);

        MazeCellPrefabScript nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                yield return GenerateMaze(currentCell, nextCell);
            }
        }
        while (nextCell != null);
    }

    private MazeCellPrefabScript GetNextUnvisitedCell(MazeCellPrefabScript currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCellPrefabScript> GetUnvisitedCells (MazeCellPrefabScript currentCell)
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
}
