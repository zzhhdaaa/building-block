using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public int minComfort, maxComfort;

    [Range(0.0f, 1.0f)]
    public float randomShift;

    private bool livingOn;

    void Start()
    {
        //gridElements = LevelGenerator.instance.gridElements;
    }

    void Update()
    {
        if (LevelGenerator.instance.gridY < 60)
        {
            //give it life!
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GiveItLife();
            }
        }

        //living objects on or off
        if (Input.GetKeyDown(KeyCode.L))
        {
            livingOn = !livingOn;
        }
    }

    //game of life algorithm
    List<bool> CalculateLife(List<bool> lastLife, int gridX, int gridZ)
    {
        List<bool> nextLife = new List<bool>();

        for (int z = 0; z < gridZ; z++)
        {
            for (int x = 0; x < gridX; x++)
            {
                int lifeCount = 0;
                int n0 = (z * gridX + x) - gridX - 1;
                int n1 = (z * gridX + x) - gridX;
                int n2 = (z * gridX + x) - gridX + 1;
                int n3 = (z * gridX + x) - 1;
                int n4 = (z * gridX + x);
                int n5 = (z * gridX + x) + 1;
                int n6 = (z * gridX + x) + gridX - 1;
                int n7 = (z * gridX + x) + gridX;
                int n8 = (z * gridX + x) + gridX + 1;
                /*
                int n0 = (z * gridX + x) - gridX - 1;
                int n1 = (z * gridX + x) - gridZ;
                int n2 = (z * gridX + x) - gridZ + 1;
                int n3 = (z * gridX + x) - 1;
                int n4 = (z * gridX + x);
                int n5 = (z * gridX + x) + 1;
                int n6 = (z * gridX + x) + gridX - 1;
                int n7 = (z * gridX + x) + gridZ;
                int n8 = (z * gridX + x) + gridZ + 1;
                */

                //
                if (n0 >= 0 && n0 < lastLife.Count)
                {
                    if (lastLife[n0])
                    {
                        lifeCount += 1;
                    }
                }
                else if(Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n1 >= 0 && n1 < lastLife.Count)
                {
                    if (lastLife[n1])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n2 >= 0 && n2 < lastLife.Count)
                {
                    if (lastLife[n2])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n3 >= 0 && n3 < lastLife.Count)
                {
                    if (lastLife[n3])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n4 >= 0 && n4 < lastLife.Count)
                {
                    if (lastLife[n4])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n5 >= 0 && n5 < lastLife.Count)
                {
                    if (lastLife[n5])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n6 >= 0 && n6 < lastLife.Count)
                {
                    if (lastLife[n6])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n7 >= 0 && n7 < lastLife.Count)
                {
                    if (lastLife[n7])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                if (n8 >= 0 && n8 < lastLife.Count)
                {
                    if (lastLife[n8])
                    {
                        lifeCount += 1;
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < randomShift)
                {
                    lifeCount += 1;
                }

                print(lifeCount);
                if (lifeCount >= minComfort && lifeCount <= maxComfort)
                {
                    nextLife.Add(true);
                }
                else
                {
                    nextLife.Add(false);
                }
            }
        }

        return nextLife;
    }

    void AddBuildingHeight()
    {
        //building height +1
        LevelGenerator.instance.gridY += 1;
        //add corner elements
        for (int z = 0; z < LevelGenerator.instance.gridZ + 1; z++)
        {
            for (int x = 0; x < LevelGenerator.instance.gridX + 1; x++)
            {
                CornerElement cornerElementInstance = Instantiate(LevelGenerator.instance.cornerElement, Vector3.zero, Quaternion.identity, LevelGenerator.instance.transform);
                cornerElementInstance.Initialize(x, LevelGenerator.instance.gridY, z);
                LevelGenerator.instance.cornerElements.Add(cornerElementInstance);
            }
        }
        //add grid elements
        for (int z = 0; z < LevelGenerator.instance.gridZ; z++)
        {
            for (int x = 0; x < LevelGenerator.instance.gridX; x++)
            {
                GridElement gridElementInstance = Instantiate(LevelGenerator.instance.gridElement, new Vector3(x, LevelGenerator.instance.gridY - 1, z), Quaternion.identity, LevelGenerator.instance.transform);
                gridElementInstance.Initialize(x, LevelGenerator.instance.gridY - 1, z, 1f);
                LevelGenerator.instance.gridElements.Add(gridElementInstance);
            }
        }
        //set near grid elements in each corner element
        foreach (CornerElement corner in LevelGenerator.instance.cornerElements)
        {
            corner.SetNearGridElements();
        }
    }

    public void GiveItLife()
    {
        List<bool> lastLife = new List<bool>();
        List<bool> nextLife = new List<bool>();

        //get the lastLife status
        for (int z = 0; z < LevelGenerator.instance.gridZ; z++)
        {
            for (int x = 0; x < LevelGenerator.instance.gridX; x++)
            {
                lastLife.Add(LevelGenerator.instance.gridElements[(LevelGenerator.instance.gridY - 1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + z * LevelGenerator.instance.gridX + x].GetEnabled());
                print(lastLife[LevelGenerator.instance.gridZ * z + x]);
            }
        }

        nextLife = CalculateLife(lastLife, LevelGenerator.instance.gridX, LevelGenerator.instance.gridZ);
        AddBuildingHeight();

        for (int z = 0; z < LevelGenerator.instance.gridZ; z++)
        {
            for (int x = 0; x < LevelGenerator.instance.gridX; x++)
            {
                if (nextLife[LevelGenerator.instance.gridZ * z + x])
                {
                    LevelGenerator.instance.gridElements[(LevelGenerator.instance.gridY - 1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + z * LevelGenerator.instance.gridX + x].SetEnable();
                }
                else
                {
                    LevelGenerator.instance.gridElements[(LevelGenerator.instance.gridY - 1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + z * LevelGenerator.instance.gridX + x].SetDisable();
                    if (livingOn)
                    {
                        LevelGenerator.instance.gridElements[(LevelGenerator.instance.gridY - 1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + z * LevelGenerator.instance.gridX + x].CreateLife();
                    }
                }
            }
        }
    }
}
