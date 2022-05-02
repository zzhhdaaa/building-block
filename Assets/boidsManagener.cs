using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    public GameObject BoidPrefab;
    public int BoidCount = 20;
    public Vector3 Limits = new Vector3(1, 1, 1);
    private GameObject Buildings;

    [HideInInspector]
    public GameObject[] boids;

    [Header("Boid Settings")]
    [Range(0.0f, 5.0f)]
    public float MinSpeed;

    [Range(0.0f, 10.0f)]
    public float MaxSpeed;

    //step 4
    [Range(1.0f, 10.0f)]
    public float NeighborDistance;

    [Range(0.0f, 5.0f)]
    public float RotationSpeed;

    //step 5
    public Vector3 GoalPos;

    //step 1
    // Start is called before the first frame update
    private void Start()
    {
        boids = new GameObject[BoidCount];
        for (int i = 0; i < BoidCount; i++)
        {
            float x = Random.Range(-Limits.x, Limits.x);
            float y = Random.Range(-Limits.y, Limits.y);
            float z = Random.Range(-Limits.z, Limits.z);
            Vector3 pos = Buildings.transform.position + new Vector3(x, y, z);

            boids[i] = Instantiate(BoidPrefab, pos, Quaternion.identity);

            //step 2 fish going forward
            boids[i].GetComponent<Boid>().Manager = this;
        }

        GoalPos = this.transform.position; //step 5
    }

    //step 3
    // Update is called once per frame
    private void Update()
    {
        //GoalPos = this.transform.position; //step 5
        //step 6
        GoalPos = this.transform.position; /*/ step 10
        //step 6
        if (Input.GetMouseButtonDown(0))
        {
            float x = Random.Range(-Limits.x, Limits.x);
            float y = Random.Range(-Limits.y, Limits.y);
            float z = Random.Range(-Limits.z, Limits.z);
            GoalPos = this.transform.position + new Vector3(x, y, z);
        }
        //*/
        for (int i = 0; i < BoidCount; i++)
        {
            //step 3 update boids
            boids[i].GetComponent<Boid>().BoidUpdate();
        }
    }
}