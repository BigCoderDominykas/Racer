using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text lapsText;
    int lapsAmount = -1;

    Vehicle vehicle;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            vehicle.Accelerate();
        }

        if (Input.GetKey(KeyCode.S))
        {
            vehicle.Brake();
        }

        var horInput = Input.GetAxis("Horizontal");
        if (horInput != 0)
        {
            vehicle.Turn(horInput);
            vehicle.turnDirection = horInput;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lapsAmount++;
        lapsText.text = "Laps: " + lapsAmount;
    }
}
