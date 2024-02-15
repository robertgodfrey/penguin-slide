using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverReason;
    [SerializeField] private TextMeshProUGUI fishLabel;
    [SerializeField] private TextMeshProUGUI timeLabel;

    [SerializeField] private AudioSource failSound;
    [SerializeField] private AudioSource successSound;

    void Start()
    {
        if (PlayerPrefs.GetInt("Success") == 1)
        {
            gameOverText.text = "YOU MADE IT!";
            fishCountText.text = PlayerPrefs.GetInt("Fish").ToString();
            totalTimeText.text = PlayerPrefs.GetString("Time") + "s";
            fishLabel.text = "FISH:";
            timeLabel.text = "TIME:";
            gameOverReason.text = "";
            successSound.PlayOneShot(successSound.clip, 0.5f);
        }
        else
        {
            gameOverText.text = "GAME OVER";
            gameOverReason.text = PlayerPrefs.GetString("FailReason");
            fishLabel.text = "";
            timeLabel.text = "";
            fishCountText.text = "";
            totalTimeText.text = "";
            failSound.PlayOneShot(failSound.clip, 0.3f);
        }
    }

    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
