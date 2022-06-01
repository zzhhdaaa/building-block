using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    private int Y02;
    private string type01 = "Closed";
    private bool envOn;
    public GameObject env;
    void Start()
    {
        env.SetActive(envOn);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.E))
        {
            envOn = !envOn;
            env.SetActive(envOn);
        }
    }
}
    