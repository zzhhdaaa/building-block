using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public int testInt;

    // Start is called before the first frame update
    void Start()
    {
        //gridElements = LevelGenerator.instance.gridElements;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int x = 0; x < gridX + 1; x++)
            {
                LevelGenerator.instance.gridElements[x * (gridY + 1)].SetDisable();
                for (int y = 0; y < gridY + 1; y++)
                {
                    for (int z = 0; z < gridZ + 1; z++)
                    {
                        //gridElements[z].SetDisable();
                        //x * (gridY + 1) * (gridZ + 1) + y * (gridZ + 1) + z
                    }
                }
            }
        }
        */
        if (Input.GetKeyDown(KeyCode.Return))
        {
            int gridX = LevelGenerator.instance.gridX;
            int gridY = LevelGenerator.instance.gridY;
            int gridZ = LevelGenerator.instance.gridZ;

            for (int i = testInt*gridX*gridY; i < LevelGenerator.instance.gridElements.Length && i < testInt * gridX * gridY + gridX * gridY; i++)
            {
                LevelGenerator.instance.gridElements[i].SetDisable();
            }
        }
    }
}
