using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFloat : MonoBehaviour
{
    public float floatHeight = 0.2f;
    public float floatSpeed = 5f;
    public float rotateSpeed = 30f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        float rotationAmount = rotateSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.up, rotationAmount);
    }
}