using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    int gridX = 5;
    int gridY = 10;
    int gridZ = 5;
    public GridElement gridElement;
    public GridElement[] gridElements;

    // Start is called before the first frame update
    void Start()
    {
        gridElements = new GridElement[gridX * gridY * gridZ];

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x, y, z), Quaternion.identity, this.transform);
                    gridElementInstance.name = x + "_" + y + "_" + z;
                    gridElements[x * gridY *gridZ + y * gridZ + z] = gridElementInstance;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
