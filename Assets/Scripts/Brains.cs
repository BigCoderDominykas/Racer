using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brains : MonoBehaviour
{
    int waypointIndex;
    Vector3 nextPoint;

    Vehicle vehicle;
    Path path;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
        path = FindObjectOfType<Path>();
        waypointIndex = path.GetClosestWaypoint(transform.position);
        nextPoint = path.GetWaypointPosition(waypointIndex);
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, nextPoint);
        Debug.DrawLine(transform.position, nextPoint, Color.red);
        if (dist < 3)
        {
            waypointIndex = path.GetNextIndex(waypointIndex);
            nextPoint = path.GetWaypointPosition(waypointIndex);
        }

        var nextPointDir = (nextPoint - transform.position).normalized;
        var dir = Vector3.SignedAngle(transform.forward, nextPointDir, Vector3.up);
        print(dir);
        if (Mathf.Abs(dir) > 15) vehicle.Brake();
        else vehicle.Accelerate();
        vehicle.Turn(Mathf.Sign(dir));
    }
}
