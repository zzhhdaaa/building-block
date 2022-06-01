using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableCamera : MonoBehaviour
{
    public Transform origin;
    private List<Vector3> upDirs;
    // Start is called before the first frame update
    void Start()
    {
        upDirs = new List<Vector3>
        {
            new Vector3(1, 0, 0).normalized,
            new Vector3(0, 1, 0).normalized,
            new Vector3(0,0,1).normalized,
            new Vector3(-1, 0, 0).normalized,
            new Vector3(0, -1, 0).normalized,
            new Vector3(0,0,-1).normalized
        };
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 trueUpDir = UpDirection();
        transform.position = origin.position;
        //print(origin.localRotation.y);
        //transform.up = Vector3.Lerp(transform.up, trueUpDir, Time.deltaTime * 3);
        //transform.forward = Vector3.ProjectOnPlane(origin.forward, trueUpDir).normalized;
        transform.LookAt(origin.position+Vector3.ProjectOnPlane(origin.forward, trueUpDir).normalized, trueUpDir);
    }

    Vector3 UpDirection()
    {
        Vector3 trueUpDir = new Vector3(0,0,0);
        float max = 0f;
        foreach (Vector3 upDir in upDirs)
        {
            float cal = (upDir + origin.up.normalized).magnitude;
            if (cal > max)
            {
                max = cal;
                trueUpDir = upDir;
            }
        }
        return Vector3.Lerp(transform.up, trueUpDir, Time.deltaTime * 3f);
    }
}
