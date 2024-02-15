using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PenguinController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishCountText;

    [Header("Health Bar")]
    [SerializeField] private Image healthBarBg;
    [SerializeField] private Image healthBarInner;
    [SerializeField] private Sprite healthBarBgWhite;
    [SerializeField] private Sprite healthBarBgRed;
    [SerializeField] private CountdownTimer countdownTimer;

    [Header("Sounds")]
    [SerializeField] private AudioClip slideSoundAudio;
    [SerializeField] private AudioSource eatFishSound;
    [SerializeField] private AudioSource penguinComplainSound;
    [SerializeField] private AudioSource rockHitSound;
    [SerializeField] private AudioSource penguinScreamSound;

    private AudioSource slideSound;
    
    private Rigidbody rb;
    private Collider playerCollider;
    private PhysicMaterial physicsMaterial;
    private float previousPositionX;

    private bool playerIsGrounded = false;
    private float airborneTimer = 0f;
    private bool screamPlayed = false;

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

        slideSound = GetComponent<AudioSource>();
        slideSound.clip = slideSoundAudio;
        slideSound.loop = true;
        slideSound.volume = 0;
        slideSound.Play();
    }

    void FixedUpdate()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }

        float distanceTraveledX = Math.Abs(transform.position.x - previousPositionX);
        float currentSpeedX = distanceTraveledX / Time.deltaTime;
        previousPositionX = transform.position.x;

        // adjust sound to match speed
        if (playerIsGrounded)
        {
            airborneTimer = 0;
            slideSound.pitch = Math.Min(currentSpeedX / 10f, 40f);
            slideSound.volume = currentSpeedX / 10f;
        }
        else
        {
            airborneTimer += Time.deltaTime;
            slideSound.volume = 0;
            if (airborneTimer >= 1.5f && !screamPlayed)
            {
                penguinScreamSound.PlayOneShot(penguinScreamSound.clip, 0.4f);
                screamPlayed = true;
            }
        }
        

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
            penguinComplainSound.PlayOneShot(penguinComplainSound.clip, 1f);
            rockHitSound.PlayOneShot(rockHitSound.clip, 1f);
            playerHealth -= 25;
            if (playerHealth <= 0)
            {
                PlayerPrefs.SetString("FailReason", "HIT TOO MANY ROCKS");
                EndGame(false);
            }
            healthBarInner.fillAmount = playerHealth * 0.01f;
            hitRock = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ocean"))
        {
            PlayerPrefs.SetString("FailReason", "MISSED THE ICEBERG");
            EndGame(false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerIsGrounded = true;
        }
        else
        {
            playerIsGrounded = false;
        }
    }

    public void IncrementFishCount()
    {
        eatFishSound.PlayOneShot(eatFishSound.clip, 0.5f);
        fishCount++;
        fishCountText.text = string.Format("{0}", fishCount);
    }

    public void EndGame(bool success)
    {
        PlayerPrefs.SetInt("Success", success ? 1 : 0);
        PlayerPrefs.SetInt("Fish", fishCount);
        PlayerPrefs.SetString("Time", countdownTimer.GetTotalTime());
        PlayerPrefs.Save();
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}

