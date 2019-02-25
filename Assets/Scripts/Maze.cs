using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject foodPrefab = null;
    public GameObject wallPrefab = null;
    public GameObject ghostPrefab = null;

    List<Vector2> ghostLocations = new List<Vector2>
    {
        new Vector2(2, 2),
        new Vector2(8, 8)
    };

    public float cellSize = 1;
    // possibly add center x, y coords

    const int WIDTH = 11;
    const int HEIGHT = 11;
    MazeCell[,] cells = new MazeCell[WIDTH, HEIGHT]
    {
        {new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,},  },
        {new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = true, leftWall = true, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = true,}, new MazeCell{growsFood = true, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = true, leftWall = false, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = true,}, },
        {new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = true, bottomWall = false,}, new MazeCell{growsFood = false, leftWall = false, bottomWall = false,}, },
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
                    GameObject food = Instantiate(foodPrefab);
                    food.transform.position = new Vector3(x * cellSize, y * cellSize, 0);
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

        // Instantiate all of the ghosts.
        foreach (Vector2 gl in ghostLocations) {
            GameObject ghost = Instantiate(ghostPrefab);
            ghost.transform.position = new Vector3(gl.x * cellSize, gl.y * cellSize, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
