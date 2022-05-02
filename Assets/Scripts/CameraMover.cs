using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Zoom();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        }
    }
    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(new Vector3((LevelGenerator.instance.gridX-1)/2, 0f, (LevelGenerator.instance.gridZ-1)/2), Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(new Vector3((LevelGenerator.instance.gridX-1) / 2, 0f, (LevelGenerator.instance.gridZ-1) / 2), Vector3.up, -rotateSpeed * Time.deltaTime);
        }
    }
    private void Zoom()
    {
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
    }
}
