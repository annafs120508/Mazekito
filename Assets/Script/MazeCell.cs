using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public bool isVisited { get; private set; }
    public int face;

    public void Visit()
    {
        isVisited = true;
    }

    public void setFace(int newFace)
    {
        face = newFace;
    }
}