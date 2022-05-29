using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerElement : MonoBehaviour
{
    Coord coord;
    public GridElement[] nearGridElements = new GridElement[8];
    public int bitMaskValue;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    public Material[] materials;
    private float y01;
    // ReSharper disable Unity.PerformanceAnalysis
    public void Initialize(int setX, int setY, int setZ)
    {
        coord = new Coord(setX, setY, setZ);
        this.name = "CE_" + coord.x + "_" + coord.y + "_" + coord.z;
        mesh = this.GetComponent<MeshFilter>();
        meshCollider = this.GetComponent<MeshCollider>();
    }
    public void Update()
    {
        ChangeMaterials();
    }
    public void ChangeMaterials()
    {

        y01 = coord.y % 60;

        if (y01 >= 0 & y01 <= 20)
        {
            this.GetComponent<Renderer>().material = materials[0];
        }
        if (y01 > 20 & y01 <= 40)
        {
            this.GetComponent<Renderer>().material = materials[1];
        }
        if (y01 > 40 & y01 <= 60)
        {
            this.GetComponent<Renderer>().material = materials[2];
        }
    }

    public void SetPosition(float setX, float setY, float setZ)
    {
        this.transform.position = new Vector3(setX, setY, setZ);
    }

    public void SetCornerElement()
    {
        bitMaskValue = BitMask.GetBitMask(nearGridElements);
        mesh.mesh = CornerMeshes.instance.GetCornerMesh(bitMaskValue, coord.y);
        //meshCollider.enabled = false;
        //meshCollider.sharedMesh = mesh.mesh;
    }

    public void EnableCollider(bool enabled)
    {
        if (enabled)
        {
            meshCollider.sharedMesh = mesh.mesh;
            meshCollider.enabled = true;
        }
        else
        {
            meshCollider.enabled = false;
        }
    }

    public void SetNearGridElements()
    {
        int gridX = LevelGenerator.instance.gridX;
        //int gridY = LevelGenerator.instance.gridY;
        int gridZ = LevelGenerator.instance.gridZ;


        //TODO: Game of life add error
        if (coord.x < gridX && coord.y < LevelGenerator.instance.gridY && coord.z < gridZ)
        {
            //UpperNorthEast
            //Debug.Log(this.name);
            nearGridElements[0] = LevelGenerator.instance.gridElements[coord.y * gridZ * gridX + coord.z * gridX + coord.x];
        }
        if (coord.x > 0 && coord.y < LevelGenerator.instance.gridY && coord.z < gridZ)
        {
            //UpperNorthWest
            //Debug.Log(this.name);
            nearGridElements[1] = LevelGenerator.instance.gridElements[coord.y * gridZ * gridX + coord.z * gridX + (coord.x - 1)];
        }
        if (coord.x > 0 && coord.y < LevelGenerator.instance.gridY && coord.z > 0)
        {
            //UpperSouthWest
            //Debug.Log(this.name);
            nearGridElements[2] = LevelGenerator.instance.gridElements[coord.y * gridZ * gridX + (coord.z - 1) * gridX + (coord.x - 1)];
        }
        if (coord.x < gridX && coord.y < LevelGenerator.instance.gridY && coord.z > 0)
        {
            //UpperSouthEast
            //Debug.Log(this.name);
            nearGridElements[3] = LevelGenerator.instance.gridElements[coord.y * gridZ * gridX + (coord.z - 1) * gridX + coord.x];
        }


        if (coord.x < gridX && coord.y > 0 && coord.z < gridZ)
        {
            //LowerNorthEast
            //Debug.Log(this.name);
            nearGridElements[4] = LevelGenerator.instance.gridElements[(coord.y - 1) * gridZ * gridX + coord.z * gridX + coord.x];
        }
        if (coord.x > 0 && coord.y > 0 && coord.z < gridZ)
        {
            //LowerNorthWest
            //Debug.Log(this.name);
            nearGridElements[5] = LevelGenerator.instance.gridElements[(coord.y - 1) * gridZ * gridX + coord.z * gridX + (coord.x - 1)];
        }
        if (coord.x > 0 && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthWest
            //Debug.Log(this.name);
            nearGridElements[6] = LevelGenerator.instance.gridElements[(coord.y - 1) * gridZ * gridX + (coord.z - 1) * gridX + (coord.x - 1)];
        }
        if (coord.x < gridX && coord.y > 0 && coord.z > 0)
        {
            //LowerSouthEast
            //Debug.Log(this.name);
            nearGridElements[7] = LevelGenerator.instance.gridElements[(coord.y - 1) * gridZ * gridX + (coord.z - 1) * gridX + coord.x];
        }
    }
}
