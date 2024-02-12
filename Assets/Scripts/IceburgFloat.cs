using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceburgFloat : MonoBehaviour
{
    public float floatHeight = 0.1f;
    public float floatSpeed = 2f;
    public float wobbleAmount = 0.1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        float wobble = Mathf.Sin(Time.time * floatSpeed) * wobbleAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.right, wobble);
    }
}