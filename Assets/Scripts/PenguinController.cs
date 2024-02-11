using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    public float horizontalSpeed = 5f; // Horizontal movement speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInputSpeed = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.forward * horizontalInputSpeed * horizontalSpeed, ForceMode.VelocityChange);

        if (new Vector3(0, 0, rb.velocity.z).magnitude > 10.0f)
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z).normalized * 10.0f + new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }
    }
}

