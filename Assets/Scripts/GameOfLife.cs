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
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            int gridX = LevelGenerator.instance.gridX;
            int gridY = LevelGenerator.instance.gridY;
            int gridZ = LevelGenerator.instance.gridZ;

            for (int i = testInt*gridX*gridZ; i < LevelGenerator.instance.gridElements.Count && i < (testInt + 1) * gridX * gridZ; i++)
            {
                LevelGenerator.instance.gridElements[i].SetDisable();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            LevelGenerator.instance.gridY += 1;
            
            for (int z = 0; z < LevelGenerator.instance.gridZ + 1; z++)
            {
                for (int x = 0; x < LevelGenerator.instance.gridX + 1; x++)
                {
                    CornerElement cornerElementInstance = Instantiate(LevelGenerator.instance.cornerElement, Vector3.zero, Quaternion.identity, LevelGenerator.instance.transform);
                    cornerElementInstance.Initialize(x, LevelGenerator.instance.gridY, z);
                    LevelGenerator.instance.cornerElements.Add(cornerElementInstance);
                }
            }
            for (int z = 0; z < LevelGenerator.instance.gridZ; z++)
            {
                for (int x = 0; x < LevelGenerator.instance.gridX; x++)
                {
                    GridElement gridElementInstance = Instantiate(LevelGenerator.instance.gridElement, new Vector3(x, LevelGenerator.instance.gridY-1, z), Quaternion.identity, LevelGenerator.instance.transform);
                    gridElementInstance.Initialize(x, LevelGenerator.instance.gridY-1, z, 1f);
                    LevelGenerator.instance.gridElements.Add(gridElementInstance);
                }
            }
            /*
            for (int z = 0; z < LevelGenerator.instance.gridZ + 1; z++)
            {
                for (int x = 0; x < LevelGenerator.instance.gridX + 1; x++)
                {
                    LevelGenerator.instance.cornerElements[LevelGenerator.instance.gridY * (LevelGenerator.instance.gridZ + 1) * (LevelGenerator.instance.gridX + 1) + z * (LevelGenerator.instance.gridX + 1) + x].SetNearGridElements();
                }
            }
            */

            foreach (CornerElement corner in LevelGenerator.instance.cornerElements)
            {
                corner.SetNearGridElements();
            }

            for (int z = 0; z < LevelGenerator.instance.gridZ; z++)
            {
                for (int x = 0; x < LevelGenerator.instance.gridX; x++)
                {
                    LevelGenerator.instance.gridElements[(LevelGenerator.instance.gridY-1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + z * LevelGenerator.instance.gridX + x].SetEnable();
                }
            }

            /*
            foreach (GridElement gridElement in LevelGenerator.instance.gridElements)
            {
                gridElement.SetEnable();
            }
            */
        }
    }
}
