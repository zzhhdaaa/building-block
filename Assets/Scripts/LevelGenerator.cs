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

    float floorHeight = 0.5f, basementHeight = 1.5f;

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

        float elementHeight;

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
                float yPos = y;

                if (y == 0)
                {
                    elementHeight = floorHeight;
                    yPos = -0.25f;
                }
                else if (y == 1)
                {
                    elementHeight = basementHeight;
                    yPos = 0.75f;
                }
                else
                {
                    elementHeight = 1f;
                }

                for (int z = 0; z < gridZ; z++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x, yPos, z), Quaternion.identity, this.transform);
                    gridElementInstance.Initialize(x, y, z, elementHeight);
                    gridElements[x * gridY * gridZ + y * gridZ + z] = gridElementInstance;
                }
            }
        }

        foreach (CornerElement corner in cornerElements)
        {
            corner.SetNearGridElements();
        }

        foreach (GridElement gridElement in gridElements)
        {
            gridElement.SetEnable();
        }
    }

    void Update()
    {
        
    }
}
