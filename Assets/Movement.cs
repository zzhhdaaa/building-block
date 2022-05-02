using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // Start is called before the first frame update  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Camera.main.transform.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Camera.main.transform.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Camera.main.transform.forward * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Camera.main.transform.forward * Time.deltaTime * 10;
        }
    }
}