using JetBrains.Annotations;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 5f;
    public float deacceleration = 5f;
    public float turnSpeed = 100f;
    public float sideDrag = 0.1f;
    public float drag = 0.1f;
    public int maxExhaustParticles = 30;

    public float speedRatio;
    public float turnDirection;
    public Transform visualsTransform;
    public Transform[] wheels;

    public ParticleSystem driftParticles;

    bool isGrounded;
    Rigidbody rb;
    AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    public void Accelerate()
    {
        if (!isGrounded) return;
        if (rb.velocity.magnitude >= maxSpeed) return;
        rb.velocity += transform.forward * acceleration * Time.deltaTime;
    }

    public void Brake()
    {
        if (!isGrounded) return;
        rb.velocity -= transform.forward * deacceleration * Time.deltaTime;
    }

    public void Turn(float direction)
    {
        if (speedRatio <= 0) return;
        direction = Mathf.Clamp(direction, -1, 1);

        transform.Rotate(0, direction * turnSpeed * Time.deltaTime, 0);
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out var hit, 0.6f);
        var targetAngle = Vector3.SignedAngle(Vector3.up, hit.normal, Vector3.right);
        var newAngle = Mathf.LerpAngle(visualsTransform.localEulerAngles.x, targetAngle, 0.01f);
        visualsTransform.localEulerAngles = new Vector3(newAngle, 0, 0);

        speedRatio = rb.velocity.magnitude / maxSpeed;
        source.pitch = speedRatio * 2 + 1f;

        var sideFriction = Vector3.Dot(transform.right, rb.velocity.normalized);
        rb.velocity += -transform.right * sideFriction * rb.velocity.magnitude * sideDrag * Time.deltaTime;

        var forwardFriction = Vector3.Dot(transform.forward, rb.velocity.normalized);
        rb.velocity += -transform.forward * forwardFriction * rb.velocity.magnitude * drag * Time.deltaTime;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].Rotate(new Vector3(rb.velocity.magnitude * maxSpeed * 50f * Time.deltaTime, 0, 0));
            
            if (i < 2)
            {
                //wheels[i].localEulerAngles = new Vector3(/*wheels[i].localEulerAngles.x*/ 0, Mathf.LerpAngle(wheels[i].localEulerAngles.y, 40 * turnDirection, 0.03f), 0);
                wheels[i].localEulerAngles = new Vector3(/*wheels[i].localEulerAngles.x*/ 0, 40 * turnDirection, 0);
            }
        }

        var emission = driftParticles.emission;
        if (Mathf.Abs(sideFriction) > 0.3f)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
    }
}
