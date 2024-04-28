using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 100f;
    Rigidbody rb;
    AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    public void Accelerate()
    {
        rb.velocity += transform.forward * speed * Time.deltaTime;
    }

    public void Brake()
    {
        rb.velocity -= transform.forward * speed * Time.deltaTime;
    }

    public void Turn(float direction)
    {
        transform.Rotate(0, direction * turnSpeed * Time.deltaTime, 0);
    }

    private void Update()
    {
        print(rb.velocity.magnitude * 3.6f + "km/h");
        source.pitch = rb.velocity.magnitude / speed * 2 + 1f;
    }
}
