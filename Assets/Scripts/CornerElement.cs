using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerElement : MonoBehaviour
{
    Coord coord;

    public void Initialize(int setX, int setY, int setZ)
    {
        coord = new Coord(setX, setY, setZ);
        this.name = "CE_" + coord.x + "_" + coord.y + "_" + coord.z;
    }

    public void SetPosition(float setX, float setY, float setZ)
    {
        this.transform.position = new Vector3(setX, setY, setZ);
    }
}
