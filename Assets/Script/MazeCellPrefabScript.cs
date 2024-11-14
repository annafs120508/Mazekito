using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeCellPrefabScript : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;

    [SerializeField] private GameObject _rightWall;

    [SerializeField] private GameObject _frontWall;

    [SerializeField] private GameObject _backWall;

    [SerializeField] private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }
    public int face { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }

    public void setFace (int newFace)
    {
        face = newFace;
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
        face |= 8;
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
        face |= 2;
        
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
        face |= 1;
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
        face |= 4;
    }
}