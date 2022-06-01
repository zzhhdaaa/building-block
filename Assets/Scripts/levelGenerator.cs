using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance; //singleton

    public int gridX = 5;
    public int gridY = 10;
    public int gridZ = 5;

    public int gridShift = 2;

    public GridElement gridElement;
    public CornerElement cornerElement;

    //public GridElement[] gridElements;
    //public CornerElement[] cornerElements;
    public List<GridElement> gridElements;
    public List<CornerElement> cornerElements;

    float floorHeight = 0.25f, basementHeight = 1f;

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

        gridElements = new List<GridElement>();
        cornerElements = new List<CornerElement>();
        //cornerElements = new CornerElement[(gridX + 1) * (gridY + 1) * (gridZ + 1)];
        //gridElements = new GridElement[gridX * gridY * gridZ];

        //create corner elements
        for (int y = 0; y < gridY + 1; y++)
        {
            for (int z = 0; z < gridZ + 1; z++)
            {
                for (int x = 0; x < gridX + 1; x++)
                {
                    CornerElement cornerElementInstance = Instantiate(cornerElement, Vector3.zero, Quaternion.identity, this.transform);
                    cornerElementInstance.Initialize(x, y, z);
                    //cornerElements[y * (gridZ + 1) * (gridX + 1) + z * (gridX + 1) + x] = cornerElementInstance;
                    cornerElements.Add(cornerElementInstance);
                }
            }
        }

        //create grid elements
        for (int y = 0; y < gridY; y++)
        {
            float yPos = y;

            if (y == 0)
            {
                elementHeight = floorHeight;
                yPos = -0.375f;
            }
            else if (y == 1)
            {
                elementHeight = basementHeight;
                yPos = 0.625f;
            }
            else
            {
                elementHeight = 1f;
            }

            for (int z = 0; z < gridZ; z++)
            {
                for (int x = 0; x < gridX; x++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x, yPos, z), Quaternion.identity, this.transform);
                    gridElementInstance.Initialize(x, y, z, elementHeight);
                    //gridElements[y * gridZ * gridX + z * gridX + x] = gridElementInstance;
                    gridElements.Add(gridElementInstance);
                }
            }
        }

        foreach (CornerElement corner in cornerElements)
        {
            corner.SetNearGridElements();
        }

        foreach (GridElement grid in gridElements)
        {
            grid.SetDisable();
        }

        for (int y = 0; y < 1; y++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                for (int x = 0; x < gridX; x++)
                {
                    gridElements[y * gridZ * gridX + z * gridX + x].SetEnable();
                }
            }
        }
        for (int y = 1; y < gridY; y++)
        {
            for (int z = gridShift; z < gridZ - gridShift; z++)
            {
                for (int x = gridShift; x < gridX - gridShift; x++)
                {
                    if (Random.Range(0f, 1f) > 0.3f)
                    {
                        gridElements[y * gridZ * gridX + z * gridX + x].SetEnable();
                    }
                }
            }
        }
    }
}
