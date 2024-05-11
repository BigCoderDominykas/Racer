using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brains : MonoBehaviour
{
    public float minTurnAngle;
    public Transform target;

    Path path;
    Vehicle vehicle;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
        path = FindObjectOfType<Path>();
        target = path.GetClosestPoint(transform.position);
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < 3)
        {
            target = path.GetNextPoint(transform.position);
        }
        
        Debug.DrawLine(transform.position, target.position, Color.red);
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);

        if (Mathf.Abs(angle) > minTurnAngle)
        {
            vehicle.Turn(angle);
        }
        vehicle.Accelerate();
    }
}
