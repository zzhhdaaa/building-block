using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coord
{
    public int x, y, z;

    public Coord(int setX, int setY, int setZ)
    {
        x = setX;
        y = setY;
        z = setZ;
    }
}

public class GridElement : MonoBehaviour
{
    Coord coord;
    Collider col;
    Renderer rend;

    public void Initialize(int setX, int setY, int setZ)
    {
        coord = new Coord(setX, setY, setZ);
        this.name = this.coord.x + "_" + this.coord.y + "_" + this.coord.z;
        this.col = this.GetComponent<Collider>();
        this.rend = this.GetComponent<Renderer>();
    }

    public Coord GetCoord()
    {
        return coord;
    }

    public void SetEnable()
    {
        this.col.enabled = true;
        this.rend.enabled = true;
    }

    public void SetDisable()
    {
        this.col.enabled = false;
        this.rend.enabled = false;
    }
}
