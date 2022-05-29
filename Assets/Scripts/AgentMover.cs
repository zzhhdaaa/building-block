using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    private bool moveOn;
    public float speed;
    public float turnTime;
    private float timer;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            moveOn = !moveOn;
        }
        if (moveOn)
        {
            Timer();
        }
    }

    void Timer()
    {
        if (timer < turnTime)
        {
            timer += Time.deltaTime;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            timer = 0f;
            direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        }
    }
}
