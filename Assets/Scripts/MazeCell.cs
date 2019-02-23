using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell
{
    public bool leftWall = false;
    public bool bottomWall = false;
    public bool growsFood = true;

    public float foodlevel = 1f;
}
