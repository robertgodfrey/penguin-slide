using static System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPenguinMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float moveDistance = 0.03f;
    [SerializeField] private float rotationSpeed = 75f;

    [SerializeField] private float minWaitTime = 2f;
    [SerializeField] private float maxWaitTime = 8f;

    private Vector3 startPosition;
    private Transform platformTransform;

    private bool rotate;
    private bool movingUp = true;
    private float waitTime;
    private bool counterclockwise = false;

    void Start()
    {
        startPosition = transform.localPosition;
        platformTransform = transform.parent;

        waitTime = Random.Range(minWaitTime, maxWaitTime);
        System.Random random = new System.Random();
        rotate = random.Next(0, 2) == 0;
    }

    void Update()
    {
        if (waitTime <=0)
        {
            if (rotate)
            {
                StartCoroutine(RotateMotion());
            }
            else
            {
                StartCoroutine(ExcitedMotion());
            }
            waitTime = Random.Range(minWaitTime, maxWaitTime);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private IEnumerator ExcitedMotion()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 targetPosition = movingUp ? startPosition + Vector3.up * moveDistance : startPosition - Vector3.up * moveDistance;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.localPosition == targetPosition)
            {
                movingUp = !movingUp;
            }
            yield return null;
        }
    }

    private IEnumerator RotateMotion()
    {
        for (int i = 0; i < 50; i++)
        {
            if (counterclockwise)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
        counterclockwise = !counterclockwise;
    }
}
