using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public FlockManager Manager;
    private float Speed;
    private bool turning = false; //step 9

    // Start is called before the first frame update
    private void Start()
    {
        Speed = Random.Range(Manager.MinSpeed, Manager.MaxSpeed);
    }

    // step 2
    /*/
    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * Speed); //z is forward direction
    }
    //*/

    //step 3
    public void BoidUpdate()
    {
        //step 9, determine the bounding box of the manager cube
        Bounds b = new Bounds(Manager.transform.position, Manager.Limits * 2);
        //if boid is outside the bounds of the cube then start turning around
        RaycastHit hit; //step 10
        Vector3 direction = Manager.transform.position - transform.position;
        if (!b.Contains(this.transform.position))
        {
            turning = true;
        }
        else if (Physics.Raycast(this.transform.position, this.transform.forward * 5, out hit)) //step 10
        {
            //
            turning = true;
            //Debug.DrawRay(this.transform.position, this.transform.forward * 50, Color.red);
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
        {
            turning = false;
        }
        if (turning)
        {
            Quaternion quat = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                this.transform.rotation,
                quat,
                Manager.RotationSpeed * Time.deltaTime);
            transform.Translate(0, 0, Time.deltaTime * Speed); //z is forward direction
            return;
        }

        //step 8, random speed/behaviour
        if (Random.Range(0, 100) < 10)
            Speed = Random.Range(Manager.MinSpeed, Manager.MaxSpeed);
        if (Random.Range(0, 100) > 20)
        {
            transform.Translate(0, 0, Time.deltaTime * Speed); //z is forward direction
            return;
        }

        GameObject[] boids;
        boids = Manager.boids;

        Vector3 groupCenter = Vector3.zero;
        float groupSpeed = 0.01f;
        int groupSize = 0; //group within distance
        Vector3 avoid = Vector3.zero;

        //update calculations
        //1. Move toward average position of the group.
        //2. Align with the average heading of the group.
        //3. Avoid crowding other group members.
        foreach (GameObject go in boids)
        {
            if (go == this.gameObject) { continue; } //next, skip

            float distance = Vector3.Distance(go.transform.position, this.transform.position);
            if (distance > Manager.NeighborDistance) { continue; } //next skip

            groupCenter += go.transform.position;
            groupSize++;

            if (distance < 1.0f) //hard coded, how close we can be to another boid before avoiding it
            {
                avoid = avoid + (this.transform.position - go.transform.position) * 2;
            }

            Boid boidScript = go.GetComponent<Boid>();
            groupSpeed += boidScript.Speed;
        }

        if (groupSize > 0)
        {
            //groupCenter = groupCenter / groupSize; //step 3
            groupCenter = groupCenter / groupSize + (Manager.GoalPos - this.transform.position); //step 5
            groupSpeed = groupSpeed / groupSize;

            direction = (groupCenter + avoid) - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion quat = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    this.transform.rotation,
                    quat,
                    Manager.RotationSpeed * Time.deltaTime);
            }
        }

        //move boid
        transform.Translate(0, 0, Time.deltaTime * Speed); //z is forward direction
    }
}