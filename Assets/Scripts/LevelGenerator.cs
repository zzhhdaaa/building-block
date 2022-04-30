using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    public int gridX = 5;
    public int gridY = 10;
    public int gridZ = 5;
    public GridElement gridElement;
    public GridElement[] gridElements;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        gridElements = new GridElement[gridX * gridY * gridZ];

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    GridElement gridElementInstance = Instantiate(gridElement, new Vector3(x, y, z), Quaternion.identity, this.transform);
                    gridElementInstance.Initialize(x, y, z);
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
