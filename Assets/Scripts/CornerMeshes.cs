using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMeshes : MonoBehaviour
{
    public static CornerMeshes instance;
    public GameObject mesh;

    Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();

    void Awake()
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
        Initialize();
    }

    private void Initialize()
    {
        foreach (Transform child in mesh.transform)
        {
            meshes.Add(child.name, child.GetComponent<MeshFilter>().sharedMesh);
        }
    }

    public Mesh GetCornerMesh(int bitMask, int level)
    {
        Mesh result;

        if (level > 1)
        {
            if (meshes.TryGetValue(bitMask.ToString(), out result))
            {
                return result;
            }
        }
        else if (level == 0)
        {
            if (meshes.TryGetValue(0 + "_" + bitMask.ToString(), out result))
            {
                return result;
            }
        }
        else if (level == 1)
        {
            if (meshes.TryGetValue(1 + "_" + bitMask.ToString(), out result))
            {
                return result;
            }
        }

        return null;
    }
}
