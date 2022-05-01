using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerElement : MonoBehaviour
{
    Coord coord;
    public GridElement[] nearGridElements = new GridElement[8];
    public int bitMaskValue;
    private MeshFilter mesh;

    public void Initialize(int setX, int setY, int setZ)
    {
        coord = new Coord(setX, setY, setZ);
        this.name = "CE_" + coord.x + "_" + coord.y + "_" + coord.z;
        mesh = this.GetComponent<MeshFilter>();
    }

    public void SetPosition(float setX, float setY, float setZ)
    {
        this.transform.position = new Vector3(setX, setY, setZ);
    }

    public void SetCornerElement()
    {
        bitMaskValue = BitMask.GetBitMask(nearGridElements);
        mesh.mesh = CornerMeshes.instance.GetCornerMesh(bitMaskValue, coord.y);
    }

    public void SetNearGridElements()
    {
        int gridX = LevelGenerator.instance.gridX;
        int gridY = LevelGenerator.instance.gridY;
        int gridZ = LevelGenerator.instance.gridZ;

        if (coord.x < gridX && coord.y < gridY && coord.z < gridZ)
        {
            //UpperNorthEast
            //Debug.Log(this.name);
            nearGridElements[0] = LevelGenerator.instance.gridElements[coord.x * gridY * gridZ + coord.y * gridZ + coord.z];
        }
        if (coord.x > 0 && coord.y < gridY && coord.z < gridZ)
        {
            //UpperNorthWest
            //Debug.Log(this.name);
            nearGridElements[1] = LevelGenerator.instance.gridElements[(coord.x - 1) * gridY * gridZ + coord.y * gridZ + coord.z];
        }
        if (coord.x > 0 && coord.y < gridY && coord.z > 0)
        {
            //UpperSouthWest
            //Debug.Log(this.name);
            nearGridElements[2] = LevelGenerator.instance.gridElements[(coord.x - 1) * gridY * gridZ + coord.y * gridZ + (coord.z - 1)];
        }
        if (coord.x < gridX && coord.y < gridY && coord.z > 0)
        {
            //UpperSouthEast
            //Debug.Log(this.name);
            nearGridElements[3] = LevelGenerator.instance.gridElements[coord.x * gridY * gridZ + coord.y * gridZ + (coord.z - 1)];
        }


        if (coord.x < gridX && coord.y > 0 && coord.z < gridZ)
        {
            //LowerNorthEast
            //Debug.Log(this.name);
            nearGridElements[4] = LevelGenerator.instance.gridElements[coord.x * gridY * gridZ + (coord.y - 1) * gridZ + coord.z];
        }
        if (coord.x > 0 && coord.y > 0 && coord.z < gridZ)
        {
            //LowerNorthWest
            //Debug.Log(this.name);
            nearGridElements[5] = LevelGenerator.instance.gridElements[(coord.x - 1) * gridY * gridZ + (coord.y - 1) * gridZ + coord.z];
        }
        if (coord.x > 0 && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthWest
            //Debug.Log(this.name);
            nearGridElements[6] = LevelGenerator.instance.gridElements[(coord.x - 1) * gridY * gridZ + (coord.y - 1) * gridZ + (coord.z - 1)];
        }
        if (coord.x < gridX && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthEast
            //Debug.Log(this.name);
            nearGridElements[7] = LevelGenerator.instance.gridElements[coord.x * gridY * gridZ + (coord.y - 1) * gridZ + (coord.z - 1)];
        }
    }
}
