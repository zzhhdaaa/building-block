using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance; //singleton

    public int gridX = 5;
    public int gridY = 10;
    public int gridZ = 5;

    public GridElement gridElement;
    public CornerElement cornerElement;

    public GridElement[] gridElements;
    public CornerElement[] cornerElements;

    void Start()
    {
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        cornerElements = new CornerElement[(gridX + 1) * (gridY + 1) * (gridZ + 1)];
        gridElements = new GridElement[gridX * gridY * gridZ];

        //create corner elements
        for (int x = 0; x < gridX + 1; x++)
        {
            for (int y = 0; y < gridY + 1; y++)
            {
                for (int z = 0; z < gridZ + 1; z++)
                {
                    CornerElement cornerElementInstance = Instantiate(cornerElement, Vector3.zero, Quaternion.identity, this.transform);
                    cornerElementInstance.Initialize(x, y, z);
                    cornerElements[x * (gridY + 1) * (gridZ + 1) + y * (gridZ + 1) + z] = cornerElementInstance;
                }
            }
        }

        //create grid elements
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x, y, z), Quaternion.identity, this.transform);
                    gridElementInstance.Initialize(x, y, z);
                    gridElements[x * gridY * gridZ + y * gridZ + z] = gridElementInstance;
                }
            }
        }

        foreach (CornerElement corner in cornerElements)
        {
            corner.SetNearGridElements();
        }
    }

    void Update()
    {
        
    }
}
