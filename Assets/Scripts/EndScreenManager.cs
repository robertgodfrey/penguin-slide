using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI gameOverText;
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
            successSound.PlayOneShot(successSound.clip, 0.5f);
        }
        else
        {
            failSound.PlayOneShot(failSound.clip, 0.3f);
        }
    }
}
