using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1f;

    [Header("Health Bar")]
    [SerializeField] private Image healthBarBg;
    [SerializeField] private Image healthBarInner;
    [SerializeField] private Sprite healthBarBgWhite;
    [SerializeField] private Sprite healthBarBgRed;
    
    private Rigidbody rb;
    private int playerHealth = 100;
    private int flashNum = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // left/right controls
        float horizontalInputSpeed = Input.GetAxis("Horizontal");
        rb.AddForce(Vector3.forward * horizontalInputSpeed * horizontalSpeed, ForceMode.VelocityChange);

        // restrict max z axis velocity
        if (new Vector3(0, 0, rb.velocity.z).magnitude > 10.0f)
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z).normalized * 10.0f + new Vector3(rb.velocity.x, rb.velocity.y, 0);
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
        } else {
            flashNum++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("Player collided with a rock!");
            playerHealth -= 25;
            healthBarInner.fillAmount = playerHealth * 0.01f;

        }
    }
}

