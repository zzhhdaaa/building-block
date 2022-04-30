using System;
using UnityEngine;
using System.Collections;
public class gamecontrol : MonoBehaviour
{
    public Transform target;
    public Camera cameraObj;
    public float minDistance;
    public float maxDistance;
    private float rotationSpeed = 2f;
    private bool rightIsActivated;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
        transform.LookAt(target);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rightIsActivated = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightIsActivated = false;
        }

        if (rightIsActivated)
        {
            cameraObj.transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
            cameraObj.transform.RotateAround(target.transform.position, Vector3.right, -Input.GetAxis("Mouse Y") * rotationSpeed);
            transform.LookAt(target);
            offset = transform.position - target.transform.position;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            cameraObj.fieldOfView -= 8 * Input.GetAxis("Mouse ScrollWheel");
            cameraObj.fieldOfView = Mathf.Clamp(cameraObj.fieldOfView, minDistance, maxDistance);
        }
    }

    void LateUpdate()
    {
        if (!rightIsActivated)
            transform.position = target.transform.position + offset;
    }
}