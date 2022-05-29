using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    private int Y02;
    private string type01 = "Closed";
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (type01 == "Closed")
        {
            if (Input.GetMouseButtonDown(2))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                type01 = "Open";
            }

        }
        if (type01 == "Open")
        {
            EnvironmentMove();

            if (Input.GetMouseButtonDown(2))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                type01 = "Closed";
            }
        }
        
    }
    public void EnvironmentMove()
    {
        Y02 = (int)GameObject.FindWithTag("Player").transform.position.y / 60;
        Debug.Log(Y02);
        gameObject.transform.position = new Vector3(0, Y02 * 60, 0);
    }
}
    