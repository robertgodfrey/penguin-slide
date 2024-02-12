using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PenguinController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishCountText;

    [Header("Health Bar")]
    [SerializeField] private Image healthBarBg;
    [SerializeField] private Image healthBarInner;
    [SerializeField] private Sprite healthBarBgWhite;
    [SerializeField] private Sprite healthBarBgRed;
    
    private Rigidbody rb;
    private Collider playerCollider;
    private PhysicMaterial physicsMaterial;
    private float previousPositionX;

    private int playerHealth = 100;
    private int fishCount = 0;
    private int flashNum = 0; // to delay health bar flashing
    private int hitRock = 0; // provides delay to prevent multiple hits on same rock

    void Start()
    {
        playerCollider = GetComponent<Collider>();
        physicsMaterial = playerCollider.material;
        rb = GetComponent<Rigidbody>();
        previousPositionX = transform.position.x;
    }

    void FixedUpdate()
    {
        float distanceTraveledX = Math.Abs(transform.position.x - previousPositionX);
        float currentSpeedX = distanceTraveledX / Time.deltaTime;
        previousPositionX = transform.position.x;

        // left/right controls
        float horizontalInputSpeed = Input.GetAxis("Horizontal");

        if (horizontalInputSpeed != 0)
        {
            rb.AddForce(Vector3.forward * horizontalInputSpeed, ForceMode.VelocityChange);
            physicsMaterial.dynamicFriction = 0.25f;

            float rotationX = horizontalInputSpeed * 60f * Time.deltaTime;
            transform.Rotate(Vector3.right, rotationX, Space.Self);
        }
        else
        {
            physicsMaterial.dynamicFriction = 0;
        }

        // restrict max z axis velocity
        if (new Vector3(0, 0, rb.velocity.z).magnitude > (currentSpeedX / 2))
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z).normalized * (currentSpeedX / 2) + new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        // flash health bar if low
        if (playerHealth <= 25 && flashNum > 10)
        {
            if (healthBarBg.sprite == healthBarBgWhite)
            {
                healthBarBg.sprite = healthBarBgRed;
            }
            else
            {
                healthBarBg.sprite = healthBarBgWhite;
            }
            flashNum = 0;
        }
        else
        {
            flashNum++;
        }

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 50f);

        hitRock++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock") && hitRock > 50)
        {
            Debug.Log("Hit a rock :(");
            playerHealth -= 25;
            healthBarInner.fillAmount = playerHealth * 0.01f;
            hitRock = 0;
        }
    }

    public void IncrementFishCount()
    {
        Debug.Log("Got a fish :)");
        fishCount++;
        fishCountText.text = string.Format("{0}", fishCount);
    }
}

