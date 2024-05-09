using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public Vehicle vehicle;
    public Transform needle;
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();    
    }

    private void LateUpdate()
    {
        text.text = (vehicle.GetComponent<Rigidbody>().velocity.magnitude * 3.6f).ToString("000") + " km/h";
        needle.localEulerAngles = new Vector3 (0, 0, Mathf.LerpAngle(needle.localEulerAngles.z, vehicle.GetComponent<Rigidbody>().velocity.magnitude / vehicle.maxSpeed * -110 * 2 + 110, 0.01f));
    }
}
