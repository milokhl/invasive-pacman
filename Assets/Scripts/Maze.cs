using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject pelletPrefab = null;
    public GameObject wallPrefab = null;

    public float cellSize = 1;
    // possibly add center x, y coords

    const int WIDTH = 4;
    const int HEIGHT = 4;
    MazeCell[,] cells = new MazeCell[WIDTH, HEIGHT]
    {
        { new MazeCell{leftWall = true, bottomWall = true}, new MazeCell{leftWall = true, bottomWall = true}, new MazeCell{leftWall = true, bottomWall = false}, new MazeCell{leftWall = false, bottomWall = true, growsFood = false }, },
        { new MazeCell{leftWall = false, bottomWall = true }, new MazeCell{leftWall = true, bottomWall = false }, new MazeCell{leftWall = false, bottomWall = true }, new MazeCell{leftWall = false, bottomWall = true, growsFood = false }, },
        { new MazeCell{leftWall = false, bottomWall = true }, new MazeCell{leftWall = true, bottomWall = false }, new MazeCell{leftWall = false, bottomWall = false }, new MazeCell{leftWall = false, bottomWall = true, growsFood = false }, },
        { new MazeCell{leftWall = true, bottomWall = false, growsFood = false }, new MazeCell{leftWall = true, bottomWall = false, growsFood = false }, new MazeCell{leftWall = true, bottomWall = false, growsFood = false }, new MazeCell{leftWall = false, bottomWall = false, growsFood = false }, },
    };

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < WIDTH; x++) 
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                if (cells[x, y].growsFood)
                {
                    GameObject pellet = Instantiate(pelletPrefab);
                    pellet.transform.position = new Vector3(x * cellSize, y * cellSize, 0);
                }
                if (cells[x, y].leftWall)
                {
                    GameObject leftWall = Instantiate(wallPrefab);
                    leftWall.transform.position = new Vector3((x - 0.5f) * cellSize, y * cellSize, 0);
                    leftWall.transform.Rotate(0, 0, 90);
                }
                if (cells[x, y].bottomWall)
                {
                    GameObject bottomWall = Instantiate(wallPrefab);
                    bottomWall.transform.position = new Vector3(x * cellSize, (y - 0.5f) * cellSize, 0);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
