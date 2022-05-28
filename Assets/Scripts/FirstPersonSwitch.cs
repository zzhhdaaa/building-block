using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonSwitch : MonoBehaviour
{
    public static FirstPersonSwitch instance;
    
    public GameObject thirdPerson;
    public GameObject firstPerson;
    public GameObject cursorCanvas;
    public LevelGenerator levelGenerator;
    private CornerElement[] cornerElements;

    private bool enableCollider = false;
    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Switch();
        }
    }

    public void Switch()
    {
        thirdPerson.SetActive(enableCollider);
        enableCollider = !enableCollider;
        cornerElements = FindObjectsOfType<CornerElement>();
        foreach (CornerElement cornerElement in cornerElements)
        {
            cornerElement.EnableCollider(enableCollider);
        }
        firstPerson.SetActive(enableCollider);
        cursorCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
