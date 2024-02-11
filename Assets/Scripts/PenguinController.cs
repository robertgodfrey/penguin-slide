using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    public float minFriction = 0;
    public float baseFriction = 0.15f;
    public float maxFriction = 0.5f;
    public float horizontalSpeed = 2f; // Horizontal movement speed
    private Collider playerCollider;
    private PhysicMaterial physicsMaterial;
    private Rigidbody rb;

    void Start()
    {
        playerCollider = GetComponent<Collider>();
        physicsMaterial = playerCollider.material;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float verticalInputSpeed = Input.GetAxis("Vertical");
        float horizontalInputSpeed = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.forward * horizontalInputSpeed * horizontalSpeed, ForceMode.VelocityChange);

        if (verticalInputSpeed == 0)
        {
            // set friction to base
            physicsMaterial.dynamicFriction = baseFriction;
        }
        else if (verticalInputSpeed > 0)
        {
            // go faster, set friction to min
            physicsMaterial.dynamicFriction = minFriction;
        }
        else
        {
            // go slower, set friction to max
            physicsMaterial.dynamicFriction = maxFriction;
        }

        if (new Vector3(0, 0, rb.velocity.z).magnitude > 3.0f)
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z).normalized * 3.0f + new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }
    }
}

