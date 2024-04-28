using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brains : MonoBehaviour
{
    Vehicle vehicle;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    private void Update()
    {
        vehicle.Turn(Mathf.PerlinNoise1D(Time.time) * 2 - 1);
        vehicle.Accelerate();
    }
}
